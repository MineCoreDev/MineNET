using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.Values;
using System;

namespace MineNET.Test.Values
{
    [TestClass]
    public class AxisAlignedBBTest
    {
        [TestMethod]
        public void TestMethod()
        {
            AxisAlignedBB box = new AxisAlignedBB(Vector3.Zero, Vector3.One);
            Console.WriteLine(box.Center);

            Assert.IsFalse(box.ContainsVector(new Vector3(1f, 2f, 1f)));
            Assert.IsTrue(box.ContainsVector(new Vector3(1f, 0.5f, 1f)));
            Assert.IsTrue(box.ContainsVector(new Vector3(1f, 1f, 1f)));
            Assert.IsFalse(box.ContainsVector(new Vector3(1f, 1.1f, 1f)));
        }
    }
}
