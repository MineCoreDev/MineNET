﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenPickaxe : ItemPickaxe
    {
        public override int ID { get; } = ItemIDs.WOODEN_PICKAXE;

        public override string GetName(int damage)
        {
            return "Wooden Pickaxe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.WOODEN;

        public override int MaxDurability { get; } = 60;
    }
}