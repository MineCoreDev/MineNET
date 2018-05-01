using MineNET.Entities.Data;
using MineNET.Entities.Players;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class EntityHuman : EntityLiving
    {
        public EntityHuman(World world, CompoundTag tag) : base(world, tag)
        {
        }

        public UUID Uuid { get; set; }

        public virtual Skin Skin { get; set; }

        public override void SpawnTo(Player player)
        {
            if (player != this && !this.viewers.Contains(player))
            {
                this.viewers.Add(player);

                AddPlayerPacket pk = new AddPlayerPacket();
                pk.Uuid = this.Uuid;
                pk.Username = this.Name;
                pk.EntityUniqueId = this.EntityID;
                pk.EntityRuntimeId = this.EntityID;
                pk.Position = this.Vector3;
                pk.Speed = new Vector3(this.MotionX, this.MotionY, this.MotionZ);
                pk.Direction = new Vector3(this.Yaw, this.Pitch, this.Yaw);
                pk.Metadata = this.DataProperties;
                player.SendPacket(pk);
            }
        }
    }
}
