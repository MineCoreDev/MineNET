using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFurnace : BlockSolid
    {
        public BlockFurnace() : base(BlockFactory.FURNACE)
        {

        }

        public override string Name
        {
            get
            {
                return "Furnace";
            }
        }
    }
}
