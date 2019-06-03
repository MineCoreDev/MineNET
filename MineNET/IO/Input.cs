using MineNET.Commands;
using MineNET.Events.IOEvents;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MineNET.IO
{
    public class Input : InputInterface
    {
        public Thread InputThread { get; private set; }

        public bool IsRunning { get; private set; }
        public bool UsingGUI { get; private set; }

        public int InputStartTop { get; internal set; }

        public ConcurrentQueue<string> InputQueue { get; private set; } = new ConcurrentQueue<string>();

        public void Start()
        {
            try
            {
                Console.Write(string.Empty);
            }
            catch
            {
                this.UsingGUI = true;
                return;
            }

            this.IsRunning = true;

            this.InputThread = new Thread(this.OnUpdate);
            this.InputThread.Name = "InputThread";
            this.InputThread.IsBackground = true;
            this.InputThread.Start();
        }

        public void InputAction(string inputText)
        {
            CommandData data = new CommandData(new ConsoleCommandSender(), inputText);
            Server.Instance.Command.CommandHandler.OnCommandExecute(data);
        }

        public void Dispose()
        {
            this.IsRunning = false;

            this.InputThread?.Abort();
            this.InputThread = null;
        }

        private void OnUpdate()
        {
            while (this.IsRunning)
            {
                InputStartTop = Console.CursorTop;
                ReadLine.HistoryEnabled = true;
                string inputText = ReadLine.Read("> ");
                if (!string.IsNullOrWhiteSpace(inputText))
                {
                    this.AddInputQueue(inputText);
                }
            }
        }

        public void AddInputQueue(string inputText)
        {
            this.InputQueue.Enqueue(inputText);
        }

        public void GetInputQueue()
        {
            if (!this.InputQueue.IsEmpty)
            {
                string inputText;
                this.InputQueue.TryDequeue(out inputText);
                InputActionEventArgs ev = new InputActionEventArgs(inputText);
                Server.Instance.Event.IO.OnInputAction(this, ev);
                this.InputAction(ev.InputText);
            }
        }
    }
}