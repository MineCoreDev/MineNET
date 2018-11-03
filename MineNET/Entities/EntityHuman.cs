using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class EntityHuman : EntityLiving
    {
        public UUID Uuid { get; protected set; }

        public Skin Skin { get; set; }

        public EntityHuman(Chunk chunk, CompoundTag nbt) : base(chunk, nbt)
        {

        }

        public override void SendSpawnPacket(Player player)
        {
            AddPlayerPacket pk = new AddPlayerPacket
            {
                Uuid = this.Uuid,
                Username = this.Name,
                EntityUniqueId = this.EntityID,
                EntityRuntimeId = this.EntityID,
                Position = this.ToVector3(),
                Motion = new Vector3(),
                Direction = new Vector3(this.Yaw, this.Pitch, this.HeadYaw),
                Metadata = this.DataProperties
            };

            player.SendPacket(pk);
        }

        public virtual void SendSkin(Player player)
        {
            PlayerSkinPacket pk = new PlayerSkinPacket
            {
                Uuid = this.Uuid,
                Skin = this.Skin
            };
            player.SendPacket(pk);
        }
    }
}
