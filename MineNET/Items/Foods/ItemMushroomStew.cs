using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMushroomStew : ItemFood
    {
        public override int ID { get; } = ItemIDs.MUSHROOM_STEW;

        public override string GetName(int damage)
        {
            return "Mushroom Stew";
        }
    }
}
