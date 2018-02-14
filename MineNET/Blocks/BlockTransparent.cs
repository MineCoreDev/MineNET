using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public abstract class BlockTransparent : Block
    {
        public BlockTransparent(byte id) : base(id)
        {

        }

        public override bool IsTransparent
        {
            get
            {
                return true;
            }
        }
    }
}
