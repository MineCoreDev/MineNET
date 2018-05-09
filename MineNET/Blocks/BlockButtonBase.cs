using MineNET.Blocks.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Blocks
{
    public abstract class BlockButtonBase : BlockTransparent
    {
        public BlockButtonBase(int id) : base(id)
        {

        }

        public override bool Place(Block clicked, Block replace, BlockFace face, Vector3 clickPos, Player player, Item item)
        {
            this.Damage = face.GetIndex();
            return base.Place(clicked, replace, face, clickPos, player, item);
        }

        public override bool Activate(Player player, Item item)
        {
            this.Damage ^= 0x08;
            this.World.SetBlock((Vector3) this, this);
            this.World.ScheduleUpdate(this, 30);
            return base.Activate(player, item);
        }

        public override bool CanBeActivated
        {
            get
            {
                return true;
            }
        }

        public override void Update(int type)
        {
            if (type == Worlds.World.BLOCK_UPDATE_SCHEDULED)
            {
                this.Damage ^= 0x08;
                this.World.SetBlock((Vector3) this, this);
            }
        }

        public override Item Item
        {
            get
            {
                return Item.Get(this.ID, 0, 1);
            }
        }
    }
}
