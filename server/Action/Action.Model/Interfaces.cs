using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public interface IInitialization
    {
        void Init();
    }

    //public interface IEntity
    //{
    //    int Id { get; }
    //    IEntityContainer Container { get; }
    //    void Init(IEntityContainer container);
    //    void Load(IEntityContainer container);
    //}

    public interface IEntityRoot
    {
        IdGen IdGen { get; }
        Snapshot Snapshot { get; }
    }

    //public interface IBox
    //{
    //    int Gold { get; set; }
    //    int Money { get; set; }
    //    int Energy { get; set; }
    //    int Repute { get; set; }
    //    IdCountPair[] Items { get; set; }
    //}

    public interface IFighter
    {
        int Level { get; }
        int HP { get; }
        int XP { get; }
        int FirstStrike { get; }
        int CommonAttack { get; }
        int CommonDefence { get; }
        int SkillAttack { get; }
        int SkillDefence { get; }
        int MindAttack { get; }
        int MindDefence { get; }
        int Hit { get; }
        int Dodge { get; }
        int Crack { get; }
        int Block { get; }
        int Crit { get; }
        int Tenacity { get; }
        int SkillId { get; }
        int SettingId { get; }
        int[] ImmuneBuffs { get; }
    }

    public interface IFighterContainer
    {
        BattleGroup CreateBattleGroup(int startId);
    }
}
