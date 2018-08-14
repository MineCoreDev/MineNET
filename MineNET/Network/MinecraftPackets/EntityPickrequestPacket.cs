namespace MineNET.Network.MinecraftPackets
{
    public class EntityPickRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ENTITY_PICK_REQUEST_PACKET;

        public long EntityUniqueId { get; set; }
        public byte HotbarSlot { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteLLong((ulong) this.EntityUniqueId);
            this.WriteByte(this.HotbarSlot);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityUniqueId = (long) this.ReadLLong();
            this.HotbarSlot = this.ReadByte();
        }
    }
}
