namespace MineNET.Network.MinecraftPackets
{
    public class RemoveEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.REMOVE_ENTITY_PACKET;

        public long EntityUniqueId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.EntityUniqueId);
        }

        protected override void DecodePayload()
        {

        }
    }
}
