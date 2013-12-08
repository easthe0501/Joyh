using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Action.Model
{
    public class BattleFighter : IFighter
    {
        private List<BuffState> _buffStates = new List<BuffState>();
        private HashSet<int> _immuneBuffs = new HashSet<int>();

        public BattleFighter(IFighter fighter, int id, int pos)
        {
            Id = id;
            Pos = pos;
            SettingId = fighter.SettingId;
            Level = fighter.Level;
            HP = MaxHP = fighter.HP;
            XP = fighter.XP;
            FirstStrike = fighter.FirstStrike;
            CommonAttack = fighter.CommonAttack;
            CommonDefence = fighter.CommonDefence;
            SkillAttack = fighter.SkillAttack;
            SkillDefence = fighter.SkillDefence;
            MindAttack = fighter.MindAttack;
            MindDefence = fighter.MindDefence;
            Hit = fighter.Hit;
            Dodge = fighter.Dodge;
            Crack = fighter.Crack;
            Block = fighter.Block;
            Crit = fighter.Crit;
            Tenacity = fighter.Tenacity;
            SkillId = fighter.SkillId;
            FightingCapacity = fighter.GetFightingCapacity();
            DoubleHit = DoubleHitStatus.Null;
            CastSkill = false;

            if (fighter.ImmuneBuffs != null)
            {
                foreach(var buff in fighter.ImmuneBuffs)
                    _immuneBuffs.Add(buff);
            }
        }

        public int Id { get; private set; }
        public int Pos { get; set; }
        public int SettingId { get; set; }
        public int Level { get; set; }
        public int MaxHP { get; set; }

        public bool IsAlive
        {
            get { return _hp > 0; }
        }

        private int _hp;
        public int HP
        {
            get { return _hp; }
            set { _hp = Math.Min(MaxHP, Math.Max(0, value)); }
        }

        private int _xp;
        public int XP
        {
            get { return _xp; }
            set { _xp = Math.Min(BattleDefs.MaxXp, Math.Max(0, value)); }
        }

        private int _firstStrike;
        public int FirstStrike
        {
            get { return _firstStrike; }
            set { _firstStrike = value; }
        }

        private int _commonAttack;
        public int CommonAttack
        {
            get { return _commonAttack; }
            set { _commonAttack = Math.Max(0, value); }
        }

        private int _commonDefence;
        public int CommonDefence
        {
            get { return _commonDefence; }
            set { _commonDefence = Math.Max(0, value); }
        }

        private int _skillAttack;
        public int SkillAttack
        {
            get { return _skillAttack; }
            set { _skillAttack = Math.Max(0, value); }
        }

        private int _skillDefence;
        public int SkillDefence
        {
            get { return _skillDefence; }
            set { _skillDefence = Math.Max(0, value); }
        }

        private int _mindAttack;
        public int MindAttack
        {
            get { return _mindAttack; }
            set { _mindAttack = Math.Max(0, value); }
        }

        private int _mindDefence;
        public int MindDefence
        {
            get { return _mindDefence; }
            set { _mindDefence = Math.Max(0, value); }
        }

        private int _hit;
        public int Hit
        {
            get { return _hit; }
            set { _hit = Math.Max(0, value); }
        }

        private int _dodge;
        public int Dodge
        {
            get { return _dodge; }
            set { _dodge = Math.Max(0, value); }
        }

        private int _crack;
        public int Crack
        {
            get { return _crack; }
            set { _crack = Math.Max(0, value); }
        }

        private int _block;
        public int Block
        {
            get { return _block; }
            set { _block = Math.Max(0, value); }
        }

        private int _crit;
        public int Crit
        {
            get { return _crit; }
            set { _crit = Math.Max(0, value); }
        }

        private int _tenacity;
        public int Tenacity
        {
            get { return _tenacity; }
            set { _tenacity = Math.Max(0, value); }
        }

        public int[] ImmuneBuffs
        {
            get { return _immuneBuffs.ToArray(); }
        }

        public int SkillId { get; set; }
        public FighterStatus Status { get; set; }
        public int FightingCapacity { get; set; }
        public BattleGroup FriendGroup { get; set; }
        public BattleGroup EnemyGroup { get; set; }
        //public bool InDoubleHit { get; set; }
        public DoubleHitStatus DoubleHit { get; set; }
        public bool XpChanged { get; set; }
        public int PlusXp { get; set; }
        public bool CastSkill { get; private set; }
        public float XpHurtRatio { get; private set; }
        private bool _inDodge = false;

        public void BeforeAction()
        {
            if (Status != FighterStatus.Lock && XP >= BattleDefs.SkillXp)
            {
                CastSkill = true;
                XpHurtRatio = XP / 100f;
            }
            else
            {
                CastSkill = false;
                XpHurtRatio = 0;
            }
        }

        public void AfterAction()
        {
            if (CastSkill)
                XP = GetSkill().RemainXp + PlusXp;
            else
                XP += PlusXp;
            PlusXp = 0;
            XpChanged = true;
        }

        public SkillSetting GetSkill()
        {
            return APF.Settings.Skills.Find(SkillId);
        }

        public BuffGroup[] GetActionBuffGroups()
        {
            if (CastSkill)
            {
                var skill = APF.Settings.Skills.Find(SkillId);
                if (skill != null)
                    return skill.BuffGroups;
            }
            return BuffGroup.Defaults;
        }

        private BattleEffect CalcByHurt(Buff buff, BattleFighter sender)
        {
            BattleEffect effect = new BattleEffect();
            float hurtData = 0;

            //计算闪避
            var dodgePct = Math.Max(0, Math.Min((int)((Dodge - sender.Hit) * BattleDefs.DodgeRatio)
                + BattleDefs.InitDodgePct, BattleDefs.MaxDodgePct));
            if (Status != FighterStatus.Sleep && APF.Random.Percent(dodgePct))
            {
                _inDodge = true;
                effect.Type = BattleEffectType.Dodge;
                return effect;
            }

            //计算格挡
            var hurtRdcPct = 0;
            if (Status != FighterStatus.Vertigo)
            {
                hurtRdcPct = Math.Max(0, Math.Min((int)((Block - sender.Crack) * BattleDefs.BlockRatio),
                    BattleDefs.MaxBlockPct));
            }

            //计算暴击
            if (APF.Random.Percent((int)((sender.Crit - Tenacity) * BattleDefs.CritRatio) + BattleDefs.InitCritPct))
                effect.Type = BattleEffectType.Crit;

            //计算伤害
            if (sender.CastSkill)
            {
                var skill = sender.GetSkill();
                hurtData = APF.BattleCalculator.CalcHurtData(sender, this, skill, buff);
            }
            else
            {
                hurtData = sender.CommonAttack - CommonDefence;

                //普攻双方加气势
                XP += BattleDefs.StepXp;
                sender.PlusXp += BattleDefs.StepXp;
                XpChanged = true;
            }

            //代入破防公式
            hurtData = Math.Max(0, hurtData) + sender.Level + APF.Random.Range(10, 20);

            //计算格挡减伤和暴击加伤
            if (hurtRdcPct > 0)
                hurtData *= (1 - hurtRdcPct / 100f);
            if (effect.Type == BattleEffectType.Crit)
                hurtData *= BattleDefs.CritHurtRatio;

            if (hurtData < 0)
            {
            }
            
            //血量赋值，如果HP为0，攻击者额外获得气势
            HP -= (effect.Data = (int)hurtData);
            if (HP == 0)
                sender.PlusXp += BattleDefs.StepXp;

            return effect;
        }

        private BattleEffect CalcByCure(Buff buff, BattleFighter sender)
        {
            var data = (int)APF.BattleCalculator.CalcCureData(sender, buff);
            HP += data;
            return new BattleEffect() { Type = BattleEffectType.Cure, Data = data };
        }

        public BattleEffect[] ApplyBuffGroup(BuffGroup group, BattleFighter sender)
        {
            return _ApplyBuffGroup(group, sender).ToArray();
        }

        private IEnumerable<BattleEffect> _ApplyBuffGroup(BuffGroup group, BattleFighter sender)
        {
            foreach (var buff in group.Buffs)
            {
                var effect = ApplyBuff(buff, sender);
                if (effect != null)
                {
                    effect.UnitId = Id;
                    yield return effect;
                }
                if (_inDodge)
                {
                    _inDodge = false;
                    yield break;
                }
            }
        }

        private BattleEffect ApplyBuff(Buff buff, BattleFighter sender)
        {
            if (_immuneBuffs.Contains((int)buff.Effect))
                return null;
            if (buff.Round == 0)
            {
                switch (buff.Effect)
                {
                    case BuffEffect.BattleHurt:
                        return CalcByHurt(buff, sender);
                    case BuffEffect.BattleCure:
                        return CalcByCure(buff, sender);
                    case BuffEffect.BattleXpPlus:
                        XP += buff.Data;
                        return new BattleEffect() { Type = BattleEffectType.XpUp, Data = buff.Data };
                    case BuffEffect.BattleXpMinus:
                        XP -= buff.Data;
                        return new BattleEffect() { Type = BattleEffectType.XpDown, Data = buff.Data };
                    case BuffEffect.BattleXpFull:
                        XP = Math.Max(XP, BattleDefs.SkillXp);
                        return new BattleEffect() { Type = BattleEffectType.XpFull };
                    case BuffEffect.BattleXpClear:
                        XP = 0;
                        return new BattleEffect() { Type = BattleEffectType.XpClear };
                    case BuffEffect.BattleDoubleHit:
                        if (DoubleHit == DoubleHitStatus.Null && APF.Random.Percent(buff.Data))
                        {
                            DoubleHit = DoubleHitStatus.Ready;
                            return new BattleEffect() { Type = BattleEffectType.DoubleHit };
                        }
                        return null;
                }
            }
            else if (buff.Effect < BuffEffect.BattleVertigo || APF.Random.Percent(buff.Data))
            {
                BattleEffect resultEffect = null;
                switch (buff.Effect)
                {
                    case BuffEffect.BattleHitPlus:
                        Hit += buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.HitUp, Data = buff.Data };
                        break;
                    case BuffEffect.BattleHitMinus:
                        Hit -= buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.HitDown, Data = buff.Data };
                        break;
                    case BuffEffect.BattleDodgePlus:
                        Dodge += buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.DodgeUp, Data = buff.Data };
                        break;
                    case BuffEffect.BattleDodgeMinus:
                        Dodge -= buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.DodgeDown, Data = buff.Data };
                        break;
                    case BuffEffect.BattleCrackPlus:
                        Crack += buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.CrackUp, Data = buff.Data };
                        break;
                    case BuffEffect.BattleCrackMinus:
                        Crack -= buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.CrackDown, Data = buff.Data };
                        break;
                    case BuffEffect.BattleBlockPlus:
                        Block += buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.BlockUp, Data = buff.Data };
                        break;
                    case BuffEffect.BattleBlockMinus:
                        Block -= buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.BlockDown, Data = buff.Data };
                        break;
                    case BuffEffect.BattleCritPlus:
                        Crit += buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.CritUp, Data = buff.Data };
                        break;
                    case BuffEffect.BattleCritMinus:
                        Crit -= buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.CritDown, Data = buff.Data };
                        break;
                    case BuffEffect.BattleTenacityPlus:
                        Tenacity += buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.TenacityUp, Data = buff.Data };
                        break;
                    case BuffEffect.BattleTenacityMinus:
                        Tenacity -= buff.Data;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.TenacityDown, Data = buff.Data };
                        break;
                    case BuffEffect.BattleVertigo:
                        Status = FighterStatus.Vertigo;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.StatusVertigo };
                        break;
                    case BuffEffect.BattleSleep:
                        Status = FighterStatus.Sleep;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.StatusSleep };
                        break;
                    case BuffEffect.BattlePoisoning:
                        Status = FighterStatus.Poisoning;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.StatusPoisoning };
                        break;
                    case BuffEffect.BattleLock:
                        Status = FighterStatus.Lock;
                        resultEffect = new BattleEffect() { Type = BattleEffectType.StatusLock };
                        break;
                }
                _buffStates.RemoveAll(bs => bs.Buff.Effect >= BuffEffect.BattleVertigo);
                _buffStates.Add(buff.ToBuffState());
                return resultEffect;
            }
            return null;
        }

        private void RestoreStatus()
        {
            Status = FighterStatus.Normal;
            _buffStates.RemoveAll(b => b.Buff.Effect >= BuffEffect.BattleVertigo);
        }

        public BattleEffect[] TryCancelBuffs()
        {
            var effects = _TryCancelBuffs().ToArray();
            foreach (var effect in effects)
                effect.UnitId = Id;
            return effects;
        }

        private IEnumerable<BattleEffect> _TryCancelBuffs()
        {
            for (int i = 0; i < _buffStates.Count; i++)
            {
                var bs = _buffStates[i];
                if (bs.Round > 0)
                {
                    bs.Round--;
                }
                else
                {
                    _buffStates.Remove(bs);
                    i--;
                    var buff = bs.Buff;
                    switch (buff.Effect)
                    {
                        case BuffEffect.BattleHitPlus:
                            Hit -= buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.HitDown };
                            break;
                        case BuffEffect.BattleHitMinus:
                            Hit += buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.HitUp };
                            break;
                        case BuffEffect.BattleDodgePlus:
                            Dodge -= buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.DodgeDown };
                            break;
                        case BuffEffect.BattleDodgeMinus:
                            Dodge += buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.DodgeUp };
                            break;
                        case BuffEffect.BattleCrackPlus:
                            Crack -= buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.CrackDown };
                            break;
                        case BuffEffect.BattleCrackMinus:
                            Crack += buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.CrackUp };
                            break;
                        case BuffEffect.BattleBlockPlus:
                            Block -= buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.BlockDown };
                            break;
                        case BuffEffect.BattleBlockMinus:
                            Block += buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.BlockUp };
                            break;
                        case BuffEffect.BattleCritPlus:
                            Crit -= buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.CritDown };
                            break;
                        case BuffEffect.BattleCritMinus:
                            Crit += buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.CritUp };
                            break;
                        case BuffEffect.BattleTenacityPlus:
                            Tenacity -= buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.TenacityDown };
                            break;
                        case BuffEffect.BattleTenacityMinus:
                            Tenacity += buff.Data;
                            yield return new BattleEffect() { Type = BattleEffectType.TenacityUp };
                            break;
                        case BuffEffect.BattleVertigo:
                        case BuffEffect.BattleSleep:
                        case BuffEffect.BattlePoisoning:
                        case BuffEffect.BattleLock:
                            RestoreStatus();
                            yield return new BattleEffect() { Type = BattleEffectType.StatusNormal };
                            break;
                    }
                }
            }

            if (Status == FighterStatus.Poisoning && DoubleHit != DoubleHitStatus.Doing)
            {
                var hurt = (int)(MaxHP * 0.1f);
                HP -= hurt;
                yield return new BattleEffect()
                {
                    UnitId = Id,
                    Type = BattleEffectType.Poisoning,
                    Data = hurt
                };
            }
        }

        public BattleUnit ToBattleUnit()
        {
            return new BattleUnit()
            {
                Id = this.Id,
                Level = this.Level,
                SkillId = this.SkillId,
                Pos = this.Pos,
                SettingId = this.SettingId,
                HP = this.HP,
                XP = this.XP
            };
        }
    }

    public enum FighterStatus
    {
        Normal,
        Vertigo,
        Sleep,
        Poisoning,
        Lock
    }

    public enum DoubleHitStatus
    {
        Null,
        Ready,
        Doing
    }
}
