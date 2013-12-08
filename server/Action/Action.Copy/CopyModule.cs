using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using System.Diagnostics;
using Action.Utility;

namespace Action.Copy
{
    [Export(typeof(IGameModule))]
    public class CopyModule : GameModule, ICopyModule
    {
        public override void Load(GameWorld world)
        {
            foreach (var copySetting in APF.Settings.Copies.All)
            {
                var strCopyId = copySetting.Id.ToString();
                Trace.Assert(copySetting.MinPlayers > 0 && copySetting.MinPlayers <= copySetting.MaxPlayers, strCopyId);
                Trace.Assert(copySetting.NextId == 0 || APF.Settings.Copies.Find(copySetting.NextId) != null, strCopyId);
                Trace.Assert(copySetting.EnterRequirement != null, strCopyId);
                Trace.Assert(copySetting.EnterRequirement.PlayerLevel >= 0, strCopyId);
                Trace.Assert(copySetting.EnterRequirement.GuildLevel >= 0, strCopyId);
                Trace.Assert(copySetting.StyleOptions != null, strCopyId);
                Trace.Assert(copySetting.StyleOptions.Money != null && copySetting.StyleOptions.Money.Length > 0, strCopyId);
                Trace.Assert(copySetting.StyleOptions.Material != null && copySetting.StyleOptions.Material.Length > 0, strCopyId);
                Trace.Assert(copySetting.StyleOptions.Box != null && copySetting.StyleOptions.Box.Length > 0, strCopyId);
                Trace.Assert(copySetting.StyleOptions.Monster != null && copySetting.StyleOptions.Monster.Length > 0, strCopyId);
                //Trace.Assert(copySetting.StyleOptions.Meeting != null && copySetting.StyleOptions.Meeting.Length > 1, strCopyId);
                Trace.Assert(copySetting.StyleOptions.Card != null && copySetting.StyleOptions.Card.Where(c=>c.Rate>=100).Count() > 5, strCopyId);


                copySetting.StyleOptions.Money.Assert(strCopyId);
                copySetting.StyleOptions.Material.Assert(strCopyId);
                copySetting.StyleOptions.Box.Assert(strCopyId);
                copySetting.StyleOptions.Card.Assert(strCopyId);

                foreach (var monsterId in copySetting.StyleOptions.Monster)
                    Trace.Assert(APF.Settings.Battles.Find(monsterId) != null, strCopyId);
                Trace.Assert(APF.Settings.Battles.Find(copySetting.StyleOptions.Boss) != null, strCopyId);

                //Trace.Assert(copySetting.Styles != null && copySetting.Styles.Length > 0, strCopyId);
                //foreach (var style in copySetting.Styles)
                //    Trace.Assert(NumberHelper.Between(style, 0, 6), strCopyId);
                foreach (var itemId in copySetting.PassPrize.Prize.Items)
                    Trace.Assert(APF.Settings.Items.Find(itemId.Id) != null, strCopyId);
                foreach (var card in copySetting.StyleOptions.Card)
                {
                    if (card.Type == CardType.I)
                    {
                        foreach (var i in card.Prize.Items)
                            Trace.Assert(APF.Settings.Items.Find(i.Id) != null, strCopyId);
                    }
                }
                foreach (var it in copySetting.StyleOptions.Material)
                    foreach (var item in it.Items)
                        Trace.Assert(APF.Settings.Items.Find(item.Id) != null, strCopyId);
                foreach (var boxitem in copySetting.StyleOptions.Box)
                    foreach (var item in boxitem.Items)
                        Trace.Assert(APF.Settings.Items.Find(item.Id) != null, strCopyId);
            }
        }

        public override void CreateRole(GamePlayer player)
        {
            //临时给副本权限
            var copies = player.Data.AsDbPlayer().Permission.Copies;
            copies.Add(110001);
        }

        public override void LeaveGame(GamePlayer player)
        {
            player.Data.AsDbPlayer().CurrentCopy = null;
        }

        public override void LevelUp(GamePlayer player, int oldLevel)
        {
            //var dbPlayer = player.Data.AsDbPlayer();
            //var lastCopy = APF.Settings.Copies.GetItem(dbPlayer.Permission.Copies.LastOrDefault());
            //if (lastCopy != null)
            //{
            //    var nextCopy = lastCopy.Next();
            //    if (nextCopy != null)
            //        nextCopy.Unlock(dbPlayer);
            //}
        }
    }
}
