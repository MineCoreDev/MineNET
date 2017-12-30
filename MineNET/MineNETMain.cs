using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MineCraftPENetwork.Server;

using Conf = MineNET.Properties;
using MineNET.Utils;

namespace MineNET
{
    public sealed class MineNETMain
    {
        public Server server;

        public MineNETMain()
        {
            Conf.MineNET.Default.Reload();
            Conf.Server.Default.Reload();

            if (Conf.MineNET.Default.AutoUpdate)
            {

            }

            server = new Server();
        }

        public bool IsShutdown()
        {
            return server.networkManager.raknet.IsShutdown();
        }
    }
}
