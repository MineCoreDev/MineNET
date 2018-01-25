using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemAcaciaDoor : Item
    {
        public ItemAcaciaDoor() : base(ItemFactory.ACACIA_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "AcaciaDoor";
            }
        }
    }
}
