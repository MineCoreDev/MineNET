using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPumpkin : BlockSolid
    {
        public BlockPumpkin() : base(BlockFactory.PUMPKIN)
        {

        }

        public override string Name
        {
            get
            {
                return "Pumpkin";
            }
        }
    }
}
