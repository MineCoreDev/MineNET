using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCauldron : BlockTransparent
    {
        public BlockCauldron() : base(BlockFactory.CAULDRON)
        {

        }

        public override string Name
        {
            get
            {
                return "Cauldron";
            }
        }
    }
}
