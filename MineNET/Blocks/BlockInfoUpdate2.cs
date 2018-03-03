using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockInfoUpdate2 : BlockSolid
    {
        public BlockInfoUpdate2() : base(BlockFactory.INFO_UPDATE2)
        {

        }

        public override string Name
        {
            get
            {
                return "InfoUpdate2";
            }
        }
    }
}
