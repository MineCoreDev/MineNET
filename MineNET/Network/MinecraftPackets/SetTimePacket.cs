namespace MineNET.Network.MinecraftPackets
{
    public class SetTimePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_TIME_PACKET;

        public int Time { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.Time);
        }
    }
}
