namespace MineNET.Network.MinecraftPackets
{
    public class ServerToClientHandshakePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SERVER_TO_CLIENT_HANDSHAKE_PACKET;

        public string Jwt { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.Jwt);
        }

        protected override void DecodePayload()
        {
            this.Jwt = this.ReadString();
        }
    }
}
