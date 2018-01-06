using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    class DoubleTag : Tag
    {
        private double data;

        public DoubleTag(double data) : this("", data)
        {

        }

        public DoubleTag(string name, double data) : base(name)
        {

        }

        public Double Data
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
