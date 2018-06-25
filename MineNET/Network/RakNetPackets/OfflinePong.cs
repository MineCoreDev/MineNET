namespace MineNET.Network.RakNetPackets
{
    public class OfflinePong : OfflineMessage
    {
        public override byte MessageID { get; } = RakNetProtocol.OfflinePong;

        public long Ping { get; set; }
        public long ServerID { get; set; }
        public string ServerName { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteLong(this.Ping);
            this.WriteLong(this.ServerID);
            this.WriteMagic();

            this.WriteFixedString(this.ServerName);
        }
    }
}
