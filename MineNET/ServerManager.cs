using MineNET.Network;
using MineNET.Network.Interfaces;

namespace MineNET
{
    public class ServerManager : IServerManager
    {
        public INetworkManager NetworkManager { get; set; }
    }
}