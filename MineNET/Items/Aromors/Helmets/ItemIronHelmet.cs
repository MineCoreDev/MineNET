using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronHelmet : ItemHelmet
    {
        public override int ID { get; } = ItemIDs.IRON_HELMET;

        public override string GetName(int damage)
        {
            return "Iron Helmet";
        }

        public override int MaxDurability { get; } = 165;
    }
}
