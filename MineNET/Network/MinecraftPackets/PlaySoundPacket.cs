using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class PlaySoundPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PLAY_SOUND_PACKET;

        public string Name { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public float Volume { get; set; }
        public float Pitch { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.Name);
            this.WriteBlockVector3(this.Position);
            this.WriteLFloat(this.Volume);
            this.WriteLFloat(this.Pitch);
        }
    }
}
