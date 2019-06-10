using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondHelmet : ItemHelmet
    {
        public override int ID { get; } = ItemIDs.DIAMOND_HELMET;

        public override string GetName(int damage)
        {
            return "Diamond Helmet";
        }

        public override int MaxDurability { get; } = 363;
    }
}
