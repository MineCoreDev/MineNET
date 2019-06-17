using MineNET.Network.RakNet;

namespace MineNET.Network
{
    public interface IRakNetServerManager
    {
        IRakNetServer RakNetServer { get; set; }
    }
}