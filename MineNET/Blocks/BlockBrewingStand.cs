using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBrewingStand : Block
    {
        public BlockBrewingStand() : base(BlockFactory.BREWING_STAND)
        {

        }

        public override string Name
        {
            get
            {
                return "BrewingStand";
            }
        }
    }
}
