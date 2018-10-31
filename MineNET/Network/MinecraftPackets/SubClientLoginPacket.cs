namespace MineNET.Network.MinecraftPackets
{
    public class SubClientLoginPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SUB_CLIENT_LOGIN_PACKET;

        public string ConnectionRequestData { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.ConnectionRequestData);
        }

        protected override void DecodePayload()
        {
            this.ConnectionRequestData = this.ReadString();
        }
    }
}
