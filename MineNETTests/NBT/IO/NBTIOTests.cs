using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Utils;

namespace MineNET.NBT.IO.Tests
{
    [TestClass()]
    public class NBTIOTests
    {
        [TestMethod()]
        public void WriteRawFileTest()
        {
            ListTag list = new ListTag("list", NBTTagType.INT);
            list.Add(new IntTag(12345));
            list.Add(new IntTag(67890));
            CompoundTag subTag = new CompoundTag();
            subTag.PutBool("bool", true);
            subTag.PutByte("byte", 123);
            CompoundTag tag = new CompoundTag();
            tag.PutBool("bool", true);
            tag.PutByte("byte", 123);
            tag.PutByteArray("byteArray", ArrayUtils.CreateArray<byte>(200));
            tag.PutShort("short", 12345);
            tag.PutInt("int", 12345678);
            tag.PutIntArray("intArray", ArrayUtils.CreateArray<int>(200));
            tag.PutLong("long", 123456789123456);
            tag.PutLongArray("longArray", ArrayUtils.CreateArray<long>(200));
            tag.PutFloat("float", 12.3456f);
            tag.PutDouble("double", 12.3456789);
            tag.PutList(list);
            tag.PutCompound("com", subTag);
            NBTIO.WriteRawFile(Environment.CurrentDirectory + "\\test.nbt", tag);
        }

        [TestMethod()]
        public void ReadRawFileTest()
        {
            CompoundTag tag = NBTIO.ReadRawFile(Environment.CurrentDirectory + "\\test.nbt");
            tag.GetBool("bool");
            tag.GetByte("byte");
            tag.GetByteArray("byteArray");
            tag.GetShort("short");
            tag.GetInt("int");
            tag.GetIntArray("intArray");
            tag.GetLong("long");
            tag.GetLongArray("longArray");
            tag.GetFloat("float");
            tag.GetDouble("double");
            tag.GetList("list");
            tag.GetCompound("com");
            Console.WriteLine(tag);
        }

        [TestMethod()]
        public void WriteGZFileTest()
        {
            ListTag list = new ListTag("list", NBTTagType.COMPOUND);
            list.Add(new CompoundTag().PutInt("c1", 123));
            list.Add(new CompoundTag().PutLong("c2", 123456));
            CompoundTag subTag = new CompoundTag();
            subTag.PutBool("bool", true);
            subTag.PutByte("byte", 123);
            CompoundTag tag = new CompoundTag();
            tag.PutBool("bool", true);
            tag.PutByte("byte", 123);
            tag.PutByteArray("byteArray", ArrayUtils.CreateArray<byte>(200));
            tag.PutShort("short", 12345);
            tag.PutInt("int", 12345678);
            tag.PutIntArray("intArray", ArrayUtils.CreateArray<int>(200));
            tag.PutLong("long", 123456789123456);
            tag.PutLongArray("longArray", ArrayUtils.CreateArray<long>(200));
            tag.PutFloat("float", 12.3456f);
            tag.PutDouble("double", 12.3456789);
            tag.PutList(list);
            tag.PutCompound("com", subTag);
            NBTIO.WriteGZIPFile(Environment.CurrentDirectory + "\\test2.nbt", tag);
        }

        [TestMethod()]
        public void ReadGZFileTest()
        {
            CompoundTag tag = NBTIO.ReadGZIPFile(Environment.CurrentDirectory + "\\test2.nbt");
            Console.WriteLine(tag);
            Console.WriteLine(tag.GetList("list")[0]);
        }

        private CompoundTag namedTag;

        [TestMethod()]
        public void Test3()
        {
            this.namedTag = new CompoundTag();
            this.namedTag.PutList(new ListTag("Attributes", NBTTagType.COMPOUND));

            this.namedTag.PutList(new ListTag("Pos", NBTTagType.FLOAT));
            this.namedTag.PutList(new ListTag("Rotation", NBTTagType.FLOAT));

            this.namedTag.PutInt("PlayerGameMode", 0);
            this.namedTag.PutInt("PlayerLevel", 0);
            this.namedTag.PutFloat("PlayerLevelProgress", 0f);

            if (!this.namedTag.Exist("Inventory"))
            {
                ListTag initItems = new ListTag("Inventory", NBTTagType.COMPOUND);
                for (int i = 0; i < 36; ++i)
                {
                    initItems.Add(NBTIO.WriteItem(Item.Get(0), i));
                }
                this.namedTag.PutList(initItems);
            }

            ListTag items = this.namedTag.GetList("Inventory");
            for (int i = 0; i < 36; ++i)
            {
                Item item = NBTIO.ReadItem((CompoundTag) items[i]);
            }

            NBTIO.WriteGZIPFile(Environment.CurrentDirectory + "\\test3.nbt", this.namedTag);
            Console.WriteLine(NBTIO.ReadGZIPFile(Environment.CurrentDirectory + "\\test3.nbt"));
        }
    }
}