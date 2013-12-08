using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Model
{
    public enum GridType
    {
        Empty = 0,
        Move,
        Battle,
        Meeting,
        Prize,
        Card,
        Random
    }

    public class CopyGrid
    {
        public GridStyle Style { get; set; }
        public GridType Type { get; set; }
        public object Data { get; set; }

        /// <summary>
        /// 根据副本配置和格子样式更新格子内容
        /// </summary>
        /// <param name="setting">副本配置</param>
        /// <param name="style">格子内容</param>
        public void Update(CopySetting setting, GridStyle style)
        {
            Style = style;
            var random = APF.Random;
            switch (style)
            {
                case GridStyle.Money:
                    Type = GridType.Prize;
                    Data = setting.StyleOptions.Money[random.Range(0, setting.StyleOptions.Money.Length - 1)];
                    break;
                case GridStyle.Material:
                    Type = GridType.Prize;
                    Data = setting.StyleOptions.Material[random.Range(0, setting.StyleOptions.Material.Length - 1)];
                    break;
                case GridStyle.Box:
                    Type = GridType.Prize;
                    Data = setting.StyleOptions.Box[random.Range(0, setting.StyleOptions.Box.Length - 1)];
                    break;
                case GridStyle.Monster:
                    Type = GridType.Battle;
                    Data = setting.StyleOptions.Monster[random.Range(0, setting.StyleOptions.Monster.Length - 1)];
                    break;
                case GridStyle.Random:
                    Type = GridType.Random;
                    break;
                case GridStyle.Boss:
                    Type = GridType.Battle;
                    Data = setting.StyleOptions.Boss;
                    break;
                case GridStyle.Meeting:
                    Type = GridType.Meeting;
                    Data = setting.StyleOptions.Meeting.Randoms(2).ToArray();
                    break;
                case GridStyle.Card:
                    Type = GridType.Card;
                    Data = setting.StyleOptions.Card.Where(c => random.Percent(c.Rate)).Randoms(6).ToArray();
                    break;
            }
        }

        public CopyGridArgs ToArgs()
        {
            var args = new CopyGridArgs() { Style = Style };
            switch (Style)
            {
                case GridStyle.Money:
                    args.Data1 = (Data as Prize).Money;
                    break;
                case GridStyle.Material:
                    var prize = Data as Prize;
                    args.Data1 = prize.Items[0].Id;
                    args.Data2 = prize.Items[0].Count;
                    break;
                case GridStyle.Box:
                    args.Data1 = (Data as PrizeEx).Quality;
                    break;
                case GridStyle.Monster:
                case GridStyle.Boss:
                    args.Data1 = MyConvert.ToInt32(Data);
                    break;
            }
            return args;
        }
    }
}
