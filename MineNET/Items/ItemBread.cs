using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBread : ItemFood
    {
        public override int ID { get; } = ItemIDs.BREAD;

        public override string GetName(int damage)
        {
            return "Bread";
        }
    }
}
