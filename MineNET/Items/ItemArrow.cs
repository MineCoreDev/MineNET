using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemArrow : Item
    {
        public override int ID { get; } = ItemIDs.ARROW;

        public override string GetName(int damage)
        {
            return "Arrow";
        }
    }
}
