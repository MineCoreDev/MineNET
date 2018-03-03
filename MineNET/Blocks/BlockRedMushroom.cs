using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedMushroom : Block
    {
        public BlockRedMushroom() : base(BlockFactory.RED_MUSHROOM)
        {

        }

        public override string Name
        {
            get
            {
                return "RedMushroom";
            }
        }
    }
}
