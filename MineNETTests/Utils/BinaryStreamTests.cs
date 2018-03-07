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
            BinaryStream bs = new BinaryStream();
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    for (int k = 0; k < 10; ++k)
                    {
                        for (int n = 0; n < 10; ++n)
                        {
                            byte[] byte0 = ArrayUtils.CreateArray<byte>(20000);
                            bs.WriteBytes(byte0);

                            Assert.IsTrue(byte0.Length == 20000);
                        }
                    }
                }
            }

        }
    }
}