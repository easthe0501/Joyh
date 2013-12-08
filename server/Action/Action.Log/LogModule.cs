using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Action.Engine;
using Action.Model;
using Action.DataAccess;
using System.IO;
using Action.Core;

namespace Action.Log
{
    [Export(typeof(IGameModule))]
    public class LogModule : GameModule
    {
        private void PrepareDirectory(string cls)
        {
            var dir = Path.Combine(Global.LogDir, cls);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        private void PrepareDatabase(MongoDatabase db, string cls, string idx = null)
        {
            if (!db.CollectionExists(cls))
                db.CreateCollection(cls, CollectionOptions.SetCapped(true)
                    .SetMaxSize(short.MaxValue).SetMaxDocuments(1000));
            if (idx != null)
            {
                var collection = db.GetCollection(cls);
                collection.EnsureIndex(IndexKeys<LOG.Command>.Ascending(r => r.Player));
            }
        }

        public override void Load(GameWorld world)
        {
            var mongo = APF.Database.Mongo;
            PrepareDirectory(LOG.Command.Cls);
            PrepareDatabase(mongo.LogDB, LOG.Debug.Cls);
            PrepareDatabase(mongo.LogDB, LOG.Error.Cls);
            PrepareDatabase(mongo.LogDB, LOG.Info.Cls);
            PrepareDatabase(mongo.LogDB, LOG.Pref.Cls);
            PrepareDatabase(mongo.LogDB, LOG.Command.Cls, "Player");
        }
    }
}
