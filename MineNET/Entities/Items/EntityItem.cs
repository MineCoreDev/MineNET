using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities.Items
{
    public class EntityItem : Entity
    {
        public override int NetworkId { get; } = EntityIDs.ITEM;

        public override string Name { get; protected set; } = "Item";
        public override string SaveId { get; } = "Item";

        public override float Width { get; } = 0.25f;
        public override float Height { get; } = 0.25f;

        public override float BaseOffset { get; } = 0.125f;

        public override float Gravity { get; } = 0.04f;
        public override float Drag { get; } = 0.02f;

        public short Age { get; set; }
        public short PickupDelay { get; set; }
        public string Owner { get; set; }
        public ItemStack Item { get; set; }

        public EntityItem(Chunk chunk, CompoundTag nbt) : base(chunk, nbt)
        {

        }

        protected override void EntityInit(CompoundTag nbt)
        {
            base.EntityInit(nbt);

            this.Age = nbt.GetShort("Age");
            this.PickupDelay = nbt.GetShort("PickupDelay");
            this.Owner = nbt.GetString("Owner");
            this.Item = NBTIO.ReadItem(nbt.GetCompound("Item"));

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_IMMOBILE, true);
        }

        public override CompoundTag SaveNBT()
        {
            CompoundTag nbt = base.SaveNBT();

            nbt.PutShort("Age", this.Age);
            nbt.PutShort("PickupDelay", this.PickupDelay);
            nbt.PutString("Owner", this.Owner);
            nbt.PutCompound("Item", NBTIO.WriteItem(this.Item));

            return nbt;
        }

        public override void SendSpawnPacket(Player player)
        {
            AddItemEntityPacket pk = new AddItemEntityPacket
            {
                EntityUniqueId = this.EntityID,
                EntityRuntimeId = this.EntityID,
                ItemStack = this.Item,
                Position = this.ToVector3(),
                Motion = this.GetMotion(),
                Metadata = this.DataProperties,
                IsFromFishing = false
            };

            player.SendPacket(pk);
        }
    }
}
