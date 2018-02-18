using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Blocks.Tests
{
    [TestClass()]
    public class BlockTests
    {
        [TestMethod()]
        public void GetTest()
        {
            BlockFactory f = new BlockFactory();
            FieldInfo field = f.GetType().GetField("STONE");

            Console.WriteLine(field.GetValue(f));

            Assert.IsTrue(true);
        }
    }
}