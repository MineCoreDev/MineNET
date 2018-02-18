using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.NBT.Tags;

namespace MineNET.NBT.Tests
{
    [TestClass()]
    public class NBTIOTests
    {
        [TestMethod()]
        public void WriteFileTest()
        {
            string path = Environment.CurrentDirectory + "\\NBTFile.txt";
            CompoundTag tag = new CompoundTag();
            tag.PutByteArray("Data", new byte[] { 1, 2, 3, 4, 5, 0x0f, 0xff, 0x00, 0x68, 0x11 });
            Console.WriteLine(path);
            NBTIO.WriteFile(path, tag);
        }

        [TestMethod()]
        public void ReadFileTest()
        {
            string path = Environment.CurrentDirectory + "\\NBTFile.txt";
            CompoundTag tag = NBTIO.ReadFile(path);
            byte[] d = tag.GetByteArray("Data");
            foreach (byte b in d)
            {
                Console.WriteLine(b);
            }
        }
    }
}