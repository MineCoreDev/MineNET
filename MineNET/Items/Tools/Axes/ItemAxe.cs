﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemAxe : ItemTool
    {
        public override ItemToolType ToolType { get; } = ItemToolType.AXE;
    }
}
