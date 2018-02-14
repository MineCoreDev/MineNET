using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStonePressurePlate : Block
    {
        public BlockStonePressurePlate() : base(BlockFactory.STONE_PRESSURE_PLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "StonePressurePlate";
            }
        }
    }
}
