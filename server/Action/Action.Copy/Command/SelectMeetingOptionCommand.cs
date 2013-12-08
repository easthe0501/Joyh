using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.SelectMeetingOption)]
    public class SelectMeetingOptionCommand : GameCommand<int>
    {
        protected override bool Ready(GameSession session, int args)
        {
            return base.Ready(session, args) && args >= 0;
        }

        protected override void Run(GameSession session, int args)
        {
            var options = session.Player.Data.AsDbPlayer().Temp.MeetingOptions;
            if (args >= options.Length)
            {
                session.SendError(ErrorCode.MeetingOptionMissing);
                return;
            }
            var option = options[args];
            option.Prize.Open(session, PrizeSource.CopyGrid);
        }
    }
}
