using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MineNET;
using MineNET.Blocks;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

namespace MineNET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = new MineNETMain();

            BlockFactory.Init();
            System.Console.WriteLine(Block.Get(0).Name);

            while (!main.IsShutdown()) ;
        }
    }
}
