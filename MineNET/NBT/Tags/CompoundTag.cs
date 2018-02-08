using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class CompoundTag : Tag
    {
        private Dictionary<string, Tag> tags = new Dictionary<string, Tag>();

        public CompoundTag() : base("")
        {

        }

        public CompoundTag(string name) : base(name)
        {
            
        }

        public void PutInt(string name, int data)
        {
            if (this.Exist(name))
            {
                this[name] = new IntTag(data);
            }
            else this.tags.Add(name, new IntTag(name, data));
        }

        public int GetInt(string name)
        {
            if (this.Exist(name))
            {
                return ((IntTag)this[name]).Data;
            }
            else throw new IndexOutOfRangeException();
        }

        //TODO Add Other Type Put/Get

        /// <summary>
        /// この関数を連続的に呼び出すとパフォーマンスが低下する可能性があります。
        /// 代わりに GetInt(string) などを使用する事をおすすめします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetTag<T>(string name) where T : Tag
        {
            if (this.Exist(name))
            {
                return (T)Convert.ChangeType(this[name], typeof(T));
            }
            else throw new IndexOutOfRangeException();
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
                else throw new IndexOutOfRangeException();
            }

            set
            {
                if (this.Exist(key))
                {
                    this.tags[key] = value;
                }
                else throw new IndexOutOfRangeException();
            }
        }

        internal Dictionary<string, Tag> Tags
        {
            get
            {
                return this.tags;
            }

            set
            {
                this.tags = value;
            }
        }

        public override byte TagID
        {
            get
            {
                return TAG_COMPOUND;
            }
        }

        public override string ToString()
        {
            return $"CompoundTag : Name {this.Name}  : Data {this.Tags}";
        }
    }
}
