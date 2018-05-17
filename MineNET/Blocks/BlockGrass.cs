using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Blocks
{
    public class BlockGrass : BlockSolid
    {
        public BlockGrass() : base(BlockFactory.GRASS)
        {

        }

        public override string Name
        {
            get
            {
                return "Grass";
            }
        }

        public override bool Activate(Player player, Item item)
        {
            if (item.IsHoe)
            {
                item.Use(this);
                this.World.SetBlock((Vector3) this, new BlockFarmland());
                return true;
            }
            else if (item.IsShovel)
            {
                item.Use(this);
                this.World.SetBlock((Vector3) this, new BlockGrassPath());
                return true;
            }
            return false;
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
