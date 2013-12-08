using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using Action.Engine;
using Action.Model;
using Action.Utility;
using System.Threading;

namespace Action.Login
{
    [Export(typeof(IGameModule))]
    public class LoginModule : GameModule
    {
        public override int Index
        {
            get { return 0; }
        }

        public override void Load(GameWorld world)
        {
            world.Token = Guid.NewGuid().ToString();
            var dftSetting = APF.Settings.Commands.Find(0);
            foreach (var command in world.AppServer.Commands.Cast<GameCommandBase>())
            {
                var setting = APF.Settings.Commands.Find(command.ID);
                if (setting == null)
                    setting = dftSetting;
                command.CD = setting.CD;
                command.CheckRight = setting.CheckRight;
            }
            world.Data = APF.Database.LoadWorld();
        }        

        public override void Unload(GameWorld world)
        {
            var dbWorld = world.Data.AsDbWorld();
            dbWorld.Dispose();
            APF.Database.SaveWorld(dbWorld);
        }

        public override void EnterGame(GamePlayer player)
        {
            player.GetSummary().Context = player.Session.Context;
            var dbPlayer = player.Data.AsDbPlayer();
            dbPlayer.EnterTime = DateTime.Now;
            APF.Database.SavePlayer(dbPlayer);
            player.Session.Logger.LogInfo(player.Session, string.Format("玩家[{0}({1})]进入游戏", player.Name, player.Account));
        }

        public override void LeaveGame(GamePlayer player)
        {
            var dbPlayer = player.Data.AsDbPlayer();
            dbPlayer.LeaveTime = DateTime.Now;
            APF.Database.SavePlayer(dbPlayer);
            player.Session.Logger.LogInfo(player.Session, string.Format("玩家[{0}({1})]离开游戏", player.Name, player.Account));
        }

        public override void PerFiveMinutes(GameWorld world)
        {
            var logger = world.AppServer.Logger;
            world.AppServer.MainQueue.Add(() =>
                {
                    //保存数据
                    var db = APF.Database;
                    try
                    {
                        db.SaveWorld(world.Data.AsDbWorld());
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(string.Format("保存世界数据异常\r\n{0}", ex));
                    }
                    foreach (var player in world.AllPlayers)
                    {
                        try
                        {
                            db.SavePlayer(player.Data.AsDbPlayer());
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(string.Format("保存玩家【{0}】数据异常\r\n{1}", player.Session, ex));
                        }
                    }
                    world.AppServer.Logger.LogInfo("成功保存数据");
                });
        }

        public override void PerHalfHour(GameWorld world)
        {
            world.Token = Guid.NewGuid().ToString();
        }
    }
}
