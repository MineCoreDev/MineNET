using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLog : BlockSolid
    {
        public BlockLog() : base(BlockFactory.LOG)
        {

        }

        public override string Name
        {
            get
            {
                return "Log";
            }
        }
    }
}
