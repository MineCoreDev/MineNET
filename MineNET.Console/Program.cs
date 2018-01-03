using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

            //TestCode
            /*var s1 = System.Diagnostics.Stopwatch.StartNew();
            for(int i = 0; i < 10000; ++i)
            {
            }
            s1.Stop();*/

            while (!main.IsShutdown()) ;
        }
    }
}
