using MineNET.Network.RakNet;
using MineNET.Network.RakNet.Interfaces;

namespace MineNET.Network.Interfaces
{
    public interface IRakNetServerManager
    {
        IRakNetServer RakNetServer { get; set; }
    }
}