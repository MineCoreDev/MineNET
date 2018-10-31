namespace MineNET.Network.MinecraftPackets
{
    public class CameraPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CAMERA_PACKET;

        public long CameraUniqueId { get; set; }
        public long PlayerUniqueId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.CameraUniqueId);
            this.WriteEntityUniqueId(this.PlayerUniqueId);
        }

        protected override void DecodePayload()
        {

        }
    }
}
