using MineNET.Network.Interfaces;

namespace MineNET.Interfaces
{
    public interface IServerManager
    {
        INetworkManager NetworkManager { get; set; }
    }
}