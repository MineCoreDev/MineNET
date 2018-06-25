using System;

namespace MineNET.IO
{
    public interface InputInterface : IDisposable
    {
        void InputAction(string inputText);

        void GetQueueCommand();
    }
}
