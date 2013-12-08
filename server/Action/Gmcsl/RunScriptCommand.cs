using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Model;
using Action.Client;

namespace Gmcsl
{
    public class RunScriptCommand : ActionCommand<ScriptVarArgs>
    {
        public override int Id
        {
            get { return 500; }
        }

        protected override void Run(ActionTcpClient client, ScriptVarArgs args)
        {
            Console.WriteLine("\n({0}){1}\n{2}\n", args.Code, (!string.IsNullOrEmpty(args.Desc) ? args.Desc : "Done"), args.Value);
            Console.Write("Gmcsl> ");
        }
    }
}
