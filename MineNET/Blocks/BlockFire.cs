using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFire : Block
    {
        public BlockFire() : base(BlockFactory.FIRE)
        {

        }

        public override string Name
        {
            get
            {
                return "Fire";
            }
        }
    }
}
