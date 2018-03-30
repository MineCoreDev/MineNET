using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Utils;
using Newtonsoft.Json.Linq;

namespace MineNET.NBT.IO.Tests
{
    [TestClass()]
    public class NBTJsonSerializerTests
    {
        [TestMethod()]
        public void SerializeTest()
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

            Console.WriteLine(NBTJsonSerializer.Serialize(tag).ToString());
        }

        [TestMethod()]
        public void DeserializeTest()
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

            JObject json = NBTJsonSerializer.Serialize(tag);
            Console.WriteLine(NBTJsonSerializer.Serialize(NBTJsonSerializer.Deserialize(json)).ToString());
        }
    }
}