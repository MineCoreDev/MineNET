using System;
using System.Collections.Generic;
using System.Text;

using MineNET;
using MineNET.Utils;
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

            var buffer = new byte[0];
            var p1 = Binary.PutBoolean(buffer, false);
            var p2 = Binary.PutByte(p1, 0xff);
            var p3 = Binary.PutSByte(p2, -2);
            var p4 = Binary.PutShort(p3, -12345);
            var p5 = Binary.PutUShort(p4, 63456);
            System.Console.WriteLine(Binary.ReadBoolean(p5, 0));
            System.Console.WriteLine(Binary.ReadByte(p5, 1));
            System.Console.WriteLine(Binary.ReadSByte(p5, 2));
            System.Console.WriteLine(Binary.ReadShort(p5, 3));
            System.Console.WriteLine(Binary.ReadUShort(p5, 5));

            while (!main.IsShutdown()) ;
        }
    }
}
