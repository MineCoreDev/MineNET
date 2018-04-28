namespace MineNET.Blocks
{
    public class BlockBedrock : BlockSolid
    {
        public BlockBedrock() : base(BlockFactory.BEDROCK)
        {

        }

        public override string Name
        {
            get
            {
                return "Bedrock";
            }
        }
    }
}
