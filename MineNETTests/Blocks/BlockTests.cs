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
            Block b7 = Block.Get("300");
            Block b8 = Block.Get("300:1");

            Console.WriteLine(b1.BlockID + ":" + b1.Damage);
            Console.WriteLine(b2.BlockID + ":" + b2.Damage);
            Console.WriteLine(b3.BlockID + ":" + b3.Damage);
            Console.WriteLine(b4.BlockID + ":" + b4.Damage);
            Console.WriteLine(b5.BlockID + ":" + b5.Damage);
            Console.WriteLine(b6.BlockID + ":" + b6.Damage);
            Console.WriteLine(b7.BlockID + ":" + b7.Damage);
            Console.WriteLine(b8.BlockID + ":" + b8.Damage);
        }
    }
}