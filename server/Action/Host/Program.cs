using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Action.Core;

namespace Host
{
    static class Program
    {
        private static MainForm _mainForm;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(_mainForm = new MainForm());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Global.Output.WriteLine(ConsoleColor.Red, e.Exception.ToString());
        }
    }
}
