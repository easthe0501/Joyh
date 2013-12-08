using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.UnwearSoul)]
    public class UnwearSoulCommand : GameCommand<WearSoulArgs>
    {
        protected override void Run(GameSession session, WearSoulArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (player.SoulWarehouse.BackSouls.Count >= APF.Settings.Role.SoulWarehouseBackSpace)
            {
                session.SendError(ErrorCode.SoulWarehouseBackSpaceNotEnough);
                return;
            }
            var hero = player.Snapshot.Find<Hero>(args.HeroId);
            if (hero == null)
                return;
            //var soul = player.Snapshot.Find<Soul>(args.SoulId);
            var soul = hero.Souls.SingleOrDefault(s => s.Id == args.SoulId);
            if (soul == null)
                return;
            if (player.SoulWarehouse.BackSouls.Exists(p => p.Pos == args.Pos))
                return;

            soul.Pos = args.Pos;
            //soul.Pos = player.SoulWarehouse.BackSouls.Count;
            hero.Souls.Remove(soul);
            hero.Refresh();
            player.SoulWarehouse.BackSouls.Add(soul);

            UnwearSoulArgs unWearSoul = new UnwearSoulArgs();
            unWearSoul.WearSoul = new WearSoulArgs() { HeroId = args.HeroId, SoulId = args.SoulId, Pos = soul.Pos };
            unWearSoul.UnwearSoul = new SoulArgs
                {
                    Id = soul.Id,
                    Level = soul.Level,
                    Pos = soul.Pos,
                    SettingId = soul.SettingId,
                    Exp = soul.Exp,
                };
            session.SendResponse(ID, unWearSoul);
        }
    }
}
