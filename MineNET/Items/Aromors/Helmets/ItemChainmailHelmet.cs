using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChainmailHelmet : ItemHelmet
    {
        public override int ID { get; } = ItemIDs.CHAINMAIL_HELMET;

        public override string GetName(int damage)
        {
            return "Chainmail Helmet";
        }

        public override int MaxDurability { get; } = 165;
    }
}
