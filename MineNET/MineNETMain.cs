using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCraftPENetwork;

using MineNET.Utils;

namespace MineNET
{
    class MineNETMain
    {
        public MainLogger logger;
        public Network network;

        public MineNETMain()
        {
            logger = new MainLogger();
        }
    }
}
