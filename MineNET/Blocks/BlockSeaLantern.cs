using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSeaLantern : BlockTransparent
    {
        public BlockSeaLantern() : base(BlockFactory.SEA_LANTERN)
        {

        }

        public override string Name
        {
            get
            {
                return "SeaLantern";
            }
        }
    }
}
