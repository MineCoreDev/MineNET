using MineNET.Blocks.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Blocks
{
    public class BlockBed : Block
    {
        public BlockBed() : base(BlockFactory.BED)
        {

        }

        public override string Name
        {
            get
            {
                return "Bed";
            }
        }

        public override bool Place(Block clicked, Block replace, BlockFace face, Vector3 clickPos, Player player, Item item)
        {
            if (!this.GetSideBlock(BlockFace.DOWN).IsSolid)
            {
                return false;
            }
            BlockFace direction = player.DirectionBlockFace;
            Block side = this.GetSideBlock(direction);
            if (!side.GetSideBlock(BlockFace.DOWN).IsSolid || !side.CanBeReplaced)
            {
                return false;
            }
            int[] metas = new int[] { 0, 0, 2, 0, 1, 3 };
            this.Damage = metas[direction.GetIndex()];
            this.World.SetBlock((Vector3) this, this);
            this.World.SetBlock((Vector3) side, Block.Get(this.ID, metas[direction.GetIndex()] | 0x08));
            return true;
        }

        public override bool Break(Player player, Item item)
        {
            int[] metas = new int[] { 3, 4, 2, 5 };
            BlockFace face;
            if (this.Damage > 4)
            {
                face = (BlockFace) metas[this.Damage & 0x03];
                face = face.GetReverseBlockFace();
            }
            else
            {
                face = (BlockFace) metas[this.Damage];
            }
            this.World.SetBlock((Vector3) this, new BlockAir());
            this.World.SetBlock((Vector3) this.GetSideBlock(face), new BlockAir());
            return base.Break(player, item);
        }

        public override bool Activate(Player player, Item item)
        {
            return base.Activate(player, item);
        }

        public override bool CanBeActivated
        {
            get
            {
                return true;
            }
        }

        public override Item Item
        {
            get
            {
                return Item.Get(ItemFactory.BED, 0, 1);
            }
        }
    }
}
