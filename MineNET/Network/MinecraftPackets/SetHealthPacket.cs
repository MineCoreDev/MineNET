namespace MineNET.Network.MinecraftPackets
{
    public class SetHealthPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_HEALTH_PACKET;

        public int Health { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.Health);
        }

        protected override void DecodePayload()
        {
            this.Health = this.ReadVarInt();
        }
    }
}
