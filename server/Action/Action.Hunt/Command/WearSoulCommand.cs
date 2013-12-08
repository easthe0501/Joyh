using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.WearSoul)]
    public class WearSoulCommand : GameCommand<WearSoulArgs>
    {
        protected override void Run(GameSession session, WearSoulArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var hero = player.Snapshot.Find<Hero>(args.HeroId);
            if (hero == null)
                return;
            if (!player.Permission.SoulPoses.Contains(args.Pos))
            {
                session.SendError(ErrorCode.SoulSpaceNotEnough);
                return;
            }

            var soul = player.SoulWarehouse.BackSouls.SingleOrDefault(s => s.Id == args.SoulId);
            if (soul == null)
                return;
            if (hero.Souls.Exists(s => s.SettingId == soul.SettingId))
            {
                session.SendError(ErrorCode.HasSameSoul);
                return;
            }

            UnwearSoulArgs unWearSoul = new UnwearSoulArgs();
            //伙伴战魂位置上有战魂
            Soul heroSoul = hero.Souls.SingleOrDefault(s => s.Pos == args.Pos);
            if (heroSoul != null)
            {
                heroSoul.Pos = soul.Pos;
                player.SoulWarehouse.BackSouls.Add(heroSoul);
                hero.Souls.Remove(heroSoul);
                unWearSoul.UnwearSoul = new SoulArgs
                {
                    Id = heroSoul.Id,
                    Level = heroSoul.Level,
                    Pos = heroSoul.Pos,
                    SettingId = heroSoul.SettingId,
                    Exp = heroSoul.Exp,
                };                
            }
            ////所有卦符移动
            //var souls = player.SoulWarehouse.BackSouls.Where(s => s.Pos > soul.Pos);
            //foreach (var s in souls)
            //{
            //    s.Pos -= 1;
            //}

            soul.Pos = args.Pos;
            hero.Souls.Add(soul);
            hero.Refresh();
            player.SoulWarehouse.BackSouls.Remove(soul);
            unWearSoul.WearSoul = args;

            session.SendResponse(ID, unWearSoul);

        }
    }
}
