using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemExperienceBottle : Item
    {
        public ItemExperienceBottle() : base(ItemFactory.EXPERIENCE_BOTTLE)
        {

        }

        public override string Name
        {
            get
            {
                return "ExperienceBottle";
            }
        }
    }
}
