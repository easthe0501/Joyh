using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;
using Action.Utility;
using Action.Core;

namespace Action.Login.Command
{
    [GameCommand((int)CommandEnum.CreateRole)]
    public class CreateRoleCommand : GameCommand<CreateRoleArgs>
    {
        protected override bool Ready(GameSession session, CreateRoleArgs args)
        {
            return session.Player.Status == LoginStatus.EnterGate
                && NumberHelper.Between(args.Sex, 0, 1);
                //&& NumberHelper.Between(args.Job, 1, 4)
                //&& NumberHelper.Between(args.Face, 1, APF.Settings.Role.FaceMaxId);
        }

        protected override void Run(GameSession session, CreateRoleArgs args)
        {
            if (session.Server.FindCommand<CheckRoleNameCommand>()
                .Check(session, MyConvert.Trim(args.Name)))
            {
                //创建玩家角色
                var player = new Player();
                player.Account = session.Player.Account;
                player.Name = args.Name;
                player.Job = args.Job;
                player.Sex = args.Sex;
                player.Face = args.Face;
                player.Init();

                //绑定玩家账户和角色
                var account = new Account();
                account.Key = player.Key;
                account.Name = player.Account;
                account.Player = args.Name;
                account.Init();

                //玩家进入游戏
                session.Server.World.Data.AsDbWorld().CreateSummary(account.Name, player.Name, player.Sex);
                session.EnterGame(player);

                //默认升到一级，并打开初始奖励
                player.OnLevelUp(session, 0);
                //if(player.Account.StartsWith("sp"))
                OpenInitPrize(session);

                //CreateRole
                foreach (var module in session.Server.ModuleFactory.Modules)
                    module.RaiseCreateRole(session.Player);

                //保存数据
                APF.Database.SavePlayer(player);
                APF.Database.SaveAccount(account);
            }
        }

        private void OpenInitPrize(GameSession session)
        {
            var prize = APF.Settings.Role.InitPrize;
            if (prize != null)
                prize.Open(session, PrizeSource.Login);
        }

        private void RunScript(GameSession session)
        {
            session.Server.ScriptEngine.Run(new ScriptVar(session, 0, ""),
                APF.Settings.Script.OnCreateRole);
        }
    }
}
