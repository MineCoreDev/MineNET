using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemApple : ItemFood
    {
        public override int ID { get; } = ItemIDs.APPLE;

        public override string GetName(int damage)
        {
            return "Apple";
        }

        public override int FoodRestore => 4;

        public override float SaturationRestore => 2.4f;
    }
}
