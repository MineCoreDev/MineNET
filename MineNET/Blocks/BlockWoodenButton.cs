using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWoodenButton : Block
    {
        public BlockWoodenButton() : base(BlockFactory.WOODEN_BUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenButton";
            }
        }
    }
}
