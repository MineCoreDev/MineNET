using System;
using System.Text;
using NLog;
using NLog.Layouts;
using NLog.Targets;

namespace MineNET.IO
{
    public class MineNetConsoleTarget : TargetWithLayout
    {
        private Input _input;
        private Encoding _utf8 = Encoding.UTF8;

        public MineNetConsoleTarget(Input input)
        {
            _input = input;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            string msg = Layout.Render(logEvent);
            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            int inputTop = _input.InputStartTop;
            int width = Console.BufferWidth;
            string[] lines = msg.Split(Environment.NewLine.ToCharArray());

            if (inputTop != 0)
            {
                int lineOverflow = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    lineOverflow += _utf8.GetBytes(lines[i]).Length / (width + 1);
                }

                Console.MoveBufferArea(0, inputTop, width, top - inputTop + 1, 0,
                    inputTop + lines.Length + lineOverflow);
                Console.SetCursorPosition(0, inputTop);
                Console.WriteLine(msg);
                Console.SetCursorPosition(left, inputTop + top - inputTop + lines.Length + lineOverflow);

                _input.InputStartTop = inputTop + lines.Length + lineOverflow;
            }
            else
            {
                Console.WriteLine(msg);
            }
        }
    }
}