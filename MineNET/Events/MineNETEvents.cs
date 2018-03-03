using System;

namespace MineNET.Events
{
    public abstract class MineNETEvents
    {
        public delegate void EventHandler<T>(T args) where T : EventArgs;
    }
}
