namespace MineNET.Network.RakNetPackets
{
    public class OnlinePong : RakNetPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.OnlinePong;

        public long PingID { get; set; }

        public override void Encode()
        {
            base.Encode();

            WriteLong(this.PingID);
        }
    }
}
