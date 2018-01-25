using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailLeggings : ItemArmor
    {
        public ItemChainmailLeggings() : base(ItemFactory.CHAINMAIL_LEGGINGS)
        {

        }

        public override string Name
        {
            get
            {
                return "ChainmailLeggings";
            }
        }

        public override bool IsLeggings
        {
            get
            {
                return true;
            }
        }
    }
}
