using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public static class APF
    {
        public const int Version = 0;

        private static IBattleCalculator _battleCalculator;
        public static IBattleCalculator BattleCalculator
        {
            get { return _battleCalculator; }
        }

        private static SettingsFacade _settings = new SettingsFacade();
        public static SettingsFacade Settings
        {
            get { return _settings; }
        }

        private static DatabaseFacade _database = new DatabaseFacade();
        public static DatabaseFacade Database
        {
            get { return _database; }
        }

        private static FactoryFacade _factory = new FactoryFacade();
        public static FactoryFacade Factory
        {
            get { return _factory; }
        }

        private static RandomFacade _random = new RandomFacade();
        public static RandomFacade Random
        {
            get { return _random; }
        }

        private static CommonFacade _common = new CommonFacade();
        public static CommonFacade Common
        {
            get { return _common; }
        }

        public static void Init(IBattleCalculator calc)
        {
            _battleCalculator = calc;
            _settings.Init();
            _database.Init();
            _random.Init();
        }

        public static Player LoadPlayer(GamePlayer self, string name)        
        {
            Player dbSelf = self.Data.AsDbPlayer();

            //1：判断是否当前用户
            if (string.IsNullOrEmpty(name) || dbSelf.Name == name)
               return dbSelf;

            //2：从在线用户中查找
            var other = self.World.GetPlayer(name);
            if (other != null)
                return other.Data.AsDbPlayer();

            //3：从最近关注缓存中查找
            var dbOther = self.Data.AsDbPlayer().Lookup;
            if (dbOther != null && dbOther.Name == name)
                return dbOther;

            //4：从数据库查找
            dbOther = _database.LoadPlayer(name);
            if (dbOther != null)
            {
                dbOther.Load();
                dbSelf.Lookup = dbOther;
            }
            return dbOther;
        }

        public static bool ContainsPlayer(GamePlayer self, string name)
        {
            return self.World.Data.AsDbWorld().ContainsSummary(name);
        }
    }
}
