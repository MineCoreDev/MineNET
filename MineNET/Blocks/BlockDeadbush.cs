using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDeadbush : Block
    {
        public BlockDeadbush() : base(BlockFactory.DEADBUSH)
        {

        }

        public override string Name
        {
            get
            {
                return "Deadbush";
            }
        }
    }
}
