namespace MineNET.Network.Packets
{
    public class CameraPacket : DataPacket
    {
        public override byte PacketID
        {
            get
            {
                return ProtocolInfo.CAMERA_PACKET;
            }
        }

        public long CameraUniqueId { get; set; }
        public long PlayerUniqueId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.CameraUniqueId);
            this.WriteEntityUniqueId(this.PlayerUniqueId);
        }

        public override void Decode()
        {
            base.Decode();

            this.CameraUniqueId = this.ReadEntityUniqueId();
            this.PlayerUniqueId = this.ReadEntityUniqueId();
        }
    }
}
