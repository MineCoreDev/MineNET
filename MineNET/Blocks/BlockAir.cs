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
            Name = "Air";
            IsTransparent = true;
        }
    }
}
