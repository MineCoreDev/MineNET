using System;
using System.Collections.Generic;

namespace MineNET.Utils
{
    public static class Logger
    {
        const int WINDOW_X = 100;
        const int WINDOW_Y = 25;

        internal class LoggerInfo
        {
            public string text;
            public LoggerLevel level;
        }

        internal enum LoggerLevel
        {
            Log,
            Info,
            Warning,
            Error,
            Fatal
        }

        static bool useGUI = false;
        public static bool UseGUI
        {
            get
            {
                return useGUI;
            }

            set
            {
                useGUI = value;
            }
        }

        static Queue<LoggerInfo> loggerTexts = new Queue<LoggerInfo>();

        internal static void Init()
        {
            if (!useGUI)
            {
                Console.SetWindowSize(WINDOW_X, WINDOW_Y);
                Console.SetBufferSize(WINDOW_X, WINDOW_Y);

                int bit = 64;
                if (BitConverter.IsLittleEndian)
                {
                    bit = 86;
                }

                Console.Title = $"MineNET x{bit}";
            }
        }

        /*public static void Trace()
        {
            string text = "";
            StackTrace st = new StackTrace();
            foreach (StackFrame sf in st.GetFrames())
            {
                text += sf.ToString() + "\n";
            }
            Log(text);
        }*/

        public static void Log(string msg, params object[] formats)
        {
            string f = string.Format(msg, formats);
            Log(f);
        }

        public static void Log(string msg)
        {
            string text = $"§7[Log][{CreateTime()}]{msg}";
            AddLogText(text, LoggerLevel.Log);
        }

        

        public static void Info(string msg, params object[] formats)
        {
            string f = string.Format(msg, formats);
            Info(f);
        }

        public static void Info(string msg)
        {
            string text = $"§f[Info][{CreateTime()}]{msg}";
            AddLogText(text);
        }

        public static void Warning(string msg, params object[] formats)
        {
            string f = string.Format(msg, formats);
            Warning(f);
        }

        public static void Warning(string msg)
        {
            string text = $"§e[Warning][{CreateTime()}]{msg}";
            AddLogText(text, LoggerLevel.Warning);

        }

        public static void Error(string msg, params object[] formats)
        {
            string f = string.Format(msg, formats);
            Error(f);
        }

        public static void Error(Exception exception)
        {
            string f = exception.Message;
            f += Environment.NewLine;
            f += exception.StackTrace;
            Error(f);
        }

        public static void Error(string msg)
        {
            string text = $"§c[Error][{CreateTime()}]{msg}";
            AddLogText(text, LoggerLevel.Error);
        }

        public static void Fatal(string msg, params object[] formats)
        {
            string f = string.Format(msg, formats);
            Fatal(f);
        }

        public static void Fatal(string msg)
        {
            string text = $"§4[Fatal][{CreateTime()}]{msg}";
            AddLogText(text, LoggerLevel.Fatal);
        }

        internal static void Update()
        {
            if (!useGUI)
            {
                if (loggerTexts.Count == 0)
                {
                    return;
                }

                for (int i = 0; i < 10; ++i)
                {
                    if (loggerTexts.Count == 0)
                    {
                        return;
                    }

                    string log = loggerTexts.Dequeue().text;
                    CUIFormat(log);
                }
            }
            else
            {

            }
        }

        internal static void CUIFormat(string text)
        {
            string[] f = text.Split('§');
            for (int i = 0; i < f.Length; ++i)
            {
                if (i == 0)
                {
                    Console.Write(f[i]);
                    continue;
                }
                if (f[i] == null || f[i] == string.Empty) continue;
                CUIOutputFormatColor(f[i]);
            }
            Console.ResetColor();
            Console.Write(Environment.NewLine);
        }

        private static void CUIOutputFormatColor(string text)
        {
            char c = text[0];
            if (c == '0')
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '1')
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '2')
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '3')
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '4')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '5')
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '6')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '7')
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '8')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == '9')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == 'a')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == 'b')
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == 'c')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == 'd')
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == 'e')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == 'f')
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(text.Remove(0, 1));
            }
            else if (c == 'r')
            {
                Console.ResetColor();
                Console.Write(text.Remove(0, 1));
            }
            else
            {
                Console.Write(text);
            }
        }

        static string CreateTime()
        {
            return DateTime.Now.ToString("yyyy/M/d H:mm:ss");
        }

        static void AddLogText(string text, LoggerLevel level = LoggerLevel.Info)
        {
            if (level == LoggerLevel.Log && !Server.MineNETConfig.EnableDebugLog)
            {
                return;
            }

            if (Server.MineNETConfig.EnableConsoleOutput)
            {
                LoggerInfo info = new LoggerInfo();
                info.level = level;
                info.text = text;
                loggerTexts.Enqueue(info);
            }
        }
    }
}
