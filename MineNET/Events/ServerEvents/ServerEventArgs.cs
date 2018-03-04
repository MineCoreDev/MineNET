using System;

namespace MineNET.Events.ServerEvents
{
    public abstract class ServerEventArgs : EventArgs
    {
        public Server Server { get; }

        public ServerEventArgs()
        {
            Server = Server.Instance;
        }
    }
}
