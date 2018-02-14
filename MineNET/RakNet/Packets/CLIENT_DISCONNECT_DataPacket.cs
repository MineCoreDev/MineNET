namespace MineNET.RakNet.Packets
{
    public class CLIENT_DISCONNECT_DataPacket : Packet
    {
        public const int ID = 0x15;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
