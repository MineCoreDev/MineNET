namespace MineNET.Network.MinecraftPackets
{
    public class EntityPickRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ENTITY_PICK_REQUEST_PACKET;

        public long EntityUniqueId { get; set; }
        public byte HotbarSlot { get; set; }

        protected override void EncodePayload()
        {
            this.WriteLLong((ulong) this.EntityUniqueId);
            this.WriteByte(this.HotbarSlot);
        }

        protected override void DecodePayload()
        {
            this.EntityUniqueId = (long) this.ReadLLong();
            this.HotbarSlot = this.ReadByte();
        }
    }
}
