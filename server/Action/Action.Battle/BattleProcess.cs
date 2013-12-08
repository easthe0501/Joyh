using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Action.Utility;
using Action.Model;

namespace Action.Battle
{
    public class BattleProcess
    {
        private List<BattleFighter> _allFighters = new List<BattleFighter>();
        private BattleGroup _group1;
        private BattleGroup _group2;

        public BattleProcess(IFighterContainer container1, IFighterContainer container2)
        {
            _group1 = container1.CreateBattleGroup(1);
            _group2 = container2.CreateBattleGroup(11);
        }

        public BattleReport Run()
        {
            //准备阶段
            var report = new BattleReport()
            {
                Guid = Guid.NewGuid().ToString(),
                Side1 = _group1.Name,
                Side2 = _group2.Name,
                Win = false,
                Sex1 = _group1.Sex,
                Sex2 = _group2.Sex
            };
            PrepareFighters(report);
            
            //回合循环阶段
            for (var i = 1; i <= 50; i++)
            {
                var round = new BattleRound() { Id = i };
                report.Rounds.Add(round);
                foreach (var fighter in _allFighters)
                {
                    FighterDoAction(round, fighter);
                    if (_group1.AllDied())
                        return report;
                    else if (_group2.AllDied())
                    {
                        report.Win = true;
                        return report;
                    }
                }
            }

            //超时返回结果
            return report;
        }

        private void PrepareFighters(BattleReport report)
        {
            IEnumerable<BattleFighter> _fstFighters = null;
            IEnumerable<BattleFighter> _scdFighters = null;
            if (_group2.Type == BattleGroupType.Environment ||
                _group1.GetSumFirstStrike() >= _group2.GetSumFirstStrike())
            {
                _fstFighters = _group1.GetAliveFighters();
                _scdFighters = _group2.GetAliveFighters();
            }
            else
            {
                _fstFighters = _group2.GetAliveFighters();
                _scdFighters = _group1.GetAliveFighters();
            }

            var count1 = _fstFighters.Count();
            var count2 = _scdFighters.Count();
            for (var i = 0; i < count1 || i < count2; i++)
            {
                if (i < count1)
                    AddFighter(report, _fstFighters.ElementAt(i));
                if (i < count2)
                    AddFighter(report, _scdFighters.ElementAt(i));
            }
        }

        private void AddFighter(BattleReport report, BattleFighter fighter)
        {
            fighter.EnemyGroup = fighter.FriendGroup == _group1 ? _group2 : _group1;
            _allFighters.Add(fighter);
            report.Units.Add(fighter.ToBattleUnit());
        }
        
        /// <summary>
        /// 战士开始行动
        /// </summary>
        /// <param name="round">回合</param>
        /// <param name="self">行动者</param>
        private void FighterDoAction(BattleRound round, BattleFighter self)
        {
            if (!self.IsAlive)
                return;

            //尝试取消Buff
            var readyEffects = self.TryCancelBuffs();
            if (readyEffects != null && readyEffects.Length > 0)
            {
                var readyAction = new BattleAction()
                {
                    UnitId = self.Id,
                    SkillId = -1
                };
                readyAction.Effects.AddRange(readyEffects);
                round.Actions.Add(readyAction);
            }

            //有可能中毒而死，要再判断一次
            if (!self.IsAlive)
                return;

            //眩晕或睡眠，无法行动
            if (self.Status == FighterStatus.Vertigo || self.Status == FighterStatus.Sleep)
                return;

            //准备绝技
            self.BeforeAction();

            //技能行为
            var skillAction = new BattleAction()
            {
                UnitId = self.Id,
                SkillId = self.CastSkill ? self.SkillId : 0
            };
            round.Actions.Add(skillAction);
            foreach (var buffGroup in self.GetActionBuffGroups())
            {
                var targets = self.GetTargets(buffGroup.Range);
                if (targets.Length == 0)
                    return;
                foreach (var target in targets)
                {
                    var effects = target.ApplyBuffGroup(buffGroup, self);
                    skillAction.Effects.AddRange(effects);
                    NoteUpdateXp(target, skillAction);
                }
            }

            //保留气势
            self.AfterAction();
            NoteUpdateXp(self, skillAction);

            //连击判断
            if (self.DoubleHit == DoubleHitStatus.Ready
                && !_group1.AllDied() && !_group2.AllDied())
            {
                self.DoubleHit = DoubleHitStatus.Doing;
                FighterDoAction(round, self);
                self.DoubleHit = DoubleHitStatus.Null;
            }
        }

        private void NoteUpdateXp(BattleFighter fighter, BattleAction action)
        {
            if (fighter.XpChanged)
            {
                action.Effects.Add(new BattleEffect()
                {
                    UnitId = fighter.Id,
                    Type = BattleEffectType.XpUpdate,
                    Data = fighter.XP
                });
                fighter.XpChanged = false;
            }
        }
    }
}
