using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGunpowder : ItemTool
    {
        public ItemGunpowder() : base(ItemFactory.GUNPOWDER)
        {

        }

        public override string Name
        {
            get
            {
                return "Gunpowder";
            }
        }
    }
}
