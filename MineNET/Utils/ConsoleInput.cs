using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MineNET.Utils
{
    public sealed class ConsoleInput
    {
        Queue<string> commandQueue = new Queue<string>();

        public ConsoleInput()
        {
            StartTask();
        }

        async void StartTask()
        {
            while (!Server.Instance.IsShutdown())
            {
                if (!Server.Instance.Logger.UseGUI)
                {
                    await Task.Delay(1000 / 20);
                    string cmd = Console.ReadLine();
                    this.commandQueue.Enqueue(cmd);
                }
                else
                {
                    return;
                }
            }
        }

        public string GetCommand()
        {
            if (this.commandQueue.Count != 0)
            {
                return this.commandQueue.Dequeue();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
