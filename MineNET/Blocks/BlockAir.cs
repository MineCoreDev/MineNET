using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockAir : Block
    {
        public BlockAir() : base(BlockFactory.AIR)
        {
        }

        public override bool IsTransparent
        {
            get
            {
                return true;
            }
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
