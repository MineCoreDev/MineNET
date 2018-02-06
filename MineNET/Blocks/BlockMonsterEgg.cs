using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMonsterEgg : BlockSolid
    {
        public BlockMonsterEgg() : base(BlockFactory.MONSTER_EGG)
        {

        }

        public override string Name
        {
            get
            {
                return "MonsterEgg";
            }
        }
    }
}
