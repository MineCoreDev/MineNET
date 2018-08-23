namespace MineNET.Network.MinecraftPackets
{
    public class SetLastHurtByPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_LAST_HURT_BY_PACKET;

        public int EntityTypeId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.EntityTypeId);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityTypeId = this.ReadVarInt();
        }
    }
}
