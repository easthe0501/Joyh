using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class CopyMember
    {
        public CopyMember(Copy instance, Player player)
        {
            Instance = instance;
            Player = player;
        }

        public Copy Instance { get; private set; }
        public Player Player { get; private set; }
        public int Pos { get; set; }

        public CopyGrid CurrentGrid
        {
            get { return Instance.Grids[Pos]; }
        }
    }
}
