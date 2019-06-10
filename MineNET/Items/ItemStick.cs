using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStick : Item
    {
        public override int ID { get; } = ItemIDs.STICK;

        public override string GetName(int damage)
        {
            return "Stick";
        }
    }
}
