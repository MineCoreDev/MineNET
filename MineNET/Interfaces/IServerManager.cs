using MineNET.Network;
using MineNET.Network.Interfaces;

namespace MineNET
{
    public interface IServerManager
    {
        INetworkManager NetworkManager { get; set; }
    }
}