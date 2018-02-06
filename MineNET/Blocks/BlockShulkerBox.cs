using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockShulkerBox : BlockSolid
    {
        public BlockShulkerBox() : base(BlockFactory.SHULKER_BOX)
        {

        }

        public override string Name
        {
            get
            {
                return "ShulkerBox";
            }
        }
    }
}
