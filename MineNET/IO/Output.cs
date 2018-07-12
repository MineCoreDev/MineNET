using MineNET.Events.IOEvents;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MineNET.IO
{
    public class Output : OutputInterface
    {
        private Logger LoggerSystem { get; set; }

        public Thread OutputThread { get; private set; }

        public bool IsRunning { get; private set; }
        public bool UsingGUI { get; private set; }

        public Output(Logger logger)
        {
            this.LoggerSystem = logger;
            
            try
            {
                Console.Clear();
                Console.Title = LanguageService.GetString("server.name");

                this.IsRunning = true;

                this.OutputThread = new Thread(this.OnUpdate);
                this.OutputThread.Name = "OutputThread";
                this.OutputThread.Start();
            }
            catch (IOException)
            {
                this.UsingGUI = true;
            }
        }

        public void OutputAction(string outputText)
        {
            this.ColorFormat(outputText);
        }

        public void Dispose()
        {
            this.IsRunning = false;

            this.OutputThread?.Join();
            this.OutputThread = null;

            this.LoggerSystem = null;
        }

        private void OnUpdate()
        {
            while (this.IsRunning)
            {
                LoggerData data = null;
                if (this.LoggerSystem.LoggerQueue.TryDequeue(out data))
                {
                    if (data.Level >= Server.Instance.Config.ShowLogLevel)
                    {
                        OutputActionEventArgs ev = new OutputActionEventArgs(data.Text);
                        Server.Instance.Event.IO.OnOutputAction(this, ev);
                        this.OutputAction(ev.OutputText);
                    }
                }
            }
        }

        private void ColorFormat(string text)
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
                this.OutputFormatColor(f[i]);
            }
            Console.ResetColor();
            Console.Write(Environment.NewLine);
        }

        private void OutputFormatColor(string text)
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
    }
}
