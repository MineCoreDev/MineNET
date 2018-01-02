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
            //
            s1.Stop();
            System.Console.WriteLine(s1.Elapsed.ToString());*/

            while (!main.IsShutdown()) ;
        }
    }
}
