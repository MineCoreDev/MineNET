using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockHeavyWeightedPressurePlate : Block
    {
        public BlockHeavyWeightedPressurePlate() : base(BlockFactory.HEAVY_WEIGHTED_PRESSURE_PLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "HeavyWeightedPressurePlate";
            }
        }
    }
}
