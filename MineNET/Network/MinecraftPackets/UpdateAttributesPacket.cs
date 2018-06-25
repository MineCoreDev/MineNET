using MineNET.Entities.Attributes;

namespace MineNET.Network.MinecraftPackets
{
    public class UpdateAttributesPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_ATTRIBUTES_PACKET;

        public long EntityRuntimeId { get; set; }
        public EntityAttributeDictionary Attributes { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteAttributes(this.Attributes);
        }
    }
}
