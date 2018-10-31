using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class LabTablePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.LAB_TABLE_PACKET;

        public byte UselessByte { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public byte ReactionType { get; set; }

        protected override void EncodePayload()
        {
            this.WriteByte(this.UselessByte);
            this.WriteSBlockVector3(this.Position);
            this.WriteByte(this.ReactionType);
        }

        protected override void DecodePayload()
        {
            this.UselessByte = this.ReadByte();
            this.Position = this.ReadSBlockVector3();
            this.ReactionType = this.ReadByte();
        }
    }
}
