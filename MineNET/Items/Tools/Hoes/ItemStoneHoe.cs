﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStoneHoe : ItemHoe
    {
        public override int ID { get; } = ItemIDs.STONE_HOE;

        public override string GetName(int damage)
        {
            return "Stone Hoe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.STONE;

        public override int MaxDurability { get; } = 132;
    }
}