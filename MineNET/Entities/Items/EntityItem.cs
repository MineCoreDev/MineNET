using MineNET.NBT.Tags;
using MineNET.Worlds;

namespace MineNET.Entities.Items
{
    public class EntityItem : Entity
    {
        public override int NetworkId { get; } = EntityIDs.ITEM;

        public override string Name { get; protected set; } = "Item";

        public EntityItem(Chunk chunk, CompoundTag nbt) : base(chunk, nbt)
        {

        }

        protected override void EntityInit(CompoundTag nbt)
        {

        }
    }
}
