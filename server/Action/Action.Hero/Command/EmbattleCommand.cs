using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Utility;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.Embattle)]
    public class EmbattleCommand : GameCommand<HeroPosArgs>
    {
        protected override bool Ready(GameSession session, HeroPosArgs args)
        {
            return base.Ready(session, args) && NumberHelper.Between(args.TargetPos, 0, 9);
        }

        protected override void Run(GameSession session, HeroPosArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var hero = player.Heros.SingleOrDefault(p => p.Id == args.HeroId);
            if (hero == null)
                return;
            if (hero.Pos == args.TargetPos)
                return;

            //下阵
            if (args.TargetPos == 0)
            {
                //主角不能下阵
                if (hero == player.GetMainHero())
                {
                    session.SendError(ErrorCode.MainHeroMustInBattle);
                    return;
                }
            }
            //上阵或移动
            if (args.TargetPos != 0)
            {
                //判断目标阵位是否开放
                if (!player.Permission.EmbattlePoses.Contains(args.TargetPos))
                    return;
                var targetHero = player.Heros.SingleOrDefault(p => p.Pos == args.TargetPos);
                //移动时，判断目标阵位是否有伙伴
                if (hero.Pos == 0 && targetHero == null)
                {
                    if (player.Heros.Where(h => h.Pos > 0).Count() >= APF.Settings.Role.MaxEmbattleCount)
                    {
                        session.SendError(ErrorCode.EmbattleIsFull);
                        return;
                    }
                }
                if (targetHero == player.GetMainHero() && hero.Pos == 0)
                {
                    session.SendError(ErrorCode.MainHeroMustInBattle);
                    return;
                }
                if (targetHero != null)
                {
                    targetHero.Pos = hero.Pos;
                    session.SendResponse(ID, new HeroPosArgs() { HeroId = targetHero.Id, TargetPos = targetHero.Pos });
                }
            }
            hero.Pos = args.TargetPos;
            session.SendResponse(ID, args);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.Embattle, null);
        }
    }
}
