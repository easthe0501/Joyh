using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Action.Core;

namespace Action.Engine
{
    public class GameProfile
    {
        public GameProfile()
        {
            TimerHelper.Loop(UpdateSlowCommands, 10000);
        }

        [ReadOnly(true), Category("程序"), DisplayName("采样时间")]
        public string CollectTime { get; set; }

        [ReadOnly(true), Category("程序"), DisplayName("运行时长")]
        public string RunTime { get; set; }

        [ReadOnly(true), Category("程序"), DisplayName("CPU使用率")]
        public string CpuUsage { get; set; }

        [ReadOnly(true), Category("程序"), DisplayName("内存占用")]
        public string WorkingSet { get; set; }

        [ReadOnly(true), Category("程序"), DisplayName("总线程数")]
        public int TotalThreadCount { get; set; }

        [ReadOnly(true), Category("线程"), DisplayName("可用辅助线程")]
        public int AvailableCompletionPortThreads { get; set; }

        [ReadOnly(true), Category("线程"), DisplayName("可用工作线程")]
        public int AvailableWorkingThreads { get; set; }

        [ReadOnly(true), Category("线程"), DisplayName("最大辅助线程")]
        public int MaxCompletionPortThreads { get; set; }

        [ReadOnly(true), Category("线程"), DisplayName("最大工作线程")]
        public int MaxWorkingThreads { get; set; }

        [ReadOnly(true), Category("游戏"), DisplayName("当前连接")]
        public int Connections { get; set; }

        [ReadOnly(true), Category("游戏"), DisplayName("当前登陆")]
        public int LoginPlayers { get; set; }

        [ReadOnly(true), Category("游戏"), DisplayName("已处理请求")]
        public string HandledCommands { get; set; }

        [ReadOnly(true), Category("游戏"), DisplayName("请求处理频率")]
        public int HandlingRate { get; set; }

        [ReadOnly(true), Category("队列"), DisplayName("通用队列任务数")]
        public int MainQueueTasks { get; set; }

        [ReadOnly(true), Category("队列"), DisplayName("通用队列是否忙碌")]
        public bool MainQueueBusy{ get; set; }

        [ReadOnly(true), Category("队列"), DisplayName("聊天队列任务数")]
        public int ChatQueueTasks { get; set; }

        [ReadOnly(true), Category("队列"), DisplayName("聊天队列是否忙碌")]
        public bool ChatQueueBusy { get; set; }

        [ReadOnly(true), Category("性能"), DisplayName("处理最慢的请求1")]
        public string SlowCommand1 { get; set; }

        [ReadOnly(true), Category("性能"), DisplayName("处理最慢的请求2")]
        public string SlowCommand2 { get; set; }

        [ReadOnly(true), Category("性能"), DisplayName("处理最慢的请求3")]
        public string SlowCommand3 { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("CPU Usage: {0}%, Physical Memory Usage: {1:N}, Total Thread Count: {2}", CpuUsage, WorkingSet, TotalThreadCount));
            sb.AppendLine(string.Format("AvailableWorkingThreads: {0}, AvailableCompletionPortThreads: {1}", AvailableWorkingThreads, AvailableCompletionPortThreads));
            sb.AppendLine(string.Format("MaxWorkingThreads: {0}, MaxCompletionPortThreads: {1}", MaxWorkingThreads, MaxCompletionPortThreads));
            sb.AppendLine("---------------------------------------------------");
            return sb.ToString();
        }

        private bool _updated = false;
        private ConcurrentDictionary<int, long> _perfCommands = new ConcurrentDictionary<int, long>();

        [Browsable(false)]
        public ConcurrentDictionary<int, long> PerfCommands
        {
            get { return _perfCommands; }
        }

        public void AddCommands(Dictionary<int, long> cmds)
        {
            _updated = true;
            foreach (var key in cmds.Keys)
            {
                var oldValue = _perfCommands.GetValue(key);
                var newValue = cmds.GetValue(key);
                _perfCommands[key] = Math.Max(oldValue, newValue);
            }
        }

        private void UpdateSlowCommands()
        {
            if (_updated)
            {
                _updated = false;
                var slowCmds = _perfCommands.OrderByDescending(p => p.Value)
                    .Select(p => string.Format("[{0}]:{1}", p.Key, p.Value))
                    .ToArray();
                if (slowCmds.Length > 0)
                    SlowCommand1 = slowCmds[0];
                if (slowCmds.Length > 1)
                    SlowCommand2 = slowCmds[1];
                if (slowCmds.Length > 2)
                    SlowCommand3 = slowCmds[2];
            }
        }
    }
}
