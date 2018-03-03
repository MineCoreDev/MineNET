using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCake : Block
    {
        public BlockCake() : base(BlockFactory.CAKE)
        {

        }

        public override string Name
        {
            get
            {
                return "Cake";
            }
        }
    }
}
