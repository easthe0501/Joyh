using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public abstract class PermissionBase
    {
        public PermissionBase()
        {
            Commands = new HashSet<int>();
            Copies = new HashSet<int>();
        }

        /// <summary>
        /// 功能权限集合
        /// </summary>
        public HashSet<int> Commands { get; private set; }

        /// <summary>
        /// 副本权限集合
        /// </summary>
        public HashSet<int> Copies { get; private set; }
    }

    public class PlayerPermission : PermissionBase
    {
        public PlayerPermission()
        {
            Heros = new HashSet<int>();
            EmbattlePoses = new HashSet<int>();
            SoulPoses = new HashSet<int>();
        }

        /// <summary>
        /// 声望伙伴集合
        /// </summary>
        public HashSet<int> Heros { get; private set; }

        /// <summary>
        /// 布阵开放位置
        /// </summary>
        public HashSet<int> EmbattlePoses { get; private set; }

        /// <summary>
        /// 战魂佩戴位置
        /// </summary>
        public HashSet<int> SoulPoses { get; private set; }
    }

    public class GuildPermission : PermissionBase
    {
    }
}
