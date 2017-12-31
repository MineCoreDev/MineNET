using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFactory
    {
        public const int AIR = 0;

        public static Block[] blockFactory = new Block[256];

        public static void Init()
        {
            blockFactory[BlockAir.ID] = new BlockAir();
        }
    }
}
