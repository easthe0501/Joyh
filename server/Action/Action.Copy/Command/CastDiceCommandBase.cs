using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    public abstract class CastDiceCommandBase : GameCommand
    {
        private IdCountPair[] _materials;
        public CastDiceCommandBase()
        {
            var tool = APF.Settings.CopyTools.Find(ID);
            if (tool != null)
                _materials = new IdCountPair[] { new IdCountPair() { Id = tool.ItemId, Count = 1 } };
        }

        protected override void Run(GameSession session)
        {
            //if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
            //{
            //    session.SendResponse((int)CommandEnum.CastDiceError);
            //    return;
            //}

            var member = CopyHelper.GetMember(session);
            if (member != null)
            {
                if (_materials == null || session.Server.ModuleFactory.Module<IBagModule>()
                    .ConsumeItem(session, _materials))
                {
                    var dice = GetDice(session);
                    if (dice > 0)
                    {
                        var action = CopyHelper.CastMove(session, member, dice);
                        if(action != null)
                            session.SendResponse(ID, action);
                    }

                    //任务事件
                    var tool = APF.Settings.CopyTools.Find(ID);
                    if (tool != null)
                    {
                        session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                            member.Player, TaskType.UseItem, tool.ItemId);
                    }
                }
            }
        }

        protected abstract int GetDice(GameSession session);
    }

    public abstract class CastDiceCommandBase<T> : GameCommand<T>
    {
        private IdCountPair[] _materials;
        public CastDiceCommandBase()
        {
            var tool = APF.Settings.CopyTools.Find(ID);
            if (tool != null)
                _materials = new IdCountPair[] { new IdCountPair() { Id = tool.ItemId, Count = 1 } };
        }

        protected override void Run(GameSession session, T args)
        {
            //if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
            //{
            //    session.SendResponse((int)CommandEnum.CastDiceError);
            //    return;
            //}

            var member = CopyHelper.GetMember(session);
            if (member != null)
            {
                if (_materials == null || session.Server.ModuleFactory.Module<IBagModule>()
                    .ConsumeItem(session, _materials))
                {
                    var dice = GetDice(session, args);
                    if (dice > 0)
                    {
                        var action = CopyHelper.CastMove(session, member, dice);
                        if(action != null)
                            session.SendResponse(ID, action);
                    }

                    //任务事件
                    var tool = APF.Settings.CopyTools.Find(ID);
                    if (tool != null)
                    {
                        session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                            member.Player, TaskType.UseItem, tool.ItemId);
                    }
                }
            }
        }

        protected abstract int GetDice(GameSession session, T args);
    }
}
