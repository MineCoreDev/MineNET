using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGlowstone : BlockTransparent
    {
        public BlockGlowstone() : base(BlockFactory.GLOWSTONE)
        {

        }

        public override string Name
        {
            get
            {
                return "Glowstone";
            }
        }
    }
}
