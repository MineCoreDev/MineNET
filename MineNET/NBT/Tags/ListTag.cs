using System;
using System.Collections.Generic;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public class ListTag : Tag
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.LIST;
            }
        }

        public NBTTagType ListTagType { get; set; }

        private List<Tag> list = new List<Tag>();

        public ListTag(NBTTagType type) : base("")
        {
            this.ListTagType = type;
        }

        public ListTag(string name, NBTTagType type) : base(name)
        {
            this.ListTagType = type;
        }

        public ListTag Add(Tag tag)
        {
            if (tag.TagType == this.ListTagType)
            {
                this.list.Add(tag);
                return this;
            }
            else
                throw new FormatException();
        }

        public T GetTag<T>(int index) where T : Tag
        {
            if (this.Exist(index))
            {
                return (T) this[index];
            }
            else
                throw new IndexOutOfRangeException();
        }

        public bool Exist(int index)
        {
            return index < this.list.Count;
        }

        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        public Tag this[int index]
        {
            get
            {
                if (this.Exist(index))
                {
                    return this.list[index];
                }
                else
                    throw new IndexOutOfRangeException();
            }

            set
            {
                if (this.Exist(index))
                {
                    if (value.TagType == this.ListTagType)
                    {
                        this.list[index] = value;
                    }
                    else
                        throw new FormatException();

                }
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public List<Tag> Tags
        {
            get
            {
                return this.list;
            }

            set
            {
                this.list = value;
            }
        }

        public override string ToString()
        {
            string data = "";
            foreach (Tag tag in this.Tags)
            {
                data += $"{Environment.NewLine}{tag.ToString()}";
            }
            return $"ListTag : Name {this.Name} : Data {data}";
        }

        internal override void Write(NBTStream stream)
        {
            stream.WriteByte((byte) this.ListTagType);
            stream.WriteInt(this.Tags.Count);
            for (int i = 0; i < this.Tags.Count; ++i)
            {
                this.Tags[i].Write(stream);
            }
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
            this.ListTagType = (NBTTagType) stream.ReadByte();
            int len = stream.ReadInt();
            for (int i = 0; i < len; ++i)
            {
                Tag tag = null;
                switch (this.ListTagType)
                {
                    case NBTTagType.BYTE:
                        tag = new ByteTag(0);
                        break;

                    case NBTTagType.SHORT:
                        tag = new ShortTag(0);
                        break;

                    case NBTTagType.INT:
                        tag = new IntTag(0);
                        break;

                    case NBTTagType.LONG:
                        tag = new LongTag(0);
                        break;

                    case NBTTagType.FLOAT:
                        tag = new FloatTag(0);
                        break;

                    case NBTTagType.DOUBLE:
                        tag = new DoubleTag(0);
                        break;

                    case NBTTagType.BYTE_ARRAY:
                        tag = new ByteArrayTag(new byte[0]);
                        break;

                    case NBTTagType.STRING:
                        tag = new StringTag("");
                        break;

                    case NBTTagType.LIST:
                        tag = new ListTag(NBTTagType.LIST);
                        break;

                    case NBTTagType.COMPOUND:
                        tag = new CompoundTag();
                        break;

                    case NBTTagType.INT_ARRAY:
                        tag = new IntArrayTag(new int[0]);
                        break;

                    case NBTTagType.LONG_ARRAY:
                        tag = new LongArrayTag(new long[0]);
                        break;

                    default:
                        throw new NotSupportedException();
                }

                tag.Read(stream);
                this.list.Add(tag);
            }
        }

        internal override void ReadTag(NBTStream stream)
        {
            this.Name = stream.ReadString();
            this.Read(stream);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ListTag))
            {
                return false;
            }
            ListTag tag = (ListTag) obj;
            if (this.Name != tag.Name)
            {
                return false;
            }
            if (this.list.Count != tag.list.Count)
            {
                return false;
            }
            for (int i = 0; i < this.list.Count; ++i)
            {
                if (!this.list[i].Equals(tag.list[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator ==(ListTag A, ListTag B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(ListTag A, ListTag B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
