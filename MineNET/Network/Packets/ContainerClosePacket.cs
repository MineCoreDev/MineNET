namespace MineNET.Network.Packets
{
    public class ContainerClosePacket : DataPacket
    {
        public const int ID = ProtocolInfo.CONTAINER_CLOSE_PACKET;

        public override byte PacketID
        {
            get
            {
                return ContainerClosePacket.ID;
            }
        }

        public byte WindowId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.WindowId);
        }

        public override void Decode()
        {
            base.Decode();

            this.WindowId = this.ReadByte();
        }
    }
}
