using System;
using System.Collections.Concurrent;

namespace MineNET.IO
{
    public interface InputInterface : IDisposable
    {
        ConcurrentQueue<string> InputQueue { get; }

        void InputAction(string inputText);
        void AddInputQueue(string inputText);
        void GetInputQueue();
    }
}