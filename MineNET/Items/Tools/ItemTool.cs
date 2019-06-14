namespace MineNET.Items
{
    public abstract class ItemTool : Item
    {
        public override byte MaxStackSize => 1;

        public override bool IsTool => true;

        public virtual ItemToolType ToolType => ItemToolType.NONE;

        public virtual ItemToolTier ToolTier => ItemToolTier.NONE;

        public virtual int MaxDurability => 0;
    }
}
