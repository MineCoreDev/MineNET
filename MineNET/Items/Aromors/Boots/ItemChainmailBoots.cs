using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailBoots : ItemBoots
    {
        public override int ID { get; } = ItemIDs.CHAINMAIL_BOOTS;

        public override string GetName(int damage)
        {
            return "Chainmail Boots";
        }

        public override int MaxDurability { get; } = 195;
    }
}
