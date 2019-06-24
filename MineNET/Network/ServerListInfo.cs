using MineNET.Network.Interfaces;

namespace MineNET.Network
{
    public class ServerListInfo : IServerListInfo
    {
        public string Platform { get; }
        public string ServerName { get; }
        public int ClientProtocol { get; }
        public string ClientVersion { get; }
        public int JoinedPlayerCount => Server.Instance.ServerManager.NetworkManager.PlayerList.GetPlayers().Length;
        public int MaxPlayerCount { get; }
        public string SystemName { get; }
        public string Description { get; }
    }
}