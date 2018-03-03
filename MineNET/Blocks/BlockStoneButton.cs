using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStoneButton : BlockTransparent
    {
        public BlockStoneButton() : base(BlockFactory.STONE_BUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneButton";
            }
        }
    }
}
