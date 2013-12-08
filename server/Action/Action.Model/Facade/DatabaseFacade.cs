using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Action.Utility;
using Action.DataAccess;
using Action.Core;
using MongoDB.Driver.Builders;
using System.Collections.Concurrent;

namespace Action.Model
{
    public class DatabaseFacade
    {
        private MongoDataAccess _mongo;
        public MongoDataAccess Mongo
        {
            get { return _mongo; }
        }

        private MongoCollection<Player> _playerCollection;
        private MongoCollection<Account> _accountCollection;
        private MongoCollection<World> _worldCollection;

        //private ConcurrentDictionary<string, Account> _accountCache;
        //private ConcurrentDictionary<string, Player> _playerCache;

        public void Init()
        {
            _mongo = new MongoDataAccess();
            _mongo.Ping();
            PrepareDatabase();
            PrepareCache();
        }

        private void PrepareDatabase()
        {
            var gameDb = _mongo.GameDB;
            if (!gameDb.CollectionExists(Player.Cls))
                gameDb.CreateCollection(Player.Cls);
            _playerCollection = gameDb.GetCollection<Player>();
            _playerCollection.EnsureIndex(IndexKeys<Player>.Ascending(p => p.Name),
                IndexOptions.SetUnique(true));

            if (!gameDb.CollectionExists(Account.Cls))
                gameDb.CreateCollection(Account.Cls);
            _accountCollection = gameDb.GetCollection<Account>();
            _accountCollection.EnsureIndex(IndexKeys<Account>.Ascending(a => a.Key),
                IndexOptions.SetUnique(true));
            _accountCollection.EnsureIndex(IndexKeys<Account>.Ascending(a => a.Name),
                IndexOptions.SetUnique(true));

            if (!gameDb.CollectionExists(World.Cls))
                gameDb.CreateCollection(World.Cls);
            _worldCollection = gameDb.GetCollection<World>();
        }

        private void PrepareCache()
        {
        }

        public bool ContainsPlayer(string name)
        {
            return _playerCollection.AsQueryable().SingleOrDefault(p => p.Name == name) != null;
        }

        public IQueryable<Player> LoadPlayers()
        {
            return _playerCollection.AsQueryable();
        }

        public Player LoadPlayer(string name)
        {
            var player = LoadPlayers().SingleOrDefault(p => p.Name == name);
            if (player != null)
                player.Load();
            return player;
        }

        public void SavePlayer(Player player)
        {
            player.Version = APF.Version;
            _playerCollection.Save(player);
        }

        public IQueryable<Account> LoadAccounts()
        {
            return _accountCollection.AsQueryable();
        }

        public Account LoadAccount(string name)
        {
            var account = LoadAccounts().SingleOrDefault(p => p.Name == name);
            if(account != null)
                account.Load();
            return account;
        }

        public void SaveAccount(Account account)
        {
            account.Version = APF.Version;
            _accountCollection.Save(account);
        }

        public World LoadWorld()
        {
            var world = _worldCollection.FindOne();
            if (world == null)
            {
                world = new World();
                world.Init();
            }
            else
                world.Load();
            return world;
        }

        public void SaveWorld(World world)
        {
            world.Version = APF.Version;
            _worldCollection.Save(world);
        }
    }
}
