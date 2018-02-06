using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLightWeightedPressurePlate : Block
    {
        public BlockLightWeightedPressurePlate() : base(BlockFactory.LIGHT_WEIGHTED_PRESSURE_PLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "LightWeightedPressurePlate";
            }
        }
    }
}
