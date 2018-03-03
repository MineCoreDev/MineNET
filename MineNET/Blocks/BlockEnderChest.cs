using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEnderChest : Block
    {
        public BlockEnderChest() : base(BlockFactory.ENDER_CHEST)
        {

        }

        public override string Name
        {
            get
            {
                return "EnderChest";
            }
        }
    }
}
