using System.Net;

namespace MineNET.Network.Interfaces
{
    public interface INetworkManager
    {
        IPlayerList PlayerList { get; set; }
        IServerListInfo ServerListInfo { get; set; }
        IRakNetServerManager RakNetServerManager { get; set; }

        bool Start(IPEndPoint endPoint);
        bool Stop();
    }
}