using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.NBT.Tags;
using MineNET.Utils;

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
            NBTIO.WriteRawFile(path, tag);
        }

        [TestMethod()]
        public void ReadFileTest()
        {
            string path = Environment.CurrentDirectory + "\\NBTFile.txt";
            CompoundTag tag = NBTIO.ReadRawFile(path);
            byte[] d = tag.GetByteArray("Data");
            foreach (byte b in d)
            {
                Console.WriteLine(b);
            }
        }

        [TestMethod()]
        public void WriteZLIBFileTest()
        {
            string path = Environment.CurrentDirectory + "\\NBTFileZLIB.txt";
            CompoundTag tag = new CompoundTag();
            tag.PutByteArray("Data", new byte[] { 1, 2, 3, 4, 5, 0x0f, 0xff, 0x00, 0x68, 0x11 });
            Console.WriteLine(path);
            NBTIO.WriteZLIBFile(path, tag);
        }

        [TestMethod()]
        public void ReadZLIBFileTest()
        {
            string path = Environment.CurrentDirectory + "\\level.dat";
            CompoundTag tag = NBTIO.ReadGZIPFile(path);
            Console.WriteLine(VarDump.Var_Dump(tag));
        }
    }
}