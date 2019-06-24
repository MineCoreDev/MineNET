using System;
using NLog;
using NLog.Targets;

namespace MineNET.IO
{
    public class MineNetSimpleConsoleTarget : TargetWithLayout
    {
        protected override void Write(LogEventInfo logEvent)
        {
            string msg = Layout.Render(logEvent);
            string[] split = msg.Split("§".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i++)
            {
                GetColor(split[i][0]);
                Console.Write(split[i].Remove(0, 1));
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        private void GetColor(char c)
        {
            switch (c)
            {
                case '0':
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case '1':
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;

                case '2':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;

                case '3':
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;

                case '4':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;

                case '5':
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;

                case '6':
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case '7':
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

                case '8':
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                case '9':
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case 'a':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case 'b':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case 'c':
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case 'd':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                case 'e':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case 'f':
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}