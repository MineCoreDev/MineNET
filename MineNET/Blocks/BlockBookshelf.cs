using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBookshelf : BlockSolid
    {
        public BlockBookshelf() : base(BlockFactory.BOOKSHELF)
        {

        }

        public override string Name
        {
            get
            {
                return "Bookshelf";
            }
        }
    }
}
