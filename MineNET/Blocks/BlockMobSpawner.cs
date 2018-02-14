using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMobSpawner : BlockSolid
    {
        public BlockMobSpawner() : base(BlockFactory.MOB_SPAWNER)
        {

        }

        public override string Name
        {
            get
            {
                return "MobSpawner";
            }
        }
    }
}
