namespace MineNET.Network.RakNetPackets
{
    public class OfflinePing : OfflineMessage
    {
        public override byte MessageID { get; } = RakNetProtocol.OfflinePing;

        public long Ping { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteLong(this.Ping);
            this.WriteMagic();
        }

        public override void Decode()
        {
            base.Decode();

            this.Ping = this.ReadLong();
            this.ReadMagic();
        }
    }
}
