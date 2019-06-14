namespace MineNET.Items
{
    public abstract class ItemArmor : Item
    {
        public override bool IsArmor => true;

        public virtual ItemArmorType ArmorType => ItemArmorType.NONE;

        public virtual int MaxDurability => 0;
    }
}
