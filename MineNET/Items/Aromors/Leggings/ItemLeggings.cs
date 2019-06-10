using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemLeggings : ItemArmor
    {
        public override ItemArmorType ArmorType { get; } = ItemArmorType.LEGGINGS;
    }
}
