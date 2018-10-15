namespace MineNET.Blocks
{
    public abstract class BlockLiquid : Block
    {
        public BlockLiquid(int id, string name) : base(id, name)
        {
            this.CanBreak = false;
        }
    }
}
