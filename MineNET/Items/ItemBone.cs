namespace MineNET.Items
{
    public class ItemBone : Item
    {
        public override int ID { get; } = ItemIDs.BONE;

        public override string GetName(int damage)
        {
            return "Bone";
        }
    }
}
