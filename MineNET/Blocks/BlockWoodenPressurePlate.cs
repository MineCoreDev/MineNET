using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWoodenPressurePlate : Block
    {
        public BlockWoodenPressurePlate() : base(BlockFactory.WOODEN_PRESSURE_PLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenPressurePlate";
            }
        }
    }
}
