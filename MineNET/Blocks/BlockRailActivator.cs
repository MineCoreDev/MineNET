namespace MineNET.Blocks
{
    public class BlockRailActivator : BlockRailBase
    {
        public BlockRailActivator() : base(BlockFactory.ACTIVATOR_RAIL)
        {

        }

        public override string Name
        {
            get
            {
                return "ActivatorRail";
            }
        }
    }
}
