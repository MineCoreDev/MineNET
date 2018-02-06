using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCarpet : BlockTransparent
    {
        public BlockCarpet() : base(BlockFactory.CARPET)
        {

        }

        public override string Name
        {
            get
            {
                return "Carpet";
            }
        }
    }
}
