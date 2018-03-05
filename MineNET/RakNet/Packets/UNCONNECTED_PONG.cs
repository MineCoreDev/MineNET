namespace MineNET.RakNet.Packets
{
    class UNCONNECTED_PONG : OfflineMessage
    {
        public const byte ID = 0x1C;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        long pingID;
        public long PingID
        {
            get
            {
                return this.pingID;
            }

            set
            {
                this.pingID = value;
            }
        }

        long serverID;
        public long ServerID
        {
            get
            {
                return this.serverID;
            }

            set
            {
                this.serverID = value;
            }
        }

        string serverName;
        public string ServerName
        {
            get
            {
                return this.serverName;
            }

            set
            {
                this.serverName = value;
            }
        }

        public override void Encode()
        {
            base.Encode();
            this.WriteLong(this.pingID);
            this.WriteLong(this.serverID);
            this.WriteMagic();
            this.WriteFixedString(this.serverName);
        }

        public override void Decode()
        {
            base.Decode();
            this.pingID = this.ReadLong();
            this.serverID = this.ReadLong();
            this.ReadMagic();
            this.serverName = this.ReadFixedString();
        }
    }
}
