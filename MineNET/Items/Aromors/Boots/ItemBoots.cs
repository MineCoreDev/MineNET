using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemBoots : ItemArmor
    {
        public override ItemArmorType ArmorType { get; } = ItemArmorType.BOOTS;
    }
}
