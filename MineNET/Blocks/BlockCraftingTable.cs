using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCraftingTable : BlockSolid
    {
        public BlockCraftingTable() : base(BlockFactory.CRAFTING_TABLE)
        {

        }

        public override string Name
        {
            get
            {
                return "CraftingTable";
            }
        }
    }
}
