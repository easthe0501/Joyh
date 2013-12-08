using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Login.Command
{
    [GameCommand((int)CommandEnum.BackdoorLogin)]
    public class BackdoorLoginCommand : GameCommand<BackdoorLoginArgs>
    {
        protected override bool Ready(GameSession session, BackdoorLoginArgs args)
        {
            return session.Player.Status == LoginStatus.CreateSocket;
        }

        protected override void Run(GameSession session, BackdoorLoginArgs args)
        {
            if (args.Account == null)
            {
                session.SendError(ErrorCode.ErrorAccount);
                return;
            }
            var acc = args.Account.Trim();
            var pwd = args.Password.Trim();
#if DEBUG
            if (pwd != "ky")
#else
            if(pwd != session.Server.World.Token)
            //var userSetting = APF.Settings.TestUsers.Find(MyConvert.ToInt32(acc));
            //if(userSetting == null || userSetting.Password != pwd)
#endif
            {
                session.SendError(ErrorCode.ErrorAccount);
                return;
            }
            session.Login(acc);
        }
    }
}
