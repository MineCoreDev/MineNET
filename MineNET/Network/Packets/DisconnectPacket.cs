namespace MineNET.Network.Packets
{
    public class DisconnectPacket : DataPacket
    {
        public const int ID = ProtocolInfo.DISCONNECT_PACKET;

        public override byte PacketID
        {
            get
            {
                return DisconnectPacket.ID;
            }
        }

        public bool HideDisconnectionScreen { get; set; } = false;

        public string Message { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBool(this.HideDisconnectionScreen);
            if (!this.HideDisconnectionScreen)
            {
                this.WriteString(this.Message);
            }
        }
    }
}
