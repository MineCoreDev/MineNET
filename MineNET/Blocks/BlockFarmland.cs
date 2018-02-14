using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFarmland : Block
    {
        public BlockFarmland() : base(BlockFactory.FARMLAND)
        {

        }

        public override string Name
        {
            get
            {
                return "Farmland";
            }
        }
    }
}
