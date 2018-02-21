using System;
using System.Collections.Generic;

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

        public NBTTagType ListTagType
        {
            get;
            set;
        }

        private List<T> list = new List<T>();

        public ListTag() : base("")
        {

        }

        public ListTag(string name) : base(name)
        {

        }

        public ListTag<T> Add(T tag)
        {
            list.Add(tag);
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
                    return list[index];
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
            throw new NotImplementedException();
        }

        internal override void WriteTag(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        internal override void Read(NBTStream stream)
        {
            ListTagType = (NBTTagType) stream.ReadByte();
            int len = stream.ReadInt();
            for (int i = 0; i < len; ++i)
            {
                Tag tag = null;
                switch (ListTagType)
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
                }

                tag.Read(stream);
                list.Add((T) tag);
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
