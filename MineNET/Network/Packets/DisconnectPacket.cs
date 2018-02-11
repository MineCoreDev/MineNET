namespace MineNET.Network.Packets
{
    public class DisconnectPacket : DataPacket
    {
        public const int ID = ProtocolInfo.DISCONNECT_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        bool hideDisconnectionScreen = false;
        public bool HideDisconnectionScreen
        {
            get
            {
                return hideDisconnectionScreen;
            }

            set
            {
                hideDisconnectionScreen = value;
            }
        }

        string message;
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteBool(hideDisconnectionScreen);
            if (!hideDisconnectionScreen)
            {
                WriteString(message);
            }
        }
    }
}
