using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Role
{
    [Export(typeof(IGameModule))]
    public class RoleModule : GameModule, IRoleModule
    {
        public override void EnterGame(GamePlayer player)
        {
            var dbPlayer = player.Data.AsDbPlayer();

            //历史
            if (dbPlayer.EnterTime.Date > dbPlayer.LeaveTime.Date)
                OnDayPast(player, dbPlayer);

            //体力
            var halfHours = GetOfflineHalfHours(dbPlayer);
            EnergyAutoPlus(dbPlayer, halfHours);
        }

        public override void LevelUp(GamePlayer player, int oldLevel)
        {
            var dbPlayer = player.Data.AsDbPlayer();
            foreach (var lvSetting in APF.Settings.Levels.All.Where(l => l.Id > oldLevel && l.Id <= dbPlayer.Level))
            {
                var embattlePos = lvSetting.EmbattlePos;
                if (embattlePos > 0)
                {
                    dbPlayer.Permission.EmbattlePoses.Add(embattlePos);
                    //player.Session.SendResponse((int)CommandEnum.UnlockEmbattlePos, embattlePos);
                }
                var soulPos = lvSetting.SoulPos;
                if (soulPos > 0)
                {
                    dbPlayer.Permission.SoulPoses.Add(soulPos);
                    //player.Session.SendResponse((int)CommandEnum.UnlockSoulPos, soulPos);
                }
            }
        }

        private int GetOfflineHalfHours(Player dbPlayer)
        {
            var timeSpan = DateTime.Now - dbPlayer.LeaveTime;
            return (int)timeSpan.TotalMinutes / 30;
        }

        private bool EnergyAutoPlus(Player dbPlayer, int halfHours)
        {
            var maxEnergy = APF.Settings.Role.EnergyMax;
            if (halfHours > 0 && dbPlayer.Energy < maxEnergy)
            {
                dbPlayer.Energy += APF.Settings.Role.EnergyAutoPlus * halfHours;
                if (dbPlayer.Energy > maxEnergy)
                    dbPlayer.Energy = maxEnergy;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 每半小时执行一次，恢复体力
        /// </summary>
        /// <param name="world"></param>
        public override void PerHalfHour(GameWorld world)
        {
            var now = DateTime.Now;
            foreach (var player in world.AllPlayers)
            {
                var dbPlayer = player.Data.AsDbPlayer();

                //历史
                if (now.Hour == 0 && now.Minute == 0)
                    OnDayPast(player, dbPlayer);

                //体力
                if (EnergyAutoPlus(dbPlayer, 1))
                    player.Session.SendResponse((int)CommandEnum.RefreshEnergy, dbPlayer.Energy);
            }
        }

        public void AddRight(GamePlayer player, int cmdId)
        {
            player.Permission.Add(cmdId);
            var dbPlayer = player.Data.AsDbPlayer();
            if (!dbPlayer.Permission.Commands.Contains(cmdId))
                dbPlayer.Permission.Commands.Add(cmdId);
        }

        private void OnDayPast(GamePlayer gPlayer, Player dbPlayer)
        {
            //重置购买次数
            gPlayer.Session.SendResponse((int)CommandEnum.RefreshDailyCount,
                new DailyCountArgs()
                {
                    BuyMoney = dbPlayer.DailyCountHistory.BuyMoney = 0,
                    BuyEnergy = dbPlayer.DailyCountHistory.BuyEnergy = 0,
                    ContributeForGuild = dbPlayer.DailyCountHistory.ContributeForGuild = 0,
                    Pvp = dbPlayer.DailyCountHistory.Pvp = 0,
                    PvpCount = dbPlayer.DailyCountHistory.BuyPvpCount = 0
                });

            //派发每日奖励
            gPlayer.Session.Server.ModuleFactory.Module<IPrizeModule>().AddPrize(gPlayer.World, gPlayer.Name,
                APF.Settings.Vips.Find(dbPlayer.Vip).DailyPrize);

            //TaskModule.OnDayPast
            gPlayer.World.AppServer.ModuleFactory.Module<ITaskModule>().OnDayPast(gPlayer, dbPlayer);

        }
    }
}
