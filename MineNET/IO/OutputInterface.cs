using System;
using System.Collections.Concurrent;

namespace MineNET.IO
{
    public interface OutputInterface : IDisposable
    {
        ConcurrentQueue<LoggerData> OutputQueue { get; }

        void OutputAction(string outputText);
        void AddOutputQueue(LoggerData outputData);
    }
}
