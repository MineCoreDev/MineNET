using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class ChangeDimensionPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CHANGE_DIMENSION_PACKET;

        public int Dimension { get; set; }
        public Vector3 Position { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.Dimension);
            this.WriteVector3(this.Position);
        }

        protected override void DecodePayload()
        {

        }
    }
}
