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
                return this.clientID;
            }
        }

        long sendPing;
        public long SendPing
        {
            get
            {
                return this.sendPing;
            }
        }

        bool useSecurity = false;
        public bool UseSecurity
        {
            get
            {
                return this.useSecurity;
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.clientID = (long) ReadLLong();
            this.sendPing = (long) ReadLLong();
            this.useSecurity = ReadBool();
        }
    }
}