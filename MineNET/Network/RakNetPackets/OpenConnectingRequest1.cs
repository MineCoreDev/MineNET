namespace MineNET.Network.RakNetPackets
{
    public class OpenConnectingRequest1 : OfflineMessage
    {
        public override byte MessageID { get; } = RakNetProtocol.OpenConnectingRequest1;

        public byte Protocol { get; set; }
        public short MTUSize { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.ReadMagic();
            this.Protocol = this.ReadByte();
            this.MTUSize = (short) (this.ReadBytes().Length + 18);
        }
    }
}
