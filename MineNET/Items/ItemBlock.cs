using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBlock : Item
    {
        public ItemBlock(Block block) : base(block.BlockID)
        {
            this.Block = block;
        }

        public override string Name
        {
            get
            {
                return this.Block.Name;
            }
        }
    }
}
