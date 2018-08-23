namespace MineNET.Network.MinecraftPackets
{
    public class ShowProfilePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SHOW_PROFILE_PACKET;

        public string Xuid { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.Xuid);
        }
    }
}
