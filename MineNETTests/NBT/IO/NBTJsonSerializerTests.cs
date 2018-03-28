using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.NBT.Tags;
using Newtonsoft.Json.Linq;

namespace MineNET.NBT.IO.Tests
{
    [TestClass()]
    public class NBTJsonSerializerTests
    {
        [TestMethod()]
        public void SerializeTest()
        {
            CompoundTag tag = new CompoundTag();
            tag.PutByte("byte", 123);
            tag.PutInt("int", 12345);
            tag.PutLong("long", 123456789);

            Console.WriteLine(NBTJsonSerializer.Serialize(tag).ToString());
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            CompoundTag tag = new CompoundTag();
            tag.PutByte("byte", 123);
            tag.PutInt("int", 12345);
            tag.PutLong("long", 123456789);

            JObject json = NBTJsonSerializer.Serialize(tag);
            Console.WriteLine(NBTJsonSerializer.Serialize(NBTJsonSerializer.Deserialize(json)).ToString());
        }
    }
}