using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Blocks.Tests
{
    [TestClass()]
    public class BlockTests
    {
        [TestMethod()]
        public void GetTest()
        {
            Block b1 = Block.Get("minecraft:stone");
            Block b2 = Block.Get("minecraft:stone:2");
            Block b3 = Block.Get("minecraft:null");
            Block b4 = Block.Get("minecraft:null:2");
            Block b5 = Block.Get("98");
            Block b6 = Block.Get("98:1");
            Block b7 = Block.Get("256");
            Block b8 = Block.Get("256:1");

            Console.WriteLine(b1.ID + ":" + b1.Damage);
            Console.WriteLine(b2.ID + ":" + b2.Damage);
            Console.WriteLine(b3.ID + ":" + b3.Damage);
            Console.WriteLine(b4.ID + ":" + b4.Damage);
            Console.WriteLine(b5.ID + ":" + b5.Damage);
            Console.WriteLine(b6.ID + ":" + b6.Damage);
            Console.WriteLine(b7.ID + ":" + b7.Damage);
            Console.WriteLine(b8.ID + ":" + b8.Damage);
        }
    }
}