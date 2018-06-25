namespace MineNET
{
    public interface IServerListInfo
    {
        string Platform { get; }
        string ServerName { get; }

        int ClientProtocol { get; }
        string ClientVersion { get; }

        int JoinedPlayerCount { get; }
        int MaxPlayerCount { get; }

        string SystemName { get; }
        string Description { get; }
    }
}
