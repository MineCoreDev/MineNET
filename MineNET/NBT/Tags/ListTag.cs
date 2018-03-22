using System;
using System.Collections.Generic;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public class ListTag<T> : Tag where T : Tag
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.LIST;
            }
        }

        public NBTTagType ListTagType { get; set; }

        private List<T> list = new List<T>();

        public ListTag() : base("")
        {
            this.CheckListTagType();
        }

        public ListTag(string name) : base(name)
        {
            this.CheckListTagType();
        }

        public ListTag<T> Add(T tag)
        {
            this.list.Add(tag);
            return this;
        }

        public T GetTag(int index)
        {
            if (this.Exist(index))
            {
                return this[index];
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

        public T this[int index]
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
                    this.list[index] = value;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public List<T> Tags
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
            return $"ListTag : Name {this.Name} : Data {this.Tags.ToString()}";
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
            if (!string.IsNullOrEmpty(this.Name))
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
                        tag = new ListTag<Tag>();
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
                this.list.Add((T) tag);
            }
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }

        internal void CheckListTagType()
        {
            if (typeof(T) == typeof(ByteTag))
            {
                this.ListTagType = NBTTagType.BYTE;
            }
            else if (typeof(T) == typeof(ByteArrayTag))
            {
                this.ListTagType = NBTTagType.BYTE_ARRAY;
            }
            else if (typeof(T) == typeof(CompoundTag))
            {
                this.ListTagType = NBTTagType.COMPOUND;
            }
            else if (typeof(T) == typeof(DoubleTag))
            {
                this.ListTagType = NBTTagType.DOUBLE;
            }
            else if (typeof(T) == typeof(EndTag))
            {
                this.ListTagType = NBTTagType.END;
            }
            else if (typeof(T) == typeof(FloatTag))
            {
                this.ListTagType = NBTTagType.FLOAT;
            }
            else if (typeof(T) == typeof(IntTag))
            {
                this.ListTagType = NBTTagType.INT;
            }
            else if (typeof(T) == typeof(IntArrayTag))
            {
                this.ListTagType = NBTTagType.INT_ARRAY;
            }
            else if (typeof(T) == typeof(ListTag<>))
            {
                this.ListTagType = NBTTagType.LIST;
            }
            else if (typeof(T) == typeof(LongTag))
            {
                this.ListTagType = NBTTagType.LONG;
            }
            else if (typeof(T) == typeof(LongArrayTag))
            {
                this.ListTagType = NBTTagType.LONG_ARRAY;
            }
            else if (typeof(T) == typeof(ShortTag))
            {
                this.ListTagType = NBTTagType.SHORT;
            }
            else if (typeof(T) == typeof(StringTag))
            {
                this.ListTagType = NBTTagType.STRING;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ListTag<T>))
            {
                return false;
            }
            ListTag<T> tag = (ListTag<T>) obj;
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

        public static bool operator ==(ListTag<T> A, ListTag<T> B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(ListTag<T> A, ListTag<T> B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
