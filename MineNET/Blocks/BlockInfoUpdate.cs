using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockInfoUpdate : BlockSolid
    {
        public BlockInfoUpdate() : base(BlockFactory.INFO_UPDATE)
        {

        }

        public override string Name
        {
            get
            {
                return "InfoUpdate";
            }
        }
    }
}
