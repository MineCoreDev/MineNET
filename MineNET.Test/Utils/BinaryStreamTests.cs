using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MineNET.Utils.Tests
{
    [TestClass()]
    public class BinaryStreamTests
    {
        [TestMethod()]
        public void CallManyTest_1000000()
        {
            BinaryStream stream = new BinaryStream();
            stream.Reservation(1000000);
            for (int i = 0; i < 1000000; i++)
            {
                stream.WriteByte((byte) i);
            }
            Console.WriteLine(stream.Length);

            BinaryStream read = new BinaryStream(stream.ToArray());
            for (int i = 0; i < 1000000; i++)
            {
                read.ReadByte();
            }
        }

        [TestMethod()]
        public void CallManyTest_1000000_INT()
        {
            BinaryStream stream = new BinaryStream();
            stream.Reservation(10000000 * 4);
            for (int i = 0; i < 10000000; i++)
            {
                stream.WriteInt(i);
            }
        }

        [TestMethod()]
        public void ReadByteTest()
        {
            BinaryStream stream = new BinaryStream();
            stream.WriteByte(123);
        }
    }
}