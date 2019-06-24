using System.Net;
using MineNET.Network.Interfaces;
using MineNET.Network.RakNet.Interfaces;

namespace MineNET.Network
{
    public class RakNetServerManager : IRakNetServerManager
    {
        public IRakNetServer RakNetServer { get; set; }

        public bool Start(IPEndPoint endPoint)
        {
            return RakNetServer.Start(endPoint);
        }

        public bool Stop()
        {
            return RakNetServer.Stop();
        }
    }
}