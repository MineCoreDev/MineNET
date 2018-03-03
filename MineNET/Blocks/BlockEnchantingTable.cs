using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEnchantingTable : Block
    {
        public BlockEnchantingTable() : base(BlockFactory.ENCHANTING_TABLE)
        {

        }

        public override string Name
        {
            get
            {
                return "EnchantingTable";
            }
        }
    }
}
