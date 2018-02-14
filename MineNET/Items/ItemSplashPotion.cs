using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSplashPotion : Item
    {
        public ItemSplashPotion() : base(ItemFactory.SPLASH_POTION)
        {

        }
        
        public override string Name
        {
            get
            {
                return "SplashPotion";
            }
        }
    }
}
