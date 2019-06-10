﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherHelmet : ItemHelmet
    {
        public override int ID { get; } = ItemIDs.LEATHER_HELMET;

        public override string GetName(int damage)
        {
            return "Leather Helmet";
        }

        public override int MaxDurability { get; } = 55;
    }
}
