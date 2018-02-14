using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBed : Block
    {
        public BlockBed() : base(BlockFactory.BED)
        {

        }

        public override string Name
        {
            get
            {
                return "Bed";
            }
        }
    }
}
