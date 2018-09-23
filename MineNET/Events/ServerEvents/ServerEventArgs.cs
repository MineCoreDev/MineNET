using System;

namespace MineNET.Events.ServerEvents
{
    public abstract class ServerEventArgs : EventArgs
    {
        public Server Server { get; protected set; }

        public ServerEventArgs()
        {
            this.Server = Server.Instance;
        }
    }
}