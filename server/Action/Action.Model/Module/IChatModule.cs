using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public enum ChatChannel
    {
        World = 1300,
        Local,
        Guild,
        Private,
        GM,
        Self
    }

    public interface IChatModule : IGameModule
    {
        void WorldNoteGoodEquip(GameSession session, Equip equip);
        void WorldNoteGoodSoul(GameSession session, int soulId);
    }
}
