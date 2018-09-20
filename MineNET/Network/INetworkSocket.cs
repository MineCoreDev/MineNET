using System;
using System.Net;
using System.Net.Sockets;

namespace MineNET.Network
{
    public interface INetworkSocket : IDisposable
    {
        UdpClient Socket { get; }

        void Init(IPEndPoint point);
    }
}
