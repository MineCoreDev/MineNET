using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGrass : BlockSolid
    {
        public BlockGrass() : base(BlockFactory.GRASS)
        {

        }

        public override string Name
        {
            get
            {
                return "Grass";
            }
        }
    }
}
