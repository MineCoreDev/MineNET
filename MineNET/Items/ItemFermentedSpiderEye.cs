using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFermentedSpiderEye : Item
    {
        public ItemFermentedSpiderEye() : base(ItemFactory.FERMENTED_SPIDER_EYE)
        {

        }

        public override string Name
        {
            get
            {
                return "FermentedSpiderEye";
            }
        }
    }
}
