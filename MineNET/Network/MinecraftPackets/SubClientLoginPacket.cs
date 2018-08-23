namespace MineNET.Network.MinecraftPackets
{
    public class SubClientLoginPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SUB_CLIENT_LOGIN_PACKET;

        public string ConnectionRequestData { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.ConnectionRequestData);
        }

        public override void Decode()
        {
            base.Decode();

            this.ConnectionRequestData = this.ReadString();
        }
    }
}
