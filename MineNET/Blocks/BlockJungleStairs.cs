﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockJungleStairs : BlockStairsBase
    {
        public BlockJungleStairs() : base(BlockFactory.JUNGLE_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "JungleStairs";
            }
        }
    }
}
