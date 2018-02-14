namespace MineNET.RakNet.Packets
{
    public class CLIENT_CONNECT_DataPacket : Packet
    {
        public const int ID = 0x09;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        long clientID;
        public long ClientID
        {
            get
            {
                return clientID;
            }
        }

        long sendPing;
        public long SendPing
        {
            get
            {
                return sendPing;
            }
        }

        bool useSecurity = false;
        public bool UseSecurity
        {
            get
            {
                return useSecurity;
            }
        }

        public override void Decode()
        {
            base.Decode();

            clientID = ReadLong();
            sendPing = ReadLong();
            useSecurity = ReadBool();
        }
    }
}