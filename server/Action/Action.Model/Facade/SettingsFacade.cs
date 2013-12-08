using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Action.Core;

namespace Action.Model
{
    public class SettingsFacade
    {
        public void Init()
        {
            _commands = new JsonSettings<CommandSetting>();
            _script = JsonSetting.Deserialize<ScriptSetting>();
            _role = JsonSetting.Deserialize<RoleSetting>();
            _home = JsonSetting.Deserialize<HomeSetting>();
            _buildings = new JsonSettings<BuildingSetting>();
            _bag = JsonSetting.Deserialize<BagSetting>();
            _items = new JsonSettings<ItemSetting>();
            _equips = new JsonSettings<EquipSetting>();
            _souls = new JsonSettings<SoulSetting>();
            _chat = JsonSetting.Deserialize<ChatSetting>();
            _heros = new JsonSettings<HeroSetting>();
            _jobs = new JsonSettings<JobSetting>();
            _skills = new JsonSettings<SkillSetting>();
            _levels = new JsonSettings<LevelSetting>();
            _equipStrenthens = new JsonSettings<EquipStrenthenSetting>();
            _equipCompounds = new JsonSettings<EquipCompoundSetting>();
            _copies = new JsonSettings<CopySetting>();
            _copyTools = new JsonSettings<CopyToolSetting>();
            _battles = new JsonSettings<BattleSetting>();
            _monsterJobs = new JsonSettings<MonsterJobSetting>();
            _soulHunts = new JsonSettings<SoulHuntSetting>();
            _tasks = new JsonSettings<TaskSetting>();
            _randomTasks = new JsonSettings<RandomTaskSetting>();
            _guild = JsonSetting.Deserialize<GuildSetting>();
            _guildContributes = new JsonSettings<GuildContributionSetting>();
            _goods = new JsonSettings<GoodsSetting>();
            _eatExps = new JsonSettings<EatExpSetting>();
            _vips = new JsonSettings<VipSetting>();
            _guildLevels = new JsonSettings<GuildLevelSetting>();
            _arena = JsonSetting.Deserialize<ArenaSetting>();
            _tencentApi = JsonSetting.Deserialize<TencentApiSetting>();
            _tencentPays = new JsonSettings<TencentPaySetting>();
            _updateHomes = new JsonSettings<UpdateHomeSetting>();
            _signPrize = JsonSetting.Deserialize<SignPrizeSetting>();
            _testUsers = new JsonSettings<TestUserSetting>();
            _fixedPrizes = new JsonSettings<FixedPrizeSetting>();

            foreach (var battle in _battles.All)
                battle.Init();
            foreach (var task in _tasks.All)
                task.Init();
            foreach (var task in _randomTasks.All)
                task.Init();
        }

        public string GetFullPath(string path)
        {
            return Path.Combine(Global.ResDir, path);
        }

        private JsonSettings<CommandSetting> _commands;
        public JsonSettings<CommandSetting> Commands
        {
            get { return _commands; }
        }

        private ScriptSetting _script;
        public ScriptSetting Script
        {
            get { return _script; }
        }

        private RoleSetting _role;
        public RoleSetting Role
        {
            get { return _role; }
        }

        private HomeSetting _home;
        public HomeSetting Home
        {
            get { return _home; }
        }

        private JsonSettings<BuildingSetting> _buildings;
        public JsonSettings<BuildingSetting> Buildings
        {
            get { return _buildings; }
        }

        private BagSetting _bag;
        public BagSetting Bag
        {
            get { return _bag; }
        }

        private JsonSettings<ItemSetting> _items;
        public JsonSettings<ItemSetting> Items
        {
            get { return _items; }
        }

        private JsonSettings<EquipSetting> _equips;
        public JsonSettings<EquipSetting> Equips
        {
            get { return _equips; }
        }

        private JsonSettings<SoulSetting> _souls;
        public JsonSettings<SoulSetting> Souls
        {
            get { return _souls; }
        }

        private ChatSetting _chat;
        public ChatSetting Chat
        {
            get { return _chat; }
        }

        private JsonSettings<HeroSetting> _heros;
        public JsonSettings<HeroSetting> Heros
        {
            get { return _heros; }
        }

        private JsonSettings<JobSetting> _jobs;
        public JsonSettings<JobSetting> Jobs
        {
            get { return _jobs; }
        }

        private JsonSettings<SkillSetting> _skills;
        public JsonSettings<SkillSetting> Skills
        {
            get { return _skills; }
        }

        private JsonSettings<LevelSetting> _levels;
        public JsonSettings<LevelSetting> Levels
        {
            get { return _levels; }
        }

        private JsonSettings<EquipStrenthenSetting> _equipStrenthens;
        public JsonSettings<EquipStrenthenSetting> EquipStrenthens
        {
            get { return _equipStrenthens; }
        }

        private JsonSettings<EquipCompoundSetting> _equipCompounds;
        public JsonSettings<EquipCompoundSetting> EquipCompounds
        {
            get { return _equipCompounds; }
        }

        private JsonSettings<CopySetting> _copies;
        public JsonSettings<CopySetting> Copies
        {
            get { return _copies; }
        }

        private JsonSettings<CopyToolSetting> _copyTools;
        public JsonSettings<CopyToolSetting> CopyTools
        {
            get { return _copyTools; }
        }

        private JsonSettings<BattleSetting> _battles;
        public JsonSettings<BattleSetting> Battles
        {
            get { return _battles; }
        }

        private JsonSettings<MonsterJobSetting> _monsterJobs;
        public JsonSettings<MonsterJobSetting> MonsterJobs
        {
            get { return _monsterJobs; }
        }

        private JsonSettings<SoulHuntSetting> _soulHunts;
        public JsonSettings<SoulHuntSetting> SoulHunts
        {
            get { return _soulHunts; }
        }

        private JsonSettings<TaskSetting> _tasks;
        public JsonSettings<TaskSetting> Tasks
        {
            get { return _tasks; }
        }

        private JsonSettings<RandomTaskSetting> _randomTasks;
        public JsonSettings<RandomTaskSetting> RandomTasks
        {
            get { return _randomTasks; }
        }

        private GuildSetting _guild;
        public GuildSetting Guild
        {
            get { return _guild; }
        }

        private JsonSettings<GuildContributionSetting> _guildContributes;
        public JsonSettings<GuildContributionSetting> GuildContributes
        {
            get { return _guildContributes; }
        }

        private JsonSettings<GoodsSetting> _goods;
        public JsonSettings<GoodsSetting> Goods
        {
            get { return _goods; }
        }

        private JsonSettings<EatExpSetting> _eatExps;
        public JsonSettings<EatExpSetting> EatExps
        {
            get { return _eatExps; }
        }

        private JsonSettings<VipSetting> _vips;
        public JsonSettings<VipSetting> Vips
        {
            get { return _vips; }
        }

        private JsonSettings<GuildLevelSetting> _guildLevels;
        public JsonSettings<GuildLevelSetting> GuildLevels
        {
            get { return _guildLevels; }
        }

        private ArenaSetting _arena;
        public ArenaSetting Arena
        {
            get { return _arena; }
        }

        private TencentApiSetting _tencentApi;
        public TencentApiSetting TencentApi
        {
            get { return _tencentApi; }
        }

        private JsonSettings<TencentPaySetting> _tencentPays;
        public JsonSettings<TencentPaySetting> TencentPays
        {
            get { return _tencentPays; }
        }

        private JsonSettings<UpdateHomeSetting> _updateHomes;
        public JsonSettings<UpdateHomeSetting> UpdateHomes
        {
            get { return _updateHomes; }
        }

        private SignPrizeSetting _signPrize;
        public SignPrizeSetting SignPrize
        {
            get { return _signPrize; }
        }

        private JsonSettings<TestUserSetting> _testUsers;
        public JsonSettings<TestUserSetting> TestUsers
        {
            get { return _testUsers; }
        }

        private JsonSettings<FixedPrizeSetting> _fixedPrizes;
        public JsonSettings<FixedPrizeSetting> FixedPrizes
        {
            get { return _fixedPrizes; }
        }
    }
}
