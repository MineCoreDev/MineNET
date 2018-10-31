using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class AddEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ADD_ENTITY_PACKET;

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public int Type { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Motion { get; set; }
        public Vector3 Direction { get; set; }
        public EntityAttributeDictionary Attributes { get; set; }
        public EntityMetadataManager Metadata { get; set; }
        //public EntityLink[] Link = new EntityLink[0];

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteUVarInt((uint) this.Type);
            this.WriteVector3(this.Position);
            this.WriteVector3(this.Motion);
            this.WriteVector3(this.Direction);
            this.WriteAttributes(this.Attributes);
            this.WriteEntityMetadata(this.Metadata);
            this.WriteUVarInt(0); //TODO: EntityLink size
            //TODO: WriteEntityLink
        }

        protected override void DecodePayload()
        {

        }
    }
}
