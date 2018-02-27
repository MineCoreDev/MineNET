using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod()]
        public void Test()
        {
            Dictionary<int, int> k = new Dictionary<int, int>();
            k[5] = 5;
            k[26] = 26;
            k[13] = 13;

            int[] a1 = k.Values.ToArray();
            for (int i = 0; i < k.Values.Count; ++i)
            {
                Console.WriteLine(a1[i]);
                k.Remove(a1[i]);
            }

            a1 = k.Values.ToArray();
            for (int i = 0; i < k.Values.Count; ++i)
            {
                Console.WriteLine(a1[i]);
            }
        }
    }
}