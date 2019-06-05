using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCoal : Item
    {
        public override int ID { get; } = ItemIDs.COAL;

        public override string GetName(int damage)
        {
            return "Coal";
        }
    }
}
