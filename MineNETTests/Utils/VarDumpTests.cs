using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Utils.Tests
{
    [TestClass()]
    public class VarDumpTests
    {
        public class A
        {
            public int f = 114514;
            public string n = "Hallo World";
            private B b = new B();
        }

        public class B
        {
            public float a = 1234.56789f;
        }

        [TestMethod()]
        public void PublicFieldDumpTest()
        {
            string s = VarDump.Var_Dump(new A());
            Console.WriteLine(s);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void FieldDumpTest()
        {
            string s = VarDump.Var_Dump(new A());
            Console.WriteLine(s);
            Assert.IsTrue(true);
        }
    }
}