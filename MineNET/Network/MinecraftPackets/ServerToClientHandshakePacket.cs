namespace MineNET.Network.MinecraftPackets
{
    public class ServerToClientHandshakePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SERVER_TO_CLIENT_HANDSHAKE_PACKET;

        public string Jwt { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.Jwt);
        }

        public override void Decode()
        {
            base.Decode();

            this.Jwt = this.ReadString();
        }
    }
}
