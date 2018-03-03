using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailBoots : ItemArmor
    {
        public ItemChainmailBoots() : base(ItemFactory.CHAINMAIL_BOOTS)
        {

        }

        public override string Name
        {
            get
            {
                return "ChainmailBoots";
            }
        }

        public override bool IsBoots
        {
            get
            {
                return true;
            }
        }
    }
}
