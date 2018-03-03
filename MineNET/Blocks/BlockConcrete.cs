using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockConcrete : BlockSolid
    {
        public BlockConcrete() : base(BlockFactory.CONCRETE)
        {

        }

        public override string Name
        {
            get
            {
                return "Concrete";
            }
        }
    }
}
