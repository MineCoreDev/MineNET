using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.Values;

namespace MineNET.Test.Values
{
    [TestClass]
    public class HashCode
    {
        [TestMethod]
        public void IntHashTest()
        {
            int a = 1234;
            Console.WriteLine(a.GetHashCode());
        }

        [TestMethod]
        public void Vector2HashTest()
        {
            Vector3 pos = new Vector3(10, 5, 3);
            Console.WriteLine(pos.GetHashCode());
        }
    }
}
