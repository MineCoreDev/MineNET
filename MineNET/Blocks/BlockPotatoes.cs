using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPotatoes : BlockTransparent
    {
        public BlockPotatoes() : base(BlockFactory.POTATOES)
        {

        }

        public override string Name
        {
            get
            {
                return "Potatoes";
            }
        }
    }
}
