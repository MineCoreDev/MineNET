namespace MineNET.Network.MinecraftPackets
{
    public class ShowProfilePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SHOW_PROFILE_PACKET;

        public string Xuid { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.Xuid);
        }

        protected override void DecodePayload()
        {

        }
    }
}
