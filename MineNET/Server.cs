using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Network;

namespace MineNET
{
    public sealed class Server
    {
        public NetworkManager networkManager;

        public Server()
        {
            networkManager = new NetworkManager();
        }
    }
}
