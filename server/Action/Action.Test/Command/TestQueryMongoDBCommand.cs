//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;
//using Action.Model;
//using Action.DataAccess;
//using MongoDB.Driver.Linq;
//using System.ComponentModel.Composition;
//using MongoDB.Driver.Builders;

//namespace Action.Test.Command
//{
//    //[GameCommand(983)]
//    public class TestQueryMongoDBCommand : GameCommand<string>
//    {
//        [Import]
//        private MongoDataAccess _mongo = null;

//        protected override bool Ready(GameSession session, string args)
//        {
//            return true;
//        }

//        protected override void Run(GameSession session, string args)
//        {
//            var db = _mongo.GameDB;
//            var player = db.GetCollection<Player>().AsQueryable().Single(p => p.Name == args);
//            session.SendResponse(983, player.Account);

//            //db.GetCollection<Player>().GeoHaystackSearch(10.0d, 10.0d, GeoHaystackSearchOptions<Player>.Null);
//        }
//    }
//}
