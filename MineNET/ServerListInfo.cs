using MineNET.Network.MinecraftPackets;

namespace MineNET
{
    public class ServerListInfo : IServerListInfo
    {
        public int ClientProtocol
        {
            get
            {
                return MinecraftProtocol.ClientProtocol;
            }
        }

        public string ClientVersion
        {
            get
            {
                return MinecraftProtocol.ClientVersion;
            }
        }

        public string Description
        {
            get
            {
                return "MineNET";//TODO: GetDefaultGameMode...
            }
        }

        public int JoinedPlayerCount
        {
            get
            {
                return Server.Instance.Network.Players.Count;
            }
        }

        public int MaxPlayerCount
        {
            get
            {
                return Server.Instance.ServerProperty.MaxPlayers;
            }
        }

        public string Platform
        {
            get
            {
                return "MCPE";
            }
        }

        public string ServerName
        {
            get
            {
                return Server.Instance.ServerProperty.ServerMotd;
            }
        }

        public string SystemName
        {
            get
            {
                return "MineNET";
            }
        }

        public override string ToString()
        {
            return $"{this.Platform};{this.ServerName}"
                + $";{this.ClientProtocol};{this.ClientVersion}"
                + $";{this.JoinedPlayerCount};{this.MaxPlayerCount}"
                + $";{this.SystemName};{this.Description}";
        }
    }
}
