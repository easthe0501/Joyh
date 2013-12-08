using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Equip.Command
{
    [GameCommand((int)CommandEnum.CompoundExpect)]
    public class CompoundExpectCommand:GameCommand<int>
    {
        protected override void Run(GameSession session, int args) 
        {
            var player = session.Player.Data.AsDbPlayer();
            var equip = player.Snapshot.Find<Model.Equip>(args);
            if (equip == null)
                return;

            int settingBefore = equip.SettingId;
            int levelBefore = equip.Level;
            int costSum = equip.StrengthenCostSum();
            int equipLevel = 1;
            //强化等级
            if (equip.EquipCompoundSetting == null)
            {
                CompoundExpectArgs noExpect = new CompoundExpectArgs()
                {
                    EquipId = 0
                };
                session.SendResponse(ID, noExpect);
                return;
            }
            int equipSettingId = equip.EquipCompoundSetting.TargetId;
            var itemSetting = APF.Settings.Items.Find(equipSettingId);
            var equipSetting = APF.Settings.Equips.Find(equipSettingId);
            int strenthenCost = APF.Settings.EquipStrenthens.Find(equipLevel).QualityCosts[itemSetting.Quality - 1];
            while (costSum > strenthenCost)
            {
                costSum -= strenthenCost;
                equipLevel += 1;
                strenthenCost = APF.Settings.EquipStrenthens.Find(equipLevel).QualityCosts[itemSetting.Quality - 1];
            }
            CompoundExpectArgs compoundExpect = new CompoundExpectArgs()
            {
                EquipId = args,
                CompoundLucky = equip.CompoundLucky,
                TargetLevel = equipLevel,
                Effect = (int)equipSetting.Buff.Effect,
                Data = equipSetting.Buff.Data * (10 + equipLevel)
            };

            session.SendResponse(ID, compoundExpect);
        }
    }
}
