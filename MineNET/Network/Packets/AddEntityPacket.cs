using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class AddEntityPacket : DataPacket
    {
        public const int ID = ProtocolInfo.ADD_ENTITY_PACKET;

        public override byte PacketID
        {
            get
            {
                return AddEntityPacket.ID;
            }
        }

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public int Type { get; set; }
        public Vector3 Vector3 { get; set; }
        public Vector3 Speed { get; set; }
        public Vector2 Direction { get; set; }
        public EntityAttribute[] Attributes { get; set; }
        public EntityMetadataManager Metadata { get; set; }
        //public EntityLink[] Link = new EntityLink[0];

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteUVarInt((uint) this.Type);
            this.WriteVector3(this.Vector3);
            this.WriteVector3(this.Speed);
            this.WriteVector2(this.Direction);
            this.WriteAttributes(this.Attributes);
            this.WriteEntityMetadata(this.Metadata);
            this.WriteUVarInt(0); //TODO : EntityLink size
            //TODO : WriteEntityLink
        }
    }
}
