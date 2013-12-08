using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using SuperSocket.SocketEngine.Configuration;
using SuperSocket.SocketEngine;
using SuperSocket.Common;
using Action.Model;
using Action.Engine;
using Action.Utility;
using Action.Log;
using Action.Core;

namespace Host
{
    public partial class MainForm : Form
    {
        private Timer _startTimer = new Timer();
        private Timer _watchTimer = new Timer();
        private Timer _stopTimer = new Timer();
        private bool _allowClose = false;
        private DateTime _startTime;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _startTimer.Interval = 500;
            _startTimer.Tick += _start_Tick;
            _startTimer.Start();

            _watchTimer.Interval = 1000;
            _watchTimer.Tick += _watch_Tick;

            _stopTimer.Interval = 1;
            _stopTimer.Tick += _stop_Tick;

            this.Text = string.Format("仙境之城 R{0}", Global.Version);
        }

        private void _start_Tick(object sender, EventArgs e)
        {
            _startTimer.Stop();
            _startTime = DateTime.Now;
            Global.Output = new RichTextOutput(txtConsole);
            LogUtil.Setup();
            ServerContext.LoggerFactory = new CompositeLoggerFactory(
                new ColorLoggerFactory(), new FileLoggerFactory());
            WordValidateHelper.Init();
            APF.Init(TypeHelper.CreateInstance<IBattleCalculator>(
                Global.Config.BattleCalculator));

            SocketServiceConfig serverConfig = ConfigurationManager.GetSection("socketServer") as SocketServiceConfig;
            if (!SocketServerManager.Initialize(serverConfig))
            {
                ShowError("初始化配置失败，请根据错误日志获取更多信息");
                Application.Exit();
                return;
            }
            if (!SocketServerManager.Start())
            {
                SocketServerManager.Stop();
                ShowError("启动服务失败，请根据错误日志获取更多信息");
                Application.Exit();
                return;
            }
            _watchTimer.Start();
        }

        private void _watch_Tick(object sender, EventArgs e)
        {
            var profile = ServerContext.GameServer.Profile;
            profile.RunTime = GetRunTime();
            pgPerfomance.SelectedObject = null;
            pgPerfomance.SelectedObject = profile;
        }

        private void _stop_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private string GetRunTime()
        {
            var span = DateTime.Now - _startTime;
            return string.Format("{0}天 {1}时 {2}分 {3}秒", span.Days, span.Hours, span.Minutes, span.Seconds);
        }

        private void tbtnTopMost_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }

        private void ShowError(string text)
        {
            MessageBox.Show(text, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_allowClose && MessageBox.Show("该操作将关闭整个服务器，确定要继续吗？", "退出", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
            else
            {
                _allowClose = true;
                if (ServerContext.SessionsCount > 0)
                {
                    e.Cancel = true;
                    ServerContext.Shutdown();
                    _stopTimer.Start();
                }
            }
        }
        
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _watchTimer.Stop();
                _stopTimer.Stop();
                SocketServerManager.Stop();
            }
            catch { }
        }

        private void tbtnClear_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
        }

        private void tbtnReloadCfg_Click(object sender, EventArgs e)
        {
            APF.Settings.Init();
            ServerContext.GameServer.Logger.LogInfo("配置已刷新");
        }
    }
}
