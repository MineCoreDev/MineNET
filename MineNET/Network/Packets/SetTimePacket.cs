namespace MineNET.Network.Packets
{
    public class SetTimePacket : DataPacket
    {
        public const int ID = ProtocolInfo.SET_TIME_PACKET;

        public override byte PacketID
        {
            get
            {
                return SetTimePacket.ID;
            }
        }

        public int Time { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.Time);
        }
    }
}
