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
    public class SubChunkTests
    {
        [TestMethod()]
        public void GetBlockTest()
        {
            SubChunk sub = new SubChunk();
            sub.SetBlock(5, 4, 1, 20);
            Console.WriteLine(sub.GetBlock(5, 4, 1));
        }
    }
}