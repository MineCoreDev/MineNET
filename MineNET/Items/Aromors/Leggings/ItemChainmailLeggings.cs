﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailLeggings : ItemLeggings
    {
        public override int ID { get; } = ItemIDs.CHAINMAIL_LEGGINGS;

        public override string GetName(int damage)
        {
            return "Chainmail Leggings";
        }

        public override int MaxDurability { get; } = 225;
    }
}
