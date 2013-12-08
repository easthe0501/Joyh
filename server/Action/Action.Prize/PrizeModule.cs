using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using Action.Utility;

namespace Action.Prize
{
    [Export(typeof(IGameModule))]
    public class PrizeModule : GameModule, IPrizeModule
    {
        public void NoticePrizes(GamePlayer player)
        {
            NoticePrizes(player, player.GetSummary().PrizeObjs);            
        }

        private void NoticePrizes(GamePlayer player, List<Model.PrizeObj> prizeObjs)
        {
            if (prizeObjs.Count > 0)
            {
                var msg = new PrizeArrayArgs();
                msg.Prizes.AddRange(prizeObjs.Select(p =>
                    new PrizeArgs() { Id = p.Id, Title = p.Prize.Title }));
                player.Session.SendResponse((int)CommandEnum.NoticePrizeObjs, msg);
            }
        }

        public bool AddPrize(GameWorld world, string playerName, string prizeJson)
        {
            var prize = JsonHelper.FromJson<Model.Prize>(prizeJson, false);
            if (prize == null)
                return false;
            return AddPrize(world, playerName, prize);
        }

        public bool AddPrize(GameWorld world, string playerName, Model.Prize prize)
        {
            var playerSum = world.Data.AsDbWorld().GetSummary(playerName);
            if (playerSum == null)
                return false;
            return AddPrize(world, playerSum, prize);
        }

        public bool AddPrize(GameWorld world, PlayerSummary playerSum, Model.Prize prize)
        {
            if (prize == null)
                return false;
            var prizeObj = prize.CreateObj(playerSum.PrizeObjs.Count > 0 ? playerSum.PrizeObjs.Max(p => p.Id) + 1 : 1);
            playerSum.PrizeObjs.Add(prizeObj);

            var player = world.GetPlayer(playerSum.Name);
            if (player != null)
                NoticePrizes(player, playerSum.PrizeObjs);
            return true;
        }
        
        
        public int AddPrize(GameWorld world, IEnumerable<PlayerSummary> summaries, Model.Prize prize)
        {
            int count = 0;
            foreach (var summary in summaries)
            {
                if (AddPrize(world, summary, prize))
                    count++;
            }
            return count;
        }
    }
}
