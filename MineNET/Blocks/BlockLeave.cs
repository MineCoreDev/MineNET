using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockLeave : BlockTransparent
    {
        public BlockLeave(int id, string name) : base(id, name)
        {
            this.LightOpacity = 0;
            this.Hardness = 0.2f;
            this.Resistance = 1f;
            this.ToolType = ItemToolType.SHEARS;
        }
    }
}
