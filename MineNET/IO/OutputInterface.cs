using System;

namespace MineNET.IO
{
    public interface OutputInterface : IDisposable
    {
        void OutputAction(string outputText);
    }
}
