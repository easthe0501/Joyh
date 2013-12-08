using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using SuperSocket.Common;
using Action.Engine;
using Action.DataAccess;
using Action.Model;
using MongoDB.Bson;
using Action.Core;
using MongoDB.Driver;

namespace Action.Log
{
    public class MongoLogger : ILogger
    {
        private MongoDatabase _db;
        private LogHelper _logHelper = null;

        public MongoLogger()
            : this("Action")
        { }

        public MongoLogger(string name)
        {
            _logHelper = new LogHelper(this.Name = name);
            _db = APF.Database.Mongo.LogDB;
        }

        public void LogDebug(string message)
        {
            _db.GetCollection<LOG.Debug>().Insert(
                new LOG.Debug()
                {
                    Target = Name,
                    CreateTime = BsonDateTime.Create(DateTime.Now),
                    Content = _logHelper.BuildLogText(message)
                });
        }

        public void LogError(Exception e)
        {
            _db.GetCollection<LOG.Error>().Insert(
                new LOG.Error()
                {
                    Target = Name,
                    CreateTime = BsonDateTime.Create(DateTime.Now),
                    Content = _logHelper.BuildLogText(e)
                });
        }

        public void LogError(string message)
        {
            _db.GetCollection<LOG.Error>().Insert(
                new LOG.Error()
                {
                    Target = Name,
                    CreateTime = BsonDateTime.Create(DateTime.Now),
                    Content = _logHelper.BuildLogText(message)
                });
        }

        public void LogError(string title, Exception e)
        {
            _db.GetCollection<LOG.Error>().Insert(
                new LOG.Error()
                {
                    Target = Name,
                    CreateTime = BsonDateTime.Create(DateTime.Now),
                    Content = _logHelper.BuildLogText(title, e)
                });
        }

        public void LogInfo(string message)
        {
            _db.GetCollection<LOG.Info>().Insert(
                new LOG.Info()
                {
                    Target = Name,
                    CreateTime = BsonDateTime.Create(DateTime.Now),
                    Content = _logHelper.BuildLogText(message)
                });
        }

        public void LogPerf(string message)
        {
            _db.GetCollection<LOG.Pref>().Insert(
                new LOG.Pref()
                {
                    Target = Name,
                    CreateTime = BsonDateTime.Create(DateTime.Now),
                    Content = _logHelper.BuildLogText(message)
                });
        }

        public string Name { get; private set; }
    }
}
