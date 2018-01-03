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

            //TestCode
            /*var s1 = System.Diagnostics.Stopwatch.StartNew();
            var b = Block.Get(0);
            for (int i = 0; i < 10000000; ++i)
            {
                //new BlockAir();
                var b2 = (Block)b.Clone();
                b2.Count = 2;
            }
            s1.Stop();

            System.Console.WriteLine(b.Count);
            System.Console.WriteLine(s1.Elapsed.ToString());*/

            while (!main.IsShutdown()) ;
        }
    }
}
