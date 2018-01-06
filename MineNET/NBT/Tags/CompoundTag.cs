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
            this.tags.Add(name, new IntTag(name, data));
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

        public bool Exist(string name)
        {
            return this.tags.ContainsKey(name);
        }
    }
}
