using MineNET.Network;
using System;

namespace MineNET.Events.NetworkEvents
{
    public abstract class NetworkEventArgs : EventArgs
    {
        public NetworkManager Network { get; protected set; }

        public NetworkEventArgs()
        {
            this.Network = Server.Instance.Network;
        }
    }
}
