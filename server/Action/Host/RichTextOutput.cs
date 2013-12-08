using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Action.Core;
using System.Diagnostics;

namespace Host
{
    public class RichTextOutput : IOutput
    {
        private object _locker = new object();
        private RichTextBox _textbox;
        private Callback<ConsoleColor, string> _callback;
        private Color[] _colors = new Color[]
        {
            Color.Black, Color.DarkBlue, Color.DarkGreen, Color.DarkCyan,
            Color.DarkRed, Color.DarkMagenta, Color.Gold, Color.Gray,
            Color.DarkGray, Color.Blue, Color.LawnGreen, Color.Cyan,
            Color.Red, Color.Magenta, Color.Yellow, Color.White
        };

        public RichTextOutput(RichTextBox tb)
        {
            _textbox = tb;
            _callback = _WriteLine;
        }

        private Color ToColor(ConsoleColor color)
        {
            return _colors[(int)color];
        }

        public void WriteLine(ConsoleColor color, string text)
        {
            _textbox.Invoke(_callback, color, text);
        }

        private void _WriteLine(ConsoleColor color, string text)
        {
            if (_textbox.Lines.Length > Global.Config.ConsoleLines)
                _textbox.Clear();
            var clr = ToColor(color);
            _textbox.SelectionColor = clr;
            _textbox.AppendText(text + "\n");
            _textbox.Select(_textbox.TextLength + 1, 1);
            _textbox.ScrollToCaret();
            _textbox.SelectionColor = clr;
        }
    }
}
