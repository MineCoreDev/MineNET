using System;

namespace MineNET.IO
{
    public interface ILogger : IDisposable
    {
        InputInterface Input { get; }
        OutputInterface Output { get; }

        void Log(object text);
        void Log(object text, params object[] args);

        void Info(object text);
        void Info(object text, params object[] args);

        void Notice(object text);
        void Notice(object text, params object[] args);

        void Warning(object text);
        void Warning(object text, params object[] args);

        void Error(object text);
        void Error(object text, params object[] args);

        void Fatal(object text);
        void Fatal(object text, params object[] args);
    }
}
