using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSnow : BlockSolid
    {
        public BlockSnow() : base(BlockFactory.SNOW)
        {

        }

        public override string Name
        {
            get
            {
                return "Snow";
            }
        }
    }
}
