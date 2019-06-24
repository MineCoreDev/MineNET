using System.Net;
using MineNET.Network.Interfaces;

namespace MineNET.Network
{
    public class NetworkManager : INetworkManager
    {
        public IPlayerList PlayerList { get; set; } = new PlayerList();
        public IServerListInfo ServerListInfo { get; set; } = new ServerListInfo();
        public IRakNetServerManager RakNetServerManager { get; set; } = new RakNetServerManager();

        public bool Start(IPEndPoint endPoint)
        {
            return RakNetServerManager.Start(endPoint);
        }

        public bool Stop()
        {
            return RakNetServerManager.Stop();
        }
    }
}