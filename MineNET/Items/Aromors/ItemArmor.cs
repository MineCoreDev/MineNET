using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemArmor : Item
    {
        public override bool IsArmor { get; } = true;

        public virtual ItemArmorType ArmorType { get; } = ItemArmorType.NONE;

        public virtual int MaxDurability { get; } = 0;
    }
}
