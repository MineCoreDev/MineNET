using System;
using System.Linq;
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
            string[] lines = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (inputTop != -1)
            {
                int lineOverflow = 0;
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length <= 0)
                        continue;

                    Console.Write(lines[i]);

                    int leftTmp = Console.CursorLeft;
                    int topTmp = Console.CursorTop;
                    lineOverflow += topTmp - top;

                    Console.SetCursorPosition(left, top);
                    builder.Clear();
                    for (int j = 0; j < leftTmp + ((topTmp - top) * width); j++)
                    {
                        builder.Append(" ");
                    }

                    Console.Write(builder.ToString());
                }

                Console.MoveBufferArea(0, inputTop, width, top - inputTop + 1, 0,
                    inputTop + lines.Length + lineOverflow);
                Console.SetCursorPosition(0, inputTop);
                Console.Write(msg);
                Console.SetCursorPosition(left, inputTop + top - inputTop + lines.Length + lineOverflow);

                _input.InputStartTop = inputTop + lines.Length + lineOverflow;
            }
            else
            {
                Console.Write(msg);
            }
        }
    }
}