using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBone : Item
    {
        public ItemBone() : base(ItemFactory.BONE)
        {

        }

        public override string Name
        {
            get
            {
                return "Bone";
            }
        }
    }
}
