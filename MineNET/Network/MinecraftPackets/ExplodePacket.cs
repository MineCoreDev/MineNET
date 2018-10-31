using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class ExplodePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.EXPLODE_PACKET;

        public Vector3 Position { get; set; }
        public float Radius { get; set; }
        public BlockCoordinate3D[] Records { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVector3(this.Position);
            this.WriteVarInt((int) (this.Radius * 32));
            this.WriteUVarInt((uint) this.Records.Length);
            for (int i = 0; i < this.Records.Length; ++i)
            {
                this.WriteSBlockVector3(this.Records[i]);
            }
        }

        protected override void DecodePayload()
        {
            this.Position = this.ReadVector3();
            this.Radius = this.ReadVarInt() / 32;
            this.Records = new BlockCoordinate3D[this.ReadUVarInt()];
            for (int i = 0; i < this.Records.Length; ++i)
            {
                this.Records[i] = this.ReadSBlockVector3();
            }
        }
    }
}
