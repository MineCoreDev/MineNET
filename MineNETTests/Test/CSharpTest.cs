using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNETTests.Test
{
    [TestClass]
    public class CSharpTest
    {
        partial class A
        {
            public void AMet(int a)
            {
                Console.WriteLine(a);
            }

            public int A2Met(int a)
            {
                return this.Sum(a, 12);
            }
        }

        partial class A
        {
            int Sum(int a, int b)
            {
                return a + b;
            }
        }

        [TestMethod]
        public void PartialTest()
        {
            A a = new A();
            Console.WriteLine(a.A2Met(12));
            a.AMet(1);
        }

        [TestMethod]
        public void StringTest()
        {
            Assert.IsTrue("Apple" == "Apple");
            Assert.IsFalse("Apple" == "apple");
            Assert.IsFalse("a p p l e" == "apple");
        } 
    }
}
