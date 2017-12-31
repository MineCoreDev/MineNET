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

        public static List<Block> blockFactory = new List<Block>();

        public static void Init()
        {
            blockFactory.Add(new BlockAir());

            blockFactory.Sort();
        }
    }
}
