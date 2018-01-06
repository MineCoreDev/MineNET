using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class IntTag : Tag
    {
        private int data;

        public IntTag(int data) : this("", data)
        {
            
        }

        public IntTag(string name, int data) : base(name)
        {
            this.data = data;
        }

        public int Data
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
