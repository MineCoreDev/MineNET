using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDropper : BlockSolid
    {
        public BlockDropper() : base(BlockFactory.DROPPER)
        {

        }

        public override string Name
        {
            get
            {
                return "Dropper";
            }
        }
    }
}
