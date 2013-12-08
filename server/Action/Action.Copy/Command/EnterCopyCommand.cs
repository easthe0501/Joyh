using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.EnterCopy)]
    public class EnterCopyCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var copySetting = APF.Settings.Copies.Find(args);
            if (copySetting == null)
                return;

            //验证是否已在副本中
            var player = session.Player.Data.AsDbPlayer();
            if (player.CurrentCopy != null)
            {
                session.SendError(ErrorCode.AlreadyInCopy);
                return;
            }

            //验证是否有权限
            if (!player.Permission.Copies.Contains(args))
            {
                session.SendError(ErrorCode.EnterCopyRightNotEngouth);
                return;
            }

            //验证等级是否达标
            if (player.Level < copySetting.EnterRequirement.PlayerLevel)
            {
                session.SendError(ErrorCode.LevelNotEnough);
                return;
            }

            //对帮派副本进行额外验证
            if (copySetting.Grade == CopyGrade.Guild)
            {
                var guild = session.Player.GetGuild();
                if (guild == null)
                {
                    session.SendError(ErrorCode.NotInGuild);
                    return;
                }
                if (guild.Level < copySetting.EnterRequirement.GuildLevel)
                {
                    session.SendError(ErrorCode.GuildLevelNotEnough);
                    return;
                }
            }

            //验证前置任务是否完成
            var taskId = copySetting.EnterRequirement.TaskId;
            if (taskId != 0 && !player.ClosedTasks.Contains(taskId))
            {
                session.SendError(ErrorCode.TaskNotFinished);
                return;
            }

            //验证体力是否足够
            if (player.Energy < copySetting.EnterConsumable.Energy)
            {
                session.SendError(ErrorCode.EnergyNotEnough);
                return;
            }

            ////验证材料是否足够并消耗
            //var materials = copySetting.EnterConsumable.Materials;
            //if (materials != null && materials.Length > 0 && !session.Server.ModuleFactory
            //    .Module<IBagModule>().ConsumeItem(session, materials))
            //    return;

            //创建副本，暂不扣除体力，等移动后扣除
            player.CurrentCopy = new Model.Copy(copySetting, player);

            //副本事件消息
            var msg1 = new CopyGridArgsArray() { CopyId = copySetting.Id };
            for (int i = 0; i < copySetting.Styles.Length; i++)
            {
                var style = (GridStyle)copySetting.Styles[i];
                msg1.Grids.Add(player.CurrentCopy.Grids[i].ToArgs());
            }
            session.SendResponse(ID, msg1);

            ////成员位置消息(暂时取消)
            //var msg2 = new MemberPosCollection();
            //msg2.Items.Add(new MemberPosArgs()
            //{
            //    Player = player.Name,
            //    Pos = player.CurrentCopy.GetMemberByName(player.Name).Pos
            //});
            //session.SendResponse((int)CommandEnum.NoteMemberPos, msg2);

            //高级副本自动移除权限(取消)
            if (copySetting.Grade == CopyGrade.Advanced)
                player.Permission.Copies.Remove(args);
        }
    }
}
