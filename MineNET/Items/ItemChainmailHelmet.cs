using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailHelmet : ItemArmor
    {
        public ItemChainmailHelmet() : base(ItemFactory.CHAINMAIL_HELMET)
        {

        }

        public override string Name
        {
            get
            {
                return "ChainmailHelmet";
            }
        }

        public override bool IsHemlet
        {
            get
            {
                return true;
            }
        }
    }
}
