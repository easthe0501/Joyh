using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;
using Action.Utility;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class DebugTaskFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//调试任务\nGamePlayer debugTask(taskId,status);"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "debugTask(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 2)
            {
                var gPlayer = (GamePlayer)data.Value;
                var player = gPlayer.Data.AsDbPlayer();
                var module = gPlayer.Session.Server.ModuleFactory.Module<ITaskModule>();
                var task = APF.Settings.Tasks.Find(MyConvert.ToInt32(args[0]));
                var status = (TaskStatus)MyConvert.ToInt32(args[1]);
                if (task != null)
                {
                    switch (status)
                    {
                        case TaskStatus.Locked:
                            module.LockTask(gPlayer, task);
                            data.Update(data.Value, 0, "Task locked.");
                            break;
                        case TaskStatus.Opened:
                            module.OpenTask(gPlayer, task);
                            data.Update(data.Value, 0, "Task opened.");
                            break;
                        case TaskStatus.Doing:
                            module.AcceptTask(gPlayer, task);
                            data.Update(data.Value, 0, "Task accpeted.");
                            break;
                        case TaskStatus.Finished:
                            module.FinishTask(gPlayer, task);
                            data.Update(data.Value, 0, "Task finished.");
                            break;
                        case TaskStatus.Closed:
                            module.CloseTask(gPlayer, task);
                            data.Update(data.Value, 0, "Task closed.");
                            break;
                        default:
                            data.Update(null, 1, "Unknow status.");
                            break;
                    }
                }
                else
                    data.Update(null, 1, "Task missing.");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("debugTask", 2, 2));
        }
    }
}
