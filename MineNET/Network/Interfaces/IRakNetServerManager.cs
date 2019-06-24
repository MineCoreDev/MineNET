using System.Net;
using MineNET.Network.RakNet.Interfaces;

namespace MineNET.Network.Interfaces
{
    public interface IRakNetServerManager
    {
        IRakNetServer RakNetServer { get; set; }

        bool Start(IPEndPoint endPoint);
        bool Stop();
    }
}