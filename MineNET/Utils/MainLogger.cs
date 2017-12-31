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
            Console.SetBufferSize(this.bufferSize, Console.BufferWidth - 10);
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

        public void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[Debug]" + message);
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

        public void Log(string message)
        {
            this.Format("[Log]" + message);
            Console.ResetColor();
        }

        public void Info(object message)
        {
            this.Format("[Info]" + message.ToString());
            Console.ResetColor();
        }

        public void Info(string message)
        {
            this.Format("[Info]" + message);
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
            int c = 0;
            foreach(var s in f)
            {
                if (c == 0)
                {
                    Console.Write(s);
                    break;
                }
                this.FomatColor(s);
                c++;
            }
            Console.Write(Environment.NewLine);
        }

        private void FomatColor(string text)
        {
            var c = text[0];
            switch (c)
            {
                case '0':
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '1':
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '2':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '3':
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '4':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '5':
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '6':
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '7':
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '8':
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(text.Remove(0, 1));
                    break;

                case '9':
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(text.Remove(0, 1));
                    break;

                case 'a':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(text.Remove(0, 1));
                    break;

                case 'b':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(text.Remove(0, 1));
                    break;

                case 'c':
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(text.Remove(0, 1));
                    break;

                case 'd':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(text.Remove(0, 1));
                    break;

                case 'e':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(text.Remove(0, 1));
                    break;

                case 'f':
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text.Remove(0, 1));
                    break;

                default:
                    Console.Write(text);
                    break;
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
