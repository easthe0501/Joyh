using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Action.Core;
using Action.Engine;
using System.Collections;
using System.Diagnostics;
using Action.Utility;

namespace Action.Model
{
    public static class ExtensionMethods
    {
        public static World AsDbWorld(this IWorldData data)
        {
            return data as World;
        }

        public static Player AsDbPlayer(this IPlayerData data)
        {
            return data as Player;
        }

        public static PlayerSummary GetSummary(this GameWorld world, string name)
        {
            return world.Data.AsDbWorld().GetSummary(name);
        }

        public static PlayerSummary GetSummary(this GamePlayer player)
        {
            return player.World.Data.AsDbWorld().GetSummary(player.Name);
        }

        public static Guild GetGuild(this GamePlayer player)
        {
            var summary = player.GetSummary();
            var gName = summary.GuildName;
            if (string.IsNullOrEmpty(gName))
                return null;
            return summary.World.Guilds.GetValue(gName);
        }

        public static IEnumerable<GamePlayer> GetGuildMembers(this GamePlayer player)
        {
            var gName = player.GetSummary().GuildName;
            if (!string.IsNullOrEmpty(gName))
            {
                var guild = player.World.Data.AsDbWorld().Guilds.GetValue(gName);
                if (guild != null)
                {
                    foreach (var pName in guild.Members.Keys)
                    {
                        var member = player.World.GetPlayer(pName);
                        if (member != null)
                            yield return member;
                    }
                }
            }
        }

        public static MongoCollection<T> GetCollection<T>(this MongoDatabase db)
        {
            return db.GetCollection<T>(typeof(T).Name);
        }

        public static string ToLocalString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static void Destory<T>(this List<T> list, T entity) where T : Entity
        {
            list.Remove(entity);
            entity.Destory();
        }

        public static void DestoryAll<T>(this List<T> list, Predicate<T> match) where T : Entity
        {
            list.FindAll(match).ForEach(e => e.Destory());
            list.RemoveAll(match);
        }

        public static T Random<T>(this IEnumerable<T> ie)
        {
            var array = ie is T[] ? (T[])ie : ie.ToArray();
            var index = APF.Random.Range(0, array.Length - 1);
            return array[index];
        }

        public static IEnumerable<T> Randoms<T>(this IEnumerable<T> ie, int count)
        {
            var list = ie.ToList();
            while(list.Count > count)
                list.RemoveAt(APF.Random.Range(0, list.Count -1));
            return list;
        }

        public static int GetFightingCapacity(this IFighter fighter)
        {
            return fighter.CommonAttack + fighter.SkillAttack;
        }

        public static void SendError(this GameSession session, int errorCode)
        {
            session.SendResponse((int)CommandEnum.ShowError, errorCode);
        }

        public static int GetCost(this int[] costs, int index)
        {
            return index < costs.Length ? costs[index] : costs[costs.Length - 1];
        }

        public static void Assert(this Prize prize, string tip)
        {
            var items = APF.Settings.Items;
            Trace.Assert(prize.Gold >= 0 && prize.Money >= 0 && prize.Energy >= 0 
                && prize.Exp >= 0 && prize.Repute >= 0, tip);
            if (prize.Items != null)
            {
                foreach (var pair in prize.Items)
                {
                    Trace.Assert(items.Find(pair.Id) != null, tip + "-" + pair.Id.ToString());
                    Trace.Assert(pair.Count > 0);
                }
            }
        }

        public static void Assert(this IEnumerable<Prize> prizes, string tip)
        {
            foreach (var prize in prizes)
                Assert(prize, tip);
        }

        public static void Assert(this IEnumerable<Card> cards, string tip)
        {
            foreach (var card in cards)
            {
                
                Trace.Assert(card.Prize != null, tip);
                Assert(card.Prize, tip);
            }
        }

        public static void FromArgs(this TencentParams tp, TencentApiArgs args)
        {
            tp.OpenId = args.OpenId;
            tp.OpenKey = args.OpenKey;
            tp.Pf = args.Pf;
            tp.PfKey = args.PfKey;
        }

        public static TencentApiArgs ToArgs(this TencentParams tp)
        {
            return new TencentApiArgs()
            {
                OpenId = tp.OpenId,
                OpenKey = tp.OpenKey,
                Pf = tp.Pf,
                PfKey = tp.PfKey
            };
        }

        public static string ToJson<T>(this T obj, bool throwError = true) where T : class
        {
            return JsonHelper.ToJson(obj, throwError);
        }
    }
}
