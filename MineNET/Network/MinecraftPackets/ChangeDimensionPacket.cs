using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class ChangeDimensionPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CHANGE_DIMENSION_PACKET;

        public int Dimension { get; set; }
        public Vector3 Position { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.Dimension);
            this.WriteVector3(this.Position);
        }
    }
}
