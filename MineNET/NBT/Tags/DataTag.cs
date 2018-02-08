using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public abstract class DataTag<T> : Tag
    {
        private T data;

        public DataTag(string name, T data) : base(name)
        {
            this.data = data;
        }

        public T Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }
    }
}
