using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedstoneLamp : BlockSolid
    {
        public BlockRedstoneLamp() : base(BlockFactory.REDSTONE_LAMP)
        {

        }

        public override string Name
        {
            get
            {
                return "RedstoneLamp";
            }
        }
    }
}
