using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockStone : BlockSolid
    {
        public BlockStone() : base(BlockIDs.STONE, "Stone")
        {
            this.Hardness = 1.5f;
            this.Resistance = 30f;
            this.ToolType = ItemToolType.PICKAXE;
        }

        public override string Name
        {
            get
            {
                if (this.Damage == 1)
                {
                    return "Granite";
                }
                else if (this.Damage == 2)
                {
                    return "SmoothGranite";
                }
                else if (this.Damage == 3)
                {
                    return "Diorite";
                }
                else if (this.Damage == 4)
                {
                    return "SmoothDiorite";
                }
                else if (this.Damage == 5)
                {
                    return "Andesite";
                }
                else if (this.Damage == 6)
                {
                    return "SmoothAndesite";
                }
                return "Stone";
            }
        }

        public override Item[] GetDrops(Item item)
        {
            return base.GetDrops(item);
        }
    }
}
