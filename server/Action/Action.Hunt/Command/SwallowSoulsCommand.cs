using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.SwallowSouls)]
    public class SwallowSoulsCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();

            //防止有废魂存在
            List<Soul> tempSouls = player.SoulWarehouse.BackSouls.FindAll(p => p.Setting.Quality == APF.Settings.Role.RubbishSoul);
            foreach (Soul s in tempSouls)
            {
                player.Money += s.Setting.Price;
                player.SoulWarehouse.BackSouls.Destory(s);
            }

            Soul soul = player.SoulWarehouse.BackSouls.SingleOrDefault(s => s.Id == args);
            if (soul == null)
                return;
            if (soul.Level >= 10)
            {
                session.SendError(ErrorCode.SoulLevelLimit);
                return;
            }
            List<Soul> souls = player.SoulWarehouse.BackSouls;
            List<Soul> copySouls = new List<Soul>(player.SoulWarehouse.BackSouls.ToArray());

            int tempExp = soul.Exp;
            int tempLevel = soul.Level;
            var rs = souls.Where(s => s.CompareTo(soul) > 0);
            foreach (var s in rs)
            {
                tempExp += s.Exp;
                int nextExp = soul.GetNextLevelExp();
                if (tempExp >= nextExp)
                {
                    tempExp -= nextExp;
                    tempLevel += 1;
                }
                s.Pos = -1;
                //souls.Destory(s);
                if (tempLevel >= 10)
                    break;
            }
            souls.DestoryAll(s => s.Pos == -1);
            soul.Level = tempLevel;
            soul.Exp = tempExp;
            soul.Refresh();

            //for (int i = 0; i < souls.Count; i++)
            //{
            //    souls[i].Pos = i;
            //}

            //返回
            SoulsArgs swallowSoulsArgs = new SoulsArgs();
            foreach (Soul s in player.SoulWarehouse.BackSouls)
                swallowSoulsArgs.Souls.Add(new SoulArgs() { Id = s.Id, Level = s.Level, Pos = s.Pos, SettingId = s.SettingId, Exp = s.Exp });
            session.SendResponse(ID, swallowSoulsArgs);
        }
    }
}
