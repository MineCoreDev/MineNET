namespace MineNET.Network.Packets
{
    public class SetTimePacket : DataPacket
    {
        public const int ID = ProtocolInfo.SET_TIME_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        int time;
        public int Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.time);
        }
    }
}
