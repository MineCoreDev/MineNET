using System;
using MineNET.Entities.Data;
using MineNET.Entities.Players;
using MineNET.Network.Packets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class EntityHuman : EntityLiving
    {
        public EntityHuman(World world, Vector3 pos) : base(world, pos)
        {
        }

        public Guid Guid { get; set; }

        public Skin Skin { get; set; }

        public override void SpawnTo(Player player)
        {
            base.SpawnTo(player);

            AddPlayerPacket pk = new AddPlayerPacket();
            pk.Guid = this.Guid;
            pk.Username = this.Name;
            pk.EntityUniqueId = this.EntityID;
            pk.EntityRuntimeId = this.EntityID;
            pk.Vector3 = this.Vector3;
            pk.Speed = new Vector3(this.MotionX, this.MotionY, this.MotionZ);
            pk.Direction = new Vector3(this.Yaw, this.Pitch, this.Yaw);
            pk.Metadata = this.DataProperties;
            player.SendPacket(pk);
        }
    }
}
