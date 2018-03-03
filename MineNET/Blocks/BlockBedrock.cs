using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBedrock : BlockSolid
    {
        public BlockBedrock() : base(BlockFactory.BEDROCK)
        {

        }

        public override string Name
        {
            get
            {
                return "Bedrock";
            }
        }
    }
}
