namespace MineNET.Network.RakNetPackets
{
    public class ClientConnectDataPacket : RakNetPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.ClientConnectDataPacket;

        public long ClientID { get; set; }
        public long SendPing { get; set; }
        public bool UseSecurity { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.ClientID = ReadLong();
            this.SendPing = ReadLong();
            this.UseSecurity = ReadBool();
        }
    }
}
