using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCraftPENetwork.Server;

using MineNET.Utils;

namespace MineNET
{
    public class MineNETMain
    {
        public MainLogger logger;

        public RakNetServer server;

        public MineNETMain()
        {
            logger = new MainLogger();

            server = new RakNetServer(1115);
        }
    }
}
