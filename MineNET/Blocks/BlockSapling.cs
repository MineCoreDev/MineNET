using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSapling : BlockSolid
    {
        public BlockSapling() : base(BlockFactory.SAPLING)
        {

        }

        public override string Name
        {
            get
            {
                return "Sapling";
            }
        }
    }
}
