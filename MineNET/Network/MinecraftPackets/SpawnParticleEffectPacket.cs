using MineNET.Values;
using MineNET.Worlds.Dimensions;

namespace MineNET.Network.MinecraftPackets
{
    public class SpawnParticleEffectPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SPAWN_PARTICLE_EFFECT_PACKET;

        public byte DimensionId { get; set; } = DimensionIDs.OverWorld;
        public Vector3 Position { get; set; }
        public string ParticleName { get; set; }

        protected override void EncodePayload()
        {
            this.WriteByte(this.DimensionId);
            this.WriteVector3(this.Position);
            this.WriteString(this.ParticleName);
        }

        protected override void DecodePayload()
        {
            this.DimensionId = this.ReadByte();
            this.Position = this.ReadVector3();
            this.ParticleName = this.ReadString();
        }
    }
}
