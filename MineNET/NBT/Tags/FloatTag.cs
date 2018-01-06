using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class FloatTag : Tag
    {
        private float data;

        public FloatTag(float data) : this("", data)
        {

        }

        public FloatTag(string name, float data) : base(name)
        {
            this.data = data;
        }

        public float Data
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
