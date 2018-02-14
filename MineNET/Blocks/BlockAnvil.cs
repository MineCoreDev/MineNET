using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockAnvil : Block
    {
        public BlockAnvil() : base(BlockFactory.ANVIL)
        {

        }

        public override string Name
        {
            get
            {
                return "Anvil";
            }
        }
    }
}
