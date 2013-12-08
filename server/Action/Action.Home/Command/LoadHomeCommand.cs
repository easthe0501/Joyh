using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;

namespace Action.Home.Command
{
    [GameCommand((int)CommandEnum.LoadHome)]
    public class LoadHomeCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args) 
        {
            var player = APF.LoadPlayer(session.Player, args);
            if (player.Home == null)
                return;

            var loadhomeArgs = new LoadHomeArgs();
            foreach (Building b in player.Home.Buildings)
            {
                int timeRest = -1;
                if (b.Setting.IfProduct)
                {
                    timeRest = (int)((b.AcquireTime.AddMinutes(b.Setting.Product.SpendTime) - DateTime.Now).TotalSeconds);
                    if (timeRest < 0)
                        timeRest = 0;
                }
                loadhomeArgs.BuildArgsCollection.Add(new BuildingArgs() { Id = b.Id, SettingId = b.SettingId, X = b.X, Y = b.Y, TimeRest = timeRest });
            }
            foreach (int i in player.Home.Properties)
            {
                loadhomeArgs.Properties.Add(i);
            }
            loadhomeArgs.Name = player.Name;
            loadhomeArgs.MinX = player.Home.MinX;
            loadhomeArgs.MinY = player.Home.MinY;
            loadhomeArgs.MaxX = player.Home.MaxX;
            loadhomeArgs.MaxY = player.Home.MaxY;
            loadhomeArgs.HomeLevel = player.Home.Level;
            loadhomeArgs.PlayerSex = player.Sex;

            session.SendResponse(ID, loadhomeArgs);
        }
    }
}
