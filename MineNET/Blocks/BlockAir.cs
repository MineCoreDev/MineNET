using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockAir : Block
    {
        public const int ID = BlockFactory.AIR;

        public BlockAir() : base(ID)
        {
            id = ID;
            name = Name;
        }

        public override string Name
        {
            get
            {
                return "Air";
            }
        }
    }
}
