using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemEnderEye : Item
    {
        public ItemEnderEye() : base(ItemFactory.ENDER_EYE)
        {

        }

        public override string Name
        {
            get
            {
                return "EnderEye";
            }
        }
    }
}
