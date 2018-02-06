using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockJukebox : BlockSolid
    {
        public BlockJukebox() : base(BlockFactory.JUKEBOX)
        {

        }

        public override string Name
        {
            get
            {
                return "Jukebox";
            }
        }
    }
}
