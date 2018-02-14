using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSpawnEgg : Item
    {
        public ItemSpawnEgg() : base(ItemFactory.SPAWN_EGG)
        {

        }

        public override string Name
        {
            get
            {
                return "SpawnEgg";
            }
        }
    }
}
