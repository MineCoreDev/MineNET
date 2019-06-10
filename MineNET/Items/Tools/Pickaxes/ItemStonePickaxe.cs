﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStonePickaxe : ItemPickaxe
    {
        public override int ID { get; } = ItemIDs.STONE_PICKAXE;

        public override string GetName(int damage)
        {
            return "Stone Pickaxe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.STONE;

        public override int MaxDurability { get; } = 132;
    }
}
