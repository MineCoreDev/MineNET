namespace MineNET.Network.MinecraftPackets
{
    public class SetLastHurtByPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_LAST_HURT_BY_PACKET;

        public int EntityTypeId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.EntityTypeId);
        }

        protected override void DecodePayload()
        {
            this.EntityTypeId = this.ReadVarInt();
        }
    }
}
