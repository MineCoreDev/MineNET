using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class Block
    {
        public static Block Get(int id)
        {
            return BlockFactory.blockFactory[id];
        }

        protected string name = "";

        protected int id = -1;
        protected int meta = 0;
        protected int count = 1;

        public Block(int id)
        {
            this.id = id;
        }

        public virtual string Name
        {
            get
            {
                return name;
            }
        }
    }
}
