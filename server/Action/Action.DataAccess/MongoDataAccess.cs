using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Configuration;
using System.Diagnostics;

namespace Action.DataAccess
{
    public class MongoDataAccess
    {
        public MongoDataAccess()
        {
            _gameDB = CreateDatabase("gamedb");
            _logDB = CreateDatabase("logdb");
        }

        private MongoDatabase CreateDatabase(string connectionKey)
        {
            var connection = ConfigurationManager.ConnectionStrings[connectionKey];
            Trace.Assert(connection != null);
            return MongoDatabase.Create(connection.ConnectionString);
        }

        private MongoDatabase _gameDB;
        /// <summary>
        /// 游戏数据库
        /// </summary>
        public MongoDatabase GameDB
        {
            get { return _gameDB; }
        }

        private MongoDatabase _logDB;
        /// <summary>
        /// 获取日志库，权限：query,insert
        /// </summary>
        public MongoDatabase LogDB
        {
            get { return _logDB;}
        }

        /// <summary>
        /// 测试数据库是否可访问
        /// </summary>
        /// <returns></returns>
        public void Ping()
        {
            _gameDB.Server.Ping();
            _logDB.Server.Ping();
        }
    }
}
