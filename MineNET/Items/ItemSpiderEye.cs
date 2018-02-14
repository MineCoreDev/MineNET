using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSpiderEye : Item
    {
        public ItemSpiderEye() : base(ItemFactory.SPIDER_EYE)
        {

        }

        public override string Name
        {
            get
            {
                return "SpiderEye";
            }
        }
    }
}
