using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.SwallowSoul)]
    public class SwallowSoulCommand:GameCommand<SwallowSoulArgs>
    {
        protected override void Run(GameSession session, SwallowSoulArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();

            Soul bigSoul = player.SoulWarehouse.BackSouls.SingleOrDefault(s => s.Id == args.Soul1);
            if (bigSoul == null)
                return;
            Soul smallSoul = player.SoulWarehouse.BackSouls.SingleOrDefault(s => s.Id == args.Soul2);
            if (smallSoul == null)
                return;
            
            if (bigSoul.Setting.Quality == APF.Settings.Role.RubbishSoul || smallSoul.Setting.Quality == APF.Settings.Role.RubbishSoul)
            {
                session.SendError(ErrorCode.CannotSwallowSoul);
                return;
            }

            //比较：品质，等级，经验
            if (bigSoul.CompareTo(smallSoul) > 0)
            {
                Soul tempSoul = bigSoul;
                bigSoul = smallSoul;
                smallSoul = tempSoul;
            }
            if (bigSoul.Level >= 10)
            {
                session.SendError(ErrorCode.SoulLevelLimit);
                return;
            }

            bigSoul.Exp += smallSoul.Exp;
            int nextExp = bigSoul.GetNextLevelExp();
            if (bigSoul.Exp >= nextExp)
            {
                bigSoul.Exp -= nextExp;
                bigSoul.Level += 1;
                bigSoul.Refresh();
            }
            //所有卦符移动
            var souls = player.SoulWarehouse.BackSouls.Where(s => s.Pos > smallSoul.Pos);
            foreach (var s in souls)
            {
                s.Pos -= 1;
            }
            player.SoulWarehouse.BackSouls.Destory(smallSoul);

            //返回
            session.SendResponse(ID, new SwallowSuccessArgs() { BeSwallowSoulId = smallSoul.Id, SoulId = bigSoul.Id, SoulExp = bigSoul.Exp, SoulLevel = bigSoul.Level });
        }

    }
}
