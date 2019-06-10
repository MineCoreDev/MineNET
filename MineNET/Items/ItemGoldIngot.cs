using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldIngot : Item
    {
        public override int ID { get; } = ItemIDs.GOLD_INGOT;

        public override string GetName(int damage)
        {
            return "Gold Ingot";
        }
    }
}
