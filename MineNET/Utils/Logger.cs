﻿using System;
using System.Collections.Generic;
using System.IO;

namespace MineNET.Utils
{
    public class Logger
    {
        const int WINDOW_X = 100;
        const int WINDOW_Y = 25;

        public class LoggerInfo
        {
            public string text;
            public LoggerLevel level;
        }

        public enum LoggerLevel
        {
            Log,
            Info,
            Notice,
            Warning,
            Error,
            Fatal
        }

        public bool UseGUI { get; set; }

        Queue<LoggerInfo> loggerTexts = new Queue<LoggerInfo>();
        public Queue<LoggerInfo> GuiLoggerTexts { get; } = new Queue<LoggerInfo>();

        internal void Init()
        {
            try
            {
                Console.SetWindowSize(WINDOW_X, WINDOW_Y);
                Console.SetBufferSize(WINDOW_X, 1000);

                int bit = 32;
                if (Environment.Is64BitOperatingSystem)
                {
                    bit = 64;
                }

                Console.Title = $"MineNET x{bit}";
            }
            catch
            {
                this.UseGUI = true;
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
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string f = string.Format(msg, formats);
                Log(f);
            }
        }

        public static void Log(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string text = $"§7[Log][{CreateTime()}]{msg}";
                Server.Instance.Logger?.AddLogText(text, LoggerLevel.Log);
            }
        }

        public static void Log(object msg)
        {
            Log(msg.ToString());
        }

        public static void Info(string msg, params object[] formats)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string f = string.Format(msg, formats);
                Info(f);
            }
        }

        public static void Info(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string text = $"§f[Info][{CreateTime()}]{msg}";
                Server.Instance.Logger?.AddLogText(text);
            }
        }

        public static void Info(object msg)
        {
            Info(msg.ToString());
        }

        public static void Notice(string msg, params object[] formats)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string f = string.Format(msg, formats);
                Notice(f);
            }
        }

        public static void Notice(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string text = $"§b[Notice][{CreateTime()}]{msg}";
                Server.Instance.Logger?.AddLogText(text, LoggerLevel.Notice);
            }
        }

        public static void Notice(object msg)
        {
            Notice(msg.ToString());
        }

        public static void Warning(string msg, params object[] formats)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string f = string.Format(msg, formats);
                Warning(f);
            }
        }

        public static void Warning(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string text = $"§e[Warning][{CreateTime()}]{msg}";
                Server.Instance.Logger?.AddLogText(text, LoggerLevel.Warning);
            }
        }

        public static void Warning(object msg)
        {
            Warning(msg.ToString());
        }

        public static void Error(string msg, params object[] formats)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string f = string.Format(msg, formats);
                Error(f);
            }
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
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string text = $"§c[Error][{CreateTime()}]{msg}";
                Server.Instance.Logger?.AddLogText(text, LoggerLevel.Error);
            }
        }

        public static void Error(object msg)
        {
            Error(msg.ToString());
        }

        public static void Fatal(string msg, params object[] formats)
        {
            if (msg[0] == '%')
            {
                msg = msg.Remove(0, 1);
                msg = LangManager.GetString(msg);
            }
            string f = string.Format(msg, formats);
            Fatal(f);
        }

        public static void Fatal(Exception exception)
        {
            string f = exception.Message;
            f += Environment.NewLine;
            f += exception.StackTrace;
            Fatal(f);
        }

        public static void Fatal(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg[0] == '%')
                {
                    msg = msg.Remove(0, 1);
                    msg = LangManager.GetString(msg);
                }
                string text = $"§4[Fatal][{CreateTime()}]{msg}";
                Server.Instance.Logger?.AddLogText(text, LoggerLevel.Fatal);
            }
        }

        public static void Fatal(object msg)
        {
            Fatal(msg.ToString());
        }

        internal void Update()
        {
            if (this.loggerTexts.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 10; ++i)
            {
                if (this.loggerTexts.Count == 0)
                {
                    return;
                }

                LoggerInfo info = this.loggerTexts.Dequeue();
                if (info != null)
                {
                    string log = info.text;
                    if (!this.UseGUI)
                    {
                        this.CUIFormat(log);
                    }
                    else
                    {
                        RemoveColorCode(ref log);
                        info.text = log;
                        this.GuiLoggerTexts.Enqueue(info);
                    }

                    if (info.level != LoggerLevel.Log)
                    {
                        this.WriteLog(log);
                    }
                }
            }
        }

        internal void CUIFormat(string text)
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
                this.CUIOutputFormatColor(f[i]);
            }
            Console.ResetColor();
            Console.Write(Environment.NewLine);
        }

        private void CUIOutputFormatColor(string text)
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

        static string CreateDay()
        {
            return DateTime.Now.ToString("yyyy-M-d");
        }

        static void RemoveColorCode(ref string text)
        {
            int seqIndex = text.IndexOf("§");
            if (seqIndex == -1)
            {
                return;
            }
            text = text.Remove(seqIndex, 2);
            RemoveColorCode(ref text);
        }

        void AddLogText(string text, LoggerLevel level = LoggerLevel.Info)
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
                this.loggerTexts.Enqueue(info);
            }
        }

        internal async void WriteLog(string text)
        {
            string path = $"{Server.ExecutePath}\\logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            RemoveColorCode(ref text);

            string filePath = $"{path}\\{CreateDay()}.log";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sr = File.CreateText(filePath))
                {
                    await sr.WriteLineAsync(text);
                }
            }
            else
            {
                using (StreamWriter sr = File.AppendText(filePath))
                {
                    await sr.WriteLineAsync(text);
                }
            }
        }
    }
}
