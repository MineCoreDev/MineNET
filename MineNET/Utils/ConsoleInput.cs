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
                await Task.Delay(1000 / 20);
                string cmd = Console.ReadLine();
                commandQueue.Enqueue(cmd);
                if (cmd == "scl")
                {
                    foreach (string c in commandQueue)
                    {
                        Logger.Log(c);
                    }
                    commandQueue.Clear();
                }
            }
        }

        public string GetCommand()
        {
            if (commandQueue.Count != 0)
            {
                return commandQueue.Dequeue();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
