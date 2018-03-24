using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNETTests.System
{
    [TestClass]
    public class ArrayTest
    {
        [TestMethod]
        public void Linq()
        {
            int[] test = new int[]
            {
                1,2,3,4,5,6,7,8,9
            };
            int[] test2 = new int[]
            {
                1,2,3,4,5,6,7,8,9
            };

            Assert.IsTrue(test.SequenceEqual(test2));
        }

        [TestMethod]
        public void Convert()
        {
            int[] test = new int[]
            {
                1,2,3,4,5,6,7,8,9
            };
            int[] test2 = new int[]
            {
                1,2,3,4,5,6,7,8,9
            };

            IStructuralEquatable i = test;
            Assert.IsTrue(i.Equals(test2, StructuralComparisons.StructuralEqualityComparer));
        }
    }
}
