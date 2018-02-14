using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockChorusPlant : Block
    {
        public BlockChorusPlant() : base(BlockFactory.CHORUS_PLANT)
        {

        }

        public override string Name
        {
            get
            {
                return "ChorusPlant";
            }
        }
    }
}
