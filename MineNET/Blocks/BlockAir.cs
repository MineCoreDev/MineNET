using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockAir : BlockTransparent
    {
        public BlockAir() : base(BlockFactory.AIR)
        {

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
