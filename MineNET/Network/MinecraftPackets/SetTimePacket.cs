namespace MineNET.Network.MinecraftPackets
{
    public class SetTimePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_TIME_PACKET;

        public int Time { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.Time);
        }

        protected override void DecodePayload()
        {

        }
    }
}
