using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockNetherrack : BlockSolid
    {
        public BlockNetherrack() : base(BlockFactory.NETHERRACK)
        {

        }

        public override string Name
        {
            get
            {
                return "Netherrack";
            }
        }
    }
}
