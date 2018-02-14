using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public abstract class BlockSolid : Block
    {
        public BlockSolid(byte id) : base(id)
        {

        }

        public override bool IsSolid
        {
            get
            {
                return true;
            }
        }
    }
}
