using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Login
{
    static class LoginHelper
    {
        public static void EnterGame(this GameSession session, Player player)
        {
            var unlockTime = session.Server.World.Data.AsDbWorld().GetSummary(player.Name).UnlockTime;
            if (unlockTime < DateTime.Now)
            {
                session.Player.BindDbPlayer(player);
                session.EnterGame(player.Key, player.Name);
                session.SendResponse((int)CommandEnum.EnterGame,
                    new EnterGameArgs() { Key = player.Key, Account = player.Account, Player = player.Name });
            }
            else
            {
                session.SendResponse((int)CommandEnum.AccountLocked, unlockTime.ToLocalString());
            }
        }

        public static void BindDbPlayer(this GamePlayer gPlayer, Player dbPlayer)
        {
            gPlayer.Data = dbPlayer;
            gPlayer.Display.Job = dbPlayer.Job;
            gPlayer.Display.Sex = dbPlayer.Sex;
            gPlayer.Display.Face = dbPlayer.Face;
            gPlayer.Permission.Import(dbPlayer.Permission.Commands);
        }

        public static void Login(this GameSession session, string acc)
        {
            session.EnterGate(acc);
            var player = session.Player.Data != null ? session.Player.Data.AsDbPlayer() : null;
            if (player == null)         //为null表示不是踢人下线
            {
                var account = APF.Database.LoadAccount(acc);
                if (account == null)
                {
                    session.SendResponse(1000, 2);
                    return;
                }
                else
                    player = APF.Database.LoadPlayer(account.Player);
            }
            session.EnterGame(player);
        }
    }
}
