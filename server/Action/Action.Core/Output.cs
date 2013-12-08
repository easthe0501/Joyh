using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Core
{
    public interface IOutput
    {
        void WriteLine(ConsoleColor color, string text);
    }

    public class ConsoleOutput : IOutput
    {
        public void WriteLine(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
