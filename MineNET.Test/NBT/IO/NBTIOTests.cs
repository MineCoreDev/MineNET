using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.NBT.Tags;
using MineNET.Utils;

namespace MineNET.NBT.IO.Tests
{
    [TestClass()]
    public class NBTIOTests
    {
        public static string Path { get; } = Environment.CurrentDirectory;

        [TestMethod()]
        public void WriteRawFileTest()
        {
            ListTag list = new ListTag(Data.NBTTagType.BYTE);
            list.Name = "listTag";
            list.Add(new ByteTag(0xff));
            list.Add(new ByteTag(0x00));
            list.Add(new ByteTag(0xff));

            CompoundTag tag = new CompoundTag();
            tag.PutBool("bool", true);
            tag.PutByte("byte", 0xff);
            tag.PutShort("short", 0x7fff);
            tag.PutInt("int", 0x7fffffff);
            tag.PutLong("long", 0x7fffffffffffffff);
            tag.PutFloat("float", 0.0001f);
            tag.PutDouble("double", 0.00000001d);
            tag.PutString("string", "Hello NBT");
            tag.PutByteArray("byte[]", ArrayUtils.CreateArray<byte>(100, 0xff));
            tag.PutIntArray("int[]", ArrayUtils.CreateArray<int>(100, 0x7fffffff));
            tag.PutLongArray("long[]", ArrayUtils.CreateArray<long>(100, 0x7fffffffffffffff));
            tag.PutList(list);

            NBTIO.WriteRawFile(Path + "\\" + "raw.nbt", tag);
        }

        [TestMethod()]
        public void ReadRawFileTest()
        {
            NBTIO.ReadRawFile(Path + "\\" + "raw.nbt");
        }

        [TestMethod()]
        public void WriteZLIBFileTest()
        {

        }

        [TestMethod()]
        public void WriteZLIBFileTest1()
        {

        }

        [TestMethod()]
        public void ReadZLIBFileTest()
        {

        }

        [TestMethod()]
        public void ReadZLIBFileTest1()
        {

        }

        [TestMethod()]
        public void WriteGZIPFileTest()
        {

        }

        [TestMethod()]
        public void ReadGZIPFileTest()
        {

        }

        [TestMethod()]
        public void WriteTagTest()
        {

        }

        [TestMethod()]
        public void ReadTagTest()
        {

        }

        [TestMethod()]
        public void WriteItemTest()
        {

        }

        [TestMethod()]
        public void ReadItemTest()
        {

        }
    }
}