using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockObserver : BlockSolid
    {
        public BlockObserver() : base(BlockFactory.OBSERVER)
        {

        }

        public override string Name
        {
            get
            {
                return "Observer";
            }
        }
    }
}
