using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class Card
    {
        public Card()
        {
            Rate = 100;
        }

        public CardType Type { get; set; }
        public string Data { get; set; }
        public Prize Prize { get; set; }
        public int Quality { get; set; }
        public int Rate { get; set; }

        public CardArgs ToArgs()
        {
            return new CardArgs() { Quality = Quality, Type = Type, Data = Data };
        }
    }

    public class CardProcess
    {
        public int TryTimes { get; set; }
        public Card[] Cards { get; set; }
    }
}
