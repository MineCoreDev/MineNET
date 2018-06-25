namespace MineNET.Network.RakNetPackets
{
    public class OnlinePing : RakNetPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.OnlinePing;

        public long PingID { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.PingID = ReadLong();
        }
    }
}
