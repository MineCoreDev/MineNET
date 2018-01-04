using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    class BlockUnknown : Block
    {
        public BlockUnknown(byte id) : base(id)
        {

        }

        public override string Name
        {
            get
            {
                return "Unknown";
            }
        }
    }
}
