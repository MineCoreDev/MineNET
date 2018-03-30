using System.Collections.Generic;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using Newtonsoft.Json.Linq;

namespace MineNET.NBT.IO
{
    public static class NBTJsonSerializer
    {
        public static JObject Serialize(CompoundTag tag)
        {
            JObject json = NBTJsonSerializer.CompoundTagSerialize(tag);
            return json;
        }

        internal static JObject CompoundTagSerialize(CompoundTag tag)
        {
            JObject json = new JObject();
            foreach (KeyValuePair<string, Tag> kv in tag.Tags)
            {
                Tag t = kv.Value;
                if (t is ByteTag)
                {
                    json.Add(t.Name, new JValue(tag.GetByte(t.Name)));
                }
                else if (t is CompoundTag)
                {
                    json.Add(t.Name, NBTJsonSerializer.CompoundTagSerialize((CompoundTag) t));
                }
                else if (t is DoubleTag)
                {
                    json.Add(t.Name, new JValue(tag.GetDouble(t.Name)));
                }
                else if (t is FloatTag)
                {
                    json.Add(t.Name, new JValue(tag.GetFloat(t.Name)));
                }
                else if (t is IntTag)
                {
                    json.Add(t.Name, new JValue(tag.GetInt(t.Name)));
                }
                else if (t is ListTag)
                {
                    json.Add(t.Name, new JArray(NBTJsonSerializer.ListTagSerialize((ListTag)t)));
                }
                else if (t is LongTag)
                {
                    json.Add(t.Name, new JValue(tag.GetLong(t.Name)));
                }
                else if (t is ShortTag)
                {
                    json.Add(t.Name, new JValue(tag.GetShort(t.Name)));
                }
                else if (t is StringTag)
                {
                    json.Add(t.Name, new JValue(tag.GetString(t.Name)));
                }
            }

            return json;
        }

        internal static JArray ListTagSerialize(ListTag tag)
        {
            JArray json = new JArray();
            foreach (Tag t in tag.Tags)
            {
                if (t is ByteTag)
                {
                    json.Add(new JValue(((ByteTag) t).Data));
                }
                else if (t is CompoundTag)
                {
                    json.Add(NBTJsonSerializer.CompoundTagSerialize((CompoundTag) t));
                }
                else if (t is DoubleTag)
                {
                    json.Add(new JValue(((DoubleTag) t).Data));
                }
                else if (t is FloatTag)
                {
                    json.Add(new JValue(((FloatTag) t).Data));
                }
                else if (t is IntTag)
                {
                    json.Add(new JValue(((IntTag) t).Data));
                }
                else if (t is ListTag)
                {
                    json.Add(new JArray(NBTJsonSerializer.ListTagSerialize((ListTag) t)));
                }
                else if (t is LongTag)
                {
                    json.Add(new JValue(((LongTag) t).Data));
                }
                else if (t is ShortTag)
                {
                    json.Add(new JValue(((ShortTag) t).Data));
                }
                else if (t is StringTag)
                {
                    json.Add(new JValue(((StringTag) t).Data));
                }
            }

            return json;
        }

        public static CompoundTag Deserialize(JObject json)
        {
            CompoundTag tag = NBTJsonSerializer.CompoundTagDeserialize(json);
            return tag;
        }

        internal static CompoundTag CompoundTagDeserialize(JObject json)
        {
            CompoundTag tag = new CompoundTag();
            foreach (KeyValuePair<string, JToken> kv in json)
            {
                JToken token = kv.Value;
                if (token is JValue)
                {
                    JValue value = (JValue) token;
                    object t = value.Value;
                    if (t is byte)
                    {
                        tag.PutByte(kv.Key, (byte) t);
                    }
                    else if (t is double)
                    {
                        tag.PutDouble(kv.Key, (double) t);
                    }
                    else if (t is float)
                    {
                        tag.PutFloat(kv.Key, (float) t);
                    }
                    else if (t is int)
                    {
                        tag.PutInt(kv.Key, (int) t);
                    }
                    else if (t is long)
                    {
                        tag.PutLong(kv.Key, (long) t);
                    }
                    else if (t is short)
                    {
                        tag.PutShort(kv.Key, (short) t);
                    }
                    else if (t is string)
                    {
                        tag.PutString(kv.Key, (string) t);
                    }
                }
                else if (token is JObject)
                {
                    tag.PutCompound(kv.Key, NBTJsonSerializer.CompoundTagDeserialize((JObject) token));
                }
                else
                {
                    tag.PutList(NBTJsonSerializer.ListTagDeserialize((JArray) token, kv.Key));
                }
            }

            return tag;
        }

        internal static ListTag ListTagDeserialize(JArray json, string key = "")
        {
            ListTag tag = new ListTag(key, NBTTagType.BYTE);
            foreach (JToken token in json)
            {
                if (token is JValue)
                {
                    JValue value = (JValue) token;
                    object t = value.Value;
                    if (t is byte)
                    {
                        tag.ListTagType = NBTTagType.BYTE;
                        tag.Add(new ByteTag((byte) t));
                    }
                    else if (t is double)
                    {
                        tag.ListTagType = NBTTagType.DOUBLE;
                        tag.Add(new DoubleTag((double) t));
                    }
                    else if (t is float)
                    {
                        tag.ListTagType = NBTTagType.FLOAT;
                        tag.Add(new FloatTag((float) t));
                    }
                    else if (t is int)
                    {
                        tag.ListTagType = NBTTagType.INT;
                        tag.Add(new IntTag((int) t));
                    }
                    else if (t is long)
                    {
                        tag.ListTagType = NBTTagType.LONG;
                        tag.Add(new LongTag((long) t));
                    }
                    else if (t is short)
                    {
                        tag.ListTagType = NBTTagType.SHORT;
                        tag.Add(new ShortTag((short) t));
                    }
                    else if (t is string)
                    {
                        tag.ListTagType = NBTTagType.STRING;
                        tag.Add(new StringTag((string) t));
                    }
                }
                else if (token is JObject)
                {
                    tag.Add(NBTJsonSerializer.CompoundTagDeserialize((JObject) token));
                }
                else
                {
                    tag.Add(NBTJsonSerializer.ListTagDeserialize((JArray) token));
                }
            }

            return tag;
        }
    }
}
