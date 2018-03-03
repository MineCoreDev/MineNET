using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStonebrick : BlockSolid
    {
        public BlockStonebrick() : base(BlockFactory.STONEBRICK)
        {

        }

        public override string Name
        {
            get
            {
                return "Stonebrick";
            }
        }
    }
}
