using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using System.Diagnostics;
using Action.Utility;

namespace Action.Hero
{
    [Export(typeof(IGameModule))]
    public class HeroModule : GameModule, IHeroModule
    {
        public override void Load(GameWorld world)
        {
            foreach (var hero in APF.Settings.Heros.All)
            {
                var strHeroId = hero.Id.ToString();

                //合法性验证
                Trace.Assert(NumberHelper.Between(hero.Sex, 0, 1), strHeroId);
                Trace.Assert(NumberHelper.Between(hero.Quality, 1, 5), strHeroId);
                Trace.Assert(hero.Life > 0, strHeroId);
                Trace.Assert(hero.Force > 0, strHeroId);
                Trace.Assert(hero.Skill > 0, strHeroId);
                Trace.Assert(hero.Charm > 0, strHeroId);
                Trace.Assert(hero.Repute >= 0, strHeroId);
                Trace.Assert(hero.Money >= 0, strHeroId);

                //引用关系验证
                Trace.Assert(APF.Settings.Jobs.Find(hero.JobId) != null, strHeroId);
                Trace.Assert(APF.Settings.Skills.Find(hero.SkillId) != null, strHeroId);
            }
        }

#if DEBUG1
        private int[] _heroIds = new int[]{90011,90012,90013,90014,90015,90016,90017,90018,90019,90020,
            90021,90022,90023,90024,90025,90026,90027,90028,90029,90030};
        //private int[] _heroIds = new int[] { 90011, 90012, 90018, 90020, 90021, 90022, 90025, 90026, 90029, 90030 };
        private int[] _heroPoses = new int[] { 1, 2, 3, 4, 6, 7, 8, 9 };

        public override void CreateRole(GamePlayer player)
        {
            var dbPlayer = player.Data.AsDbPlayer();
            //var poses = _heroPoses.Randoms(4).ToArray();
            //var i = 0;
            foreach (var heroId in _heroIds.Randoms(4))
            {
                var hero = APF.Factory.Create<Model.Hero>(dbPlayer, heroId);
                hero.Pos = 0;
                dbPlayer.Heros.Add(hero);
            }
            foreach (int hid in _heroIds)
                dbPlayer.Permission.Heros.Add(hid);
        }
#endif

        public void UnlockHeros(GameSession session, Player player, params int[] heroIds)
        {
            foreach (var heroId in heroIds)
                player.Permission.Heros.Add(heroId);
            var msg = new IntArrayArgs();
            msg.Items.AddRange(heroIds);
            session.SendResponse((int)CommandEnum.UnlockHeros, msg);
        }
    }
}
