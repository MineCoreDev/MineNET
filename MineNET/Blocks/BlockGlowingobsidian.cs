using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGlowingobsidian : BlockSolid
    {
        public BlockGlowingobsidian() : base(BlockFactory.GLOWINGOBSIDIAN)
        {

        }

        public override string Name
        {
            get
            {
                return "Glowingobsidian";
            }
        }
    }
}
