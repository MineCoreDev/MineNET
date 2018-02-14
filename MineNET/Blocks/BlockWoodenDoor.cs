﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWoodenDoor : BlockDoorBase
    {
        public BlockWoodenDoor() : base(BlockFactory.WOODEN_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenDoor";
            }
        }
    }
}
