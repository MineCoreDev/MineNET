namespace MineNET.Items
{
    public class ItemBow : ItemTool
    {
        public override int ID => ItemIDs.BOW;

        public override string Name => "Bow";

        public override ItemToolType ToolType => ItemToolType.BOW;

        public override int MaxDurability => 385;
    }
}
