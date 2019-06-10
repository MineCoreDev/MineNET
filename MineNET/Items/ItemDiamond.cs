using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamond : Item
    {
        public override int ID { get; } = ItemIDs.DIAMOND;

        public override string GetName(int damage)
        {
            return "Diamond";
        }
    }
}
