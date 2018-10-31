namespace MineNET.Network.MinecraftPackets
{
    public class DisconnectPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.DISCONNECT_PACKET;

        public bool HideDisconnectionScreen { get; set; } = false;
        public string Message { get; set; }

        protected override void EncodePayload()
        {
            this.WriteBool(this.HideDisconnectionScreen);
            if (!this.HideDisconnectionScreen)
            {
                this.WriteString(this.Message);
            }
        }

        protected override void DecodePayload()
        {

        }
    }
}
