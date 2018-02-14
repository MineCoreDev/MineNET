using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemElytra : ItemArmor
    {
        public ItemElytra() : base(ItemFactory.ELYTRA)
        {

        }

        public override string Name
        {
            get
            {
                return "Elytra";
            }
        }

        public override bool IsChestplate
        {
            get
            {
                return true;
            }
        }
    }
}
