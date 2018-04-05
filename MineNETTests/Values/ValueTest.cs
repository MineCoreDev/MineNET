using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Values
{
    [TestClass]
    public class ValueTest
    {
        [TestMethod]
        public void ValueTest_AxisAlignedBB_Center()
        {
            AxisAlignedBB bb = new AxisAlignedBB();
            bb.Max = new Vector3(16f, 4f, 12f);
            bb.Min = new Vector3(8f, 3f, 6f);
            Console.WriteLine(bb.Center);
        }

        [TestMethod]
        public void ValueTest_AxisAlignedBB_Size()
        {
            AxisAlignedBB bb = new AxisAlignedBB();
            bb.Max = new Vector3(16f, 4f, 12f);
            bb.Min = new Vector3(6f, 2f, 2f);
            Console.WriteLine(bb.Size);
        }
    }
}
