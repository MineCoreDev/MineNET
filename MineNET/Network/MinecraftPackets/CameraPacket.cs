namespace MineNET.Network.MinecraftPackets
{
    public class CameraPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CAMERA_PACKET;

        public long CameraUniqueId { get; set; }
        public long PlayerUniqueId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.CameraUniqueId);
            this.WriteEntityUniqueId(this.PlayerUniqueId);
        }
    }
}
