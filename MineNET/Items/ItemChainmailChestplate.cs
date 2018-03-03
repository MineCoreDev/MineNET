using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailChestplate : ItemArmor
    {
        public ItemChainmailChestplate() : base(ItemFactory.CHAINMAIL_CHESTPLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "ChainmailChestplate";
            }
        }

        public override bool IsChestplate
        {
            get
            {
                return true;
            }
        }
    }
}
