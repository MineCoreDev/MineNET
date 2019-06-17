using MineNET.Network;

namespace MineNET
{
    public interface IServerManager
    {
        INetworkManager NetworkManager { get; set; }
    }
}