using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class LabTablePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.LAB_TABLE_PACKET;

        public byte UselessByte { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public byte ReactionType { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.UselessByte);
            this.WriteSBlockVector3(this.Position);
            this.WriteByte(this.ReactionType);
        }

        public override void Decode()
        {
            base.Decode();

            this.UselessByte = this.ReadByte();
            this.Position = this.ReadSBlockVector3();
            this.ReactionType = this.ReadByte();
        }
    }
}
