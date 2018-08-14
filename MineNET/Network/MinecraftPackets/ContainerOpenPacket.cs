using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class ContainerOpenPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CONTAINER_OPEN_PACKET;

        public byte WindowId { get; set; }
        public byte Type { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public long EntityId { get; set; } = -1;

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.WindowId);
            this.WriteByte(this.Type);
            this.WriteBlockVector3(this.Position);
            this.WriteEntityUniqueId(this.EntityId);
        }

        public override void Decode()
        {
            base.Decode();

            this.WindowId = this.ReadByte();
            this.Type = this.ReadByte();
            this.Position = this.ReadBlockVector3();
            this.EntityId = this.ReadEntityUniqueId();
        }
    }
}
