namespace MineNET.Network.MinecraftPackets
{
    public class ServerSettingsResponsePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SERVER_SETTINGS_RESPONSE_PACKET;

        public int FormId { get; set; }
        public string Data { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.FormId);
            this.WriteString(this.Data);
        }
    }
}
