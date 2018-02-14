using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockIronTrapdoor : Block
    {
        public BlockIronTrapdoor() : base(BlockFactory.IRON_TRAPDOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "IronTrapdoor";
            }
        }
    }
}
