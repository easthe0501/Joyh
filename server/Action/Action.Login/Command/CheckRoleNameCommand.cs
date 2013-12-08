using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Utility;
using Action.Model;
using Action.Core;

namespace Action.Login.Command
{
    [GameCommand((int)CommandEnum.CheckRoleName)]
    public class CheckRoleNameCommand : GameCommand<string>
    {
        public bool Check(GameSession session, string name)
        {
            if (!NumberHelper.Between(name.Length,
                APF.Settings.Role.NameMinLength,
#if DEBUG
                50))
#else
                APF.Settings.Role.NameMaxLength))
#endif
            {
                session.SendError(ErrorCode.RoleNameLengthError);
                return false;
            }
            if (!WordValidateHelper.FilterForBool(name))
            {
                session.SendError(ErrorCode.RoleNameNotValidate);
                return false;
            }

            //重名验证
            if (APF.Database.ContainsPlayer(name))
            {
                session.SendError(ErrorCode.RoleNameExisted);
                return false;
            }
            return true;
        }

        protected override bool Ready(GameSession session, string args)
        {
            return session.Player.Status == LoginStatus.EnterGate;
        }

        protected override void Run(GameSession session, string args)
        {
            Check(session, MyConvert.Trim(args));
        }
    }
}
