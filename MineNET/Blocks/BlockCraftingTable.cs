namespace MineNET.Blocks
{
    public class BlockCraftingTable : BlockSolid
    {
        public BlockCraftingTable() : base(BlockFactory.CRAFTING_TABLE)
        {

        }

        public override string Name
        {
            get
            {
                return "CraftingTable";
            }
        }

        public override bool CanBeActivated
        {
            get
            {
                return true;
            }
        }
    }
}
