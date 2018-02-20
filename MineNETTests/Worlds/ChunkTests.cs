using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Worlds.Tests
{
    [TestClass()]
    public class ChunkTests
    {
        [TestMethod()]
        public void CreateArrayTest()
        {
            Chunk c = new Chunk(0, 0);
            byte[] b = c.CreateArray<byte>(0, 16 * 16 * 16);
            Console.WriteLine(b.Length);
            foreach(byte bb in b)
            {
                Console.WriteLine(bb);
            }
        }
    }
}