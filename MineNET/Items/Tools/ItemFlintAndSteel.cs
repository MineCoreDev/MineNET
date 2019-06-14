namespace MineNET.Items
{
    public class ItemFlintAndSteel : ItemTool
    {
        public override int ID => ItemIDs.FLINT_AND_STEEL;

        public override string Name => "Flint And Steel";

        public override ItemToolType ToolType => ItemToolType.FLINT_AND_STEEL;

        public override int MaxDurability => 65;
    }
}
