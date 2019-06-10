using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailChestplate : ItemChestplate
    {
        public override int ID { get; } = ItemIDs.CHAINMAIL_CHESTPLATE;

        public override string GetName(int damage)
        {
            return "Chainmail Chestplate";
        }

        public override int MaxDurability { get; } = 241;
    }
}
