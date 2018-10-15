namespace MineNET.Blocks
{
    public class BlockGlass : BlockTransparent
    {
        public BlockGlass(int id, string name) : base(id, name)
        {
            this.LightOpacity = 0;
            this.Hardness = 0.3f;
            this.Resistance = 1.5f;
        }
    }
}
