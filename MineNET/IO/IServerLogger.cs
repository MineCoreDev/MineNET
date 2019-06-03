using NLog;
using System;

namespace MineNET.IO
{
    public interface IServerLogger : IDisposable
    {
        ILogger OutputLogger { get; }
        InputInterface InputLogger { get; }

        void StartInputThread();
    }
}