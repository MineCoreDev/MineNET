using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class ListTag<T> : Tag where T : Tag
    {
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
            else throw new IndexOutOfRangeException();
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
                else throw new IndexOutOfRangeException();
            }

            set
            {
                if (this.Exist(index))
                {
                    this.list[index] = value;
                }
                else throw new IndexOutOfRangeException();
            }
        }

        internal List<T> Tags
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

        public override byte TagID
        {
            get
            {
                return TAG_LIST;
            }
        }

        public override string ToString()
        {
            return $"ListTag : Name {this.Name}  : Data {this.Tags.ToString()}";
        }
    }
}
