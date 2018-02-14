using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDoublePlant : BlockTransparent
    {
        public BlockDoublePlant() : base(BlockFactory.DOUBLE_PLANT)
        {

        }

        public override string Name
        {
            get
            {
                return "DoublePlant";
            }
        }
    }
}
