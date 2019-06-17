namespace MineNET.Network
{
    public class NetworkManager : INetworkManager
    {
        public IPlayerList PlayerList { get; set; }
        public IServerListInfo ServerListInfo { get; set; }
        public IRakNetServerManager RakNetServerManager { get; set; }
    }
}