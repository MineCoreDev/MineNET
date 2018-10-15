using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockSand : BlockFalling
    {
        public BlockSand() : base(BlockIDs.SAND, "sand")
        {
            this.IsSolid = true;

            this.Hardness = 0.5f;
            this.Resistance = 2.5f;
            this.ToolType = ItemToolType.SHOVEL;
        }

        public override string Name
        {
            get
            {
                if (this.Damage == 1)
                {
                    return "RedSand";
                }
                return "Sand";
            }
        }
    }
}
