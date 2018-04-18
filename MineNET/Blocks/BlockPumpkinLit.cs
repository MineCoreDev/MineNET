namespace MineNET.Blocks
{
    public class BlockPumpkinLit : BlockSolid
    {
        public BlockPumpkinLit() : base(BlockFactory.LIT_PUMPKIN)
        {

        }

        public override string Name
        {
            get
            {
                return "LitPumpkin";
            }
        }
    }
}
