using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.DataAccess;
using Action.Model;
using Action.Core;

namespace Action.Log
{
    [Export(typeof(ICommandLogger))]
    public class MongoCommandLogger : ICommandLogger
    {
        public MongoCommandLogger()
        {
            
        }

        public int Type
        {
            get { return 2; }
        }

        public void Save(GameSession session)
        {
            var cmdLog = new LOG.Command();
            cmdLog.Player = session.Player.Name;
            cmdLog.Import(session.LoggedCommands);
            APF.Database.Mongo.LogDB.GetCollection<LOG.Command>().Insert(cmdLog);
        }
    }
}
