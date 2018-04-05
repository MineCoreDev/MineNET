using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.Worlds;

namespace MineNET.Utils.Tests
{
    [TestClass()]
    public class ArrayUtilsTests
    {
        [TestMethod()]
        public void ArrayUtilsTests_CreateArrayTest()
        {
            SubChunk c = new SubChunk();
            SubChunk[] c1 = ArrayUtils.CreateArray(10, c);
            SubChunk[] c2 = ArrayUtils.CreateArray<SubChunk>(10);
            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine(c1[i]);
            }

            Console.WriteLine();

            for (int j = 0; j < 10; ++j)
            {
                Console.WriteLine(c2[j]);
            }
        }
    }
}