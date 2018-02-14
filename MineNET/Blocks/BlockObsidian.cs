using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockObsidian : BlockSolid
    {
        public BlockObsidian() : base(BlockFactory.OBSIDIAN)
        {

        }

        public override string Name
        {
            get
            {
                return "Obsidian";
            }
        }
    }
}
