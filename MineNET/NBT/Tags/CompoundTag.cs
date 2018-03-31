using System;
using System.Collections.Generic;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

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

        public CompoundTag PutByte(string name, byte data)
        {
            this.tags[name] = new ByteTag(name, data);
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

        public CompoundTag PutShort(string name, short data)
        {
            this.tags[name] = new ShortTag(name, data);
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

        public CompoundTag PutInt(string name, int data)
        {
            this.tags[name] = new IntTag(name, data);
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

        public CompoundTag PutLong(string name, long data)
        {
            this.tags[name] = new LongTag(name, data);
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

        public CompoundTag PutFloat(string name, float data)
        {
            this.tags[name] = new FloatTag(name, data);
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

        public CompoundTag PutDouble(string name, double data)
        {
            this.tags[name] = new DoubleTag(name, data);
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

        public CompoundTag PutString(string name, string data)
        {
            this.tags[name] = new StringTag(name, data);
            return this;
        }

        public bool GetBool(string name)
        {
            return this.GetByte(name) != 0;
        }

        public CompoundTag PutBool(string name, bool data)
        {
            this.tags[name] = new ByteTag(name, data ? (byte) 1 : (byte) 0);
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

        public CompoundTag PutByteArray(string name, byte[] data)
        {
            this.tags[name] = new ByteArrayTag(name, data);
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

        public CompoundTag PutIntArray(string name, int[] data)
        {
            this.tags[name] = new IntArrayTag(name, data);
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

        public CompoundTag PutLongArray(string name, long[] data)
        {
            this.tags[name] = new LongArrayTag(name, data);
            return this;
        }

        public ListTag GetList(string name)
        {
            if (this.Exist(name))
            {
                return ((ListTag) this.tags[name]);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public CompoundTag PutList(ListTag data)
        {
            this.tags[data.Name] = data;
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

        public CompoundTag PutCompound(string name, CompoundTag data)
        {
            data.Name = name;
            this.tags[name] = data;
            return this;
        }

        public Tag GetTag(string name)
        {
            if (this.Exist(name))
            {
                return this[name];
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void PutTag(string name, Tag tag)
        {
            this.tags[name] = tag;
        }

        public void Remove(string name)
        {
            this.tags.Remove(name);
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
            string data = "";
            foreach (Tag tag in this.tags.Values)
            {
                data += $"{Environment.NewLine}{tag.ToString()}";
            }
            return $"CompoundTag : Name {this.Name} : Data {data}";
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
            if (this.Name != null)
            {
                stream.WriteByte((byte) this.TagType);
                stream.WriteString(this.Name);
                this.Write(stream);
            }
            else
            {
                throw new NullReferenceException("Tag Name Null");
            }
        }

        internal override void Read(NBTStream stream)
        {
            while (stream.Offset != stream.Length)
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
                        this.PutByte(tagName, stream.ReadByte());
                        break;

                    case NBTTagType.SHORT:
                        tagName = stream.ReadString();
                        this.PutShort(tagName, stream.ReadShort());
                        break;

                    case NBTTagType.INT:
                        tagName = stream.ReadString();
                        this.PutInt(tagName, stream.ReadInt());
                        break;

                    case NBTTagType.LONG:
                        tagName = stream.ReadString();
                        this.PutLong(tagName, stream.ReadLong());
                        break;

                    case NBTTagType.FLOAT:
                        tagName = stream.ReadString();
                        this.PutFloat(tagName, stream.ReadFloat());
                        break;

                    case NBTTagType.DOUBLE:
                        tagName = stream.ReadString();
                        this.PutDouble(tagName, stream.ReadDouble());
                        break;

                    case NBTTagType.BYTE_ARRAY:
                        tagName = stream.ReadString();
                        len = stream.ReadInt();
                        byte[] b = new byte[len];
                        for (int i = 0; i < len; ++i)
                        {
                            b[i] = stream.ReadByte();
                        }
                        this.PutByteArray(tagName, b);
                        break;

                    case NBTTagType.STRING:
                        tagName = stream.ReadString();
                        this.PutString(tagName, stream.ReadString());
                        break;

                    case NBTTagType.LIST:
                        tagName = stream.ReadString();
                        ListTag listtag = new ListTag(NBTTagType.BYTE);
                        listtag.Read(stream);
                        listtag.Name = tagName;
                        this.PutList(listtag);
                        break;

                    case NBTTagType.COMPOUND:
                        tagName = stream.ReadString();
                        CompoundTag comp = new CompoundTag();
                        comp.Read(stream);
                        this.PutCompound(tagName, comp);
                        break;

                    case NBTTagType.INT_ARRAY:
                        tagName = stream.ReadString();
                        len = stream.ReadInt();
                        int[] n = new int[len];
                        for (int i = 0; i < len; ++i)
                        {
                            n[i] = stream.ReadInt();
                        }
                        this.PutIntArray(tagName, n);
                        break;

                    case NBTTagType.LONG_ARRAY:
                        tagName = stream.ReadString();
                        len = stream.ReadInt();
                        long[] l = new long[len];
                        for (int i = 0; i < len; ++i)
                        {
                            l[i] = stream.ReadLong();
                        }
                        this.PutLongArray(tagName, l);
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

        public override bool Equals(object obj)
        {
            return Equals(this, obj);
        }

        public new static bool Equals(object objA, object objB)
        {
            if (!(objA is CompoundTag) && !(objB is CompoundTag))
            {
                return true;
            }
            else if (!(objA is CompoundTag) || !(objB is CompoundTag))
            {
                return false;
            }
            CompoundTag tagA = (CompoundTag) objA;
            CompoundTag tagB = (CompoundTag) objB;
            if (tagA.Name != tagB.Name)
            {
                return false;
            }
            if (tagA.tags.Count != tagB.tags.Count)
            {
                return false;
            }
            foreach (string key in tagA.tags.Keys)
            {
                if (!tagB.tags.ContainsKey(key))
                {
                    return false;
                }
                if (!tagA.tags[key].Equals(tagB.tags[key]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator ==(CompoundTag A, CompoundTag B)
        {
            return Equals(A, B);
        }

        public static bool operator !=(CompoundTag A, CompoundTag B)
        {
            return !Equals(A, B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
