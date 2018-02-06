using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockInvisiblebedrock : BlockSolid
    {
        public BlockInvisiblebedrock() : base(BlockFactory.INVISIBLEBEDROCK)
        {

        }

        public override string Name
        {
            get
            {
                return "Invisiblebedrock";
            }
        }
    }
}
