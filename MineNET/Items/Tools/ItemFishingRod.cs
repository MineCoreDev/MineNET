namespace MineNET.Items
{
    public class ItemFishingRod : ItemTool
    {
        public override int ID { get; } = ItemIDs.FISHING_ROD;

        public override string GetName(int damage)
        {
            return "Fishing Rod";
        }
    }
}
