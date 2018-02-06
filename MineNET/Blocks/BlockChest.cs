using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockChest : Block
    {
        public BlockChest() : base(BlockFactory.CHEST)
        {

        }

        public override string Name
        {
            get
            {
                return "Chest";
            }
        }
    }
}
