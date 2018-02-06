using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStructureBlock : BlockSolid
    {
        public BlockStructureBlock() : base(BlockFactory.STRUCTURE_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "StructureBlock";
            }
        }
    }
}
