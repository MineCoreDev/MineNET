using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCarrotOnAStick : Item
    {
        public ItemCarrotOnAStick() : base(ItemFactory.CARROT_ON_A_STICK)
        {

        }

        public override string Name
        {
            get
            {
                return "CarrotOnAStick";
            }
        }
    }
}
