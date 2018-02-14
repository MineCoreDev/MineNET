using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCarrots : BlockTransparent
    {
        public BlockCarrots() : base(BlockFactory.CARROTS)
        {

        }

        public override string Name
        {
            get
            {
                return "Carrots";
            }
        }
    }
}
