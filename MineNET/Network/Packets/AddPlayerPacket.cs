using System;
using MineNET.Data;
using MineNET.Entities.Metadata;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class AddPlayerPacket : DataPacket
    {
        public const int ID = ProtocolInfo.ADD_PLAYER_PACKET;

        public override byte PacketID
        {
            get
            {
                return AddPlayerPacket.ID;
            }
        }

        public Guid Guid { get; set; }
        public string Username { get; set; }
        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public Vector3 Vector3 { get; set; }
        public Vector3 Speed { get; set; }
        public Vector3 Direction { get; set; }
        public Item Item { get; set; }
        public EntityMetadataManager Metadata { get; set; }

        public int Flag { get; set; } = 0;
        public int CommandPermission { get; set; } = 0;
        public int ActionPermissions { get; set; } = 0; //TODO
        public int PermissionLevel { get; set; } = PlayerPermissions.MEMBER.GetIndex();
        public int StoredCustomPermissions { get; set; } = 0;

        public override void Encode()
        {
            base.Encode();

            this.WriteGuid(this.Guid);
            this.WriteString(this.Username);
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteVector3(this.Vector3);
            this.WriteVector3(this.Speed);
            this.WriteVector3(this.Direction);
            this.WriteItem(this.Item);
            this.WriteEntityMetadata(this.Metadata);

            this.WriteVarInt(this.Flag);
            this.WriteVarInt(this.CommandPermission);
            this.WriteVarInt(this.ActionPermissions);
            this.WriteVarInt(this.PermissionLevel);
            this.WriteVarInt(this.StoredCustomPermissions);
            this.WriteLLong(0);
        }
    }
}
