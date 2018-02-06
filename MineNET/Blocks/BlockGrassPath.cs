using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGrassPath : BlockSolid
    {
        public BlockGrassPath() : base(BlockFactory.GRASS_PATH)
        {

        } 

        public override string Name
        {
            get
            {
                return "GrassPath";
            }
        }
    }
}
