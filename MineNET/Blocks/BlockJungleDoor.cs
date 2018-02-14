﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockJungleDoor : BlockDoorBase
    {
        public BlockJungleDoor() : base(BlockFactory.JUNGLE_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "JungleDoor";
            }
        }
    }
}
