using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.ViewMeetingOptions)]
    public class ViewMeetingOptionsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var msg = new IntArrayArgs();
            msg.Items.AddRange(session.Player.Data.AsDbPlayer().Temp.MeetingOptions.Select(m => m.Id));
            session.SendResponse(ID, msg);
        }
    }
}
