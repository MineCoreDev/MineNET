using MineNET.Network.Interfaces;
using MineNET.Network.RakNet;
using MineNET.Network.RakNet.Interfaces;

namespace MineNET.Network
{
    public class RakNetServerManager : IRakNetServerManager
    {
        public IRakNetServer RakNetServer { get; set; }
    }
}