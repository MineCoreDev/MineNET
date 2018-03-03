using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Utils.Tests
{
    [TestClass()]
    public class BinaryStreamTests
    {
        [TestMethod()]
        public void WriteReadGUIDTest()
        {
            Guid newId = Guid.NewGuid();
            BinaryStream bs = new BinaryStream();
            bs.WriteGUID(newId);
            bs.Position = 0;
            Guid readId = bs.ReadGUID();

            Assert.AreEqual(newId, readId);
        }

        [TestMethod()]
        public void WriteBytesTest()
        {
            byte[] byte0 = new byte[0];
            BinaryStream bs = new BinaryStream(byte0);

            Assert.IsTrue(bs.Position == 0);
        }
    }
}