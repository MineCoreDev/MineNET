using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MineNET;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

namespace MineNET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = new MineNETMain();

            while (!main.server.IsShutdown()) ;
        }
    }
}
