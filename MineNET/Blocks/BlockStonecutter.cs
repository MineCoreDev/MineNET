using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStonecutter : BlockSolid
    {
        public BlockStonecutter() : base(BlockFactory.STONECUTTER)
        {

        }

        public override string Name
        {
            get
            {
                return "Stonecutter";
            }
        }
    }
}
