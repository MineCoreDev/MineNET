using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Utils
{
    public class MainLogger : ILogger
    {
        private Queue<string> logText = new Queue<string>();

        private int bufferSize = 500;

        public MainLogger()
        {
            //this.bufferSize = MineNETMain.GetConfig().LoggerBufferSize;
            //Console.SetBufferSize(this.bufferSize, Console.BufferWidth - 10);
        }

        public void Debug(object message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[Debug]" + message.ToString());
            Console.ResetColor();
        }

        public void Debug(Exception message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[Debug]" + message.Message + Environment.NewLine + message.StackTrace);
            Console.ResetColor();
        }

        public void Debug(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            var f = string.Format(message, args);
            Console.WriteLine("[Debug]" + f);
            Console.ResetColor();
        }

        public void Error(Exception message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error]" + message.Message + Environment.NewLine + message.StackTrace);
            Console.ResetColor();
        }

        public void Error(object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error]" + message.ToString());
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error]" + message);
            Console.ResetColor();
        }

        public void Log(object message)
        {
            this.Format("[Log]" + message.ToString());
            Console.ResetColor();
        }

        public void Log(Exception message)
        {
            this.Format("[Log]" + message.Message);
            Console.ResetColor();
        }

        public void Log(string message, params object[] args)
        {
            var f = string.Format(message, args);
            this.Format("[Log]" + f);
            Console.ResetColor();
        }

        public void Info(object message)
        {
            this.Format("[Info]" + message.ToString());
            Console.ResetColor();
        }

        public void Info(string message, params object[] args)
        {
            var f = string.Format(message, args);
            this.Format("[Info]" + f);
            Console.ResetColor();
        }

        public void Trace(object message)
        {
            throw new NotImplementedException();
        }

        public void Trace(Exception message)
        {
            throw new NotImplementedException();
        }

        public void Warning(object message)
        {
            throw new NotImplementedException();
        }

        public void Warning(Exception message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }

        private void Format(string text)
        {
            string[] f = text.Split('§');
            for(int i = 0; i < f.Length; ++i)
            {
                if (i == 0)
                {
                    Console.Write(f[i]);
                    continue;
                }
                this.FomatColor(f[i]);
            }
            Console.Write(Environment.NewLine);
        }

        private void FomatColor(string text)
        {
            var c = text[0];
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

        private void AddLogTextQueue(string text)
        {
            if (this.logText.Count > this.bufferSize)
            {
                this.logText.Dequeue();
            }
            this.logText.Enqueue(text);
        }

        public void ClearLogTextQueue()
        {
            this.logText.Clear();
        }
    }
}
