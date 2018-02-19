using System;
using System.Collections.Generic;

namespace MineNET.NBT.Tags
{
    public class CompoundTag : Tag
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.COMPOUND;
            }
        }

        readonly Dictionary<string, Tag> tags = new Dictionary<string, Tag>();

        public CompoundTag() : base("")
        {

        }

        public CompoundTag(string name) : base(name)
        {

        }

        public CompoundTag PutByte(string name, byte data)
        {
            this.tags[name] = new ByteTag(name, data);
            return this;
        }

        public byte GetByte(string name)
        {
            if (this.Exist(name))
            {
                return ((ByteTag) this.tags[name]).Data;
            }
            else
            {
                return 0;
            }
        }

        public CompoundTag PutShort(string name, short data)
        {
            this.tags[name] = new ShortTag(name, data);
            return this;
        }

        public short GetShort(string name)
        {
            if (this.Exist(name))
            {
                return ((ShortTag) this.tags[name]).Data;
            }
            else
            {
                return 0;
            }
        }

        public CompoundTag PutInt(string name, int data)
        {
            this.tags[name] = new IntTag(name, data);
            return this;
        }

        public int GetInt(string name)
        {
            if (this.Exist(name))
            {
                return ((IntTag) this.tags[name]).Data;
            }
            else
            {
                return 0;
            }
        }

        public CompoundTag PutLong(string name, long data)
        {
            this.tags[name] = new LongTag(name, data);
            return this;
        }

        public long GetLong(string name)
        {
            if (this.Exist(name))
            {
                return ((LongTag) this.tags[name]).Data;
            }
            else
            {
                return 0;
            }
        }

        public CompoundTag PutFloat(string name, float data)
        {
            this.tags[name] = new FloatTag(name, data);
            return this;
        }

        public float GetFloat(string name)
        {
            if (this.Exist(name))
            {
                return ((FloatTag) this.tags[name]).Data;
            }
            else
            {
                return 0;
            }
        }

        public CompoundTag PutDouble(string name, double data)
        {
            this.tags[name] = new DoubleTag(name, data);
            return this;
        }

        public double GetDouble(string name)
        {
            if (this.Exist(name))
            {
                return ((DoubleTag) this.tags[name]).Data;
            }
            else
            {
                return 0;
            }
        }

        public CompoundTag PutString(string name, string data)
        {
            this.tags[name] = new StringTag(name, data);
            return this;
        }

        public string GetString(string name)
        {
            if (this.Exist(name))
            {
                return ((StringTag) this.tags[name]).Data;
            }
            else
            {
                return "";
            }
        }

        public CompoundTag PutBool(string name, bool data)
        {
            this.tags[name] = new ByteTag(name, data ? (byte) 1 : (byte) 0);
            return this;
        }

        public bool GetBool(string name)
        {
            return this.GetByte(name) != 0;
        }

        public CompoundTag PutByteArray(string name, byte[] data)
        {
            this.tags[name] = new ByteArrayTag(name, data);
            return this;
        }

        public byte[] GetByteArray(string name)
        {
            if (this.Exist(name))
            {
                return ((ByteArrayTag) this.tags[name]).Data;
            }
            else
            {
                return new byte[0];
            }
        }

        public CompoundTag PutIntArray(string name, int[] data)
        {
            this.tags[name] = new IntArrayTag(name, data);
            return this;
        }

        public int[] GetIntArray(string name)
        {
            if (this.Exist(name))
            {
                return ((IntArrayTag) this.tags[name]).Data;
            }
            else
            {
                return new int[0];
            }
        }

        public CompoundTag PutLongArray(string name, long[] data)
        {
            this.tags[name] = new LongArrayTag(name, data);
            return this;
        }

        public long[] GetLongArray(string name)
        {
            if (this.Exist(name))
            {
                return ((LongArrayTag) this.tags[name]).Data;
            }
            else
            {
                return new long[0];
            }
        }

        public CompoundTag PutList(string name, ListTag<Tag> data)
        {
            data.Name = name;
            this.tags[name] = data;
            return this;
        }

        public ListTag<T> GetList<T>(string name) where T : Tag
        {
            if (this.Exist(name))
            {
                return (ListTag<T>) this.tags[name];
            }
            else
            {
                return new ListTag<T>();
            }
        }

        public CompoundTag PutCompound(string name, CompoundTag data)
        {
            data.Name = name;
            this.tags[name] = data;
            return this;
        }

        public CompoundTag GetCompound(string name)
        {
            if (this.Exist(name))
            {
                return (CompoundTag) this.tags[name];
            }
            else
            {
                return new CompoundTag();
            }
        }

        public void PutTag(string name, Tag tag)
        {
            this.tags[name] = tag;
        }

        public T GetTag<T>(string name) where T : Tag
        {
            if (this.Exist(name))
            {
                return (T) this[name];
            }
            else
                throw new IndexOutOfRangeException();
        }

        public bool Exist(string name)
        {
            return this.tags.ContainsKey(name);
        }

        public int Count
        {
            get
            {
                return this.tags.Count;
            }
        }

        public Tag this[string key]
        {
            get
            {
                if (this.Exist(key))
                {
                    return this.tags[key];
                }
                else
                    throw new IndexOutOfRangeException();
            }

            set
            {
                if (this.Exist(key))
                {
                    this.tags[key] = value;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public Dictionary<string, Tag> Tags
        {
            get
            {
                return this.tags;
            }
        }

        public override string ToString()
        {
            return $"CompoundTag : Name {this.Name}  : Data {this.Tags}";
        }

        internal override void Write(NBTStream stream)
        {
            foreach (Tag t in this.tags.Values)
            {
                t.WriteTag(stream);
            }
            stream.WriteByte((byte) NBTTagType.END);
        }

        internal override void WriteTag(NBTStream stream)
        {
            if (Name != null)
            {
                stream.WriteByte((byte) this.TagType);
                stream.WriteString(this.Name);
                this.Write(stream);
            }
        }

        internal override void Read(NBTStream stream)
        {
            while (stream.Position != stream.Length)
            {
                NBTTagType type = (NBTTagType) stream.ReadByte();
                string tagName = "";
                int len = 0;
                switch (type)
                {
                    case NBTTagType.END:
                        return;

                    case NBTTagType.BYTE:
                        tagName = stream.ReadString();
                        PutByte(tagName, stream.ReadByte());
                        break;

                    case NBTTagType.SHORT:
                        tagName = stream.ReadString();
                        PutShort(tagName, stream.ReadShort());
                        break;

                    case NBTTagType.INT:
                        tagName = stream.ReadString();
                        PutInt(tagName, stream.ReadInt());
                        break;

                    case NBTTagType.LONG:
                        tagName = stream.ReadString();
                        PutLong(tagName, stream.ReadLong());
                        break;

                    case NBTTagType.FLOAT:
                        tagName = stream.ReadString();
                        PutFloat(tagName, stream.ReadFloat());
                        break;

                    case NBTTagType.DOUBLE:
                        tagName = stream.ReadString();
                        PutDouble(tagName, stream.ReadDouble());
                        break;

                    case NBTTagType.BYTE_ARRAY:
                        tagName = stream.ReadString();
                        len = stream.ReadInt();
                        byte[] b = new byte[len];
                        for (int i = 0; i < len; ++i)
                        {
                            b[i] = stream.ReadByte();
                        }
                        PutByteArray(tagName, b);
                        break;

                    case NBTTagType.STRING:
                        tagName = stream.ReadString();
                        PutString(tagName, stream.ReadString());
                        break;

                    case NBTTagType.LIST:
                        tagName = stream.ReadString();
                        ListTag<Tag> listtag = new ListTag<Tag>();
                        listtag.Read(stream);
                        PutList(tagName, listtag);
                        break;

                    case NBTTagType.COMPOUND:
                        tagName = stream.ReadString();
                        CompoundTag comp = new CompoundTag();
                        comp.Read(stream);
                        PutCompound(tagName, comp);
                        break;

                    case NBTTagType.INT_ARRAY:
                        tagName = stream.ReadString();
                        len = stream.ReadInt();
                        int[] n = new int[len];
                        for (int i = 0; i < len; ++i)
                        {
                            n[i] = stream.ReadInt();
                        }
                        PutIntArray(tagName, n);
                        break;

                    case NBTTagType.LONG_ARRAY:
                        tagName = stream.ReadString();
                        len = stream.ReadInt();
                        long[] l = new long[len];
                        for (int i = 0; i < len; ++i)
                        {
                            l[i] = stream.ReadLong();
                        }
                        PutLongArray(tagName, l);
                        break;

                    default:
                        throw new FormatException();
                }
            }
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }
    }
}
