using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class SpawnExperienceOrbPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SPAWN_EXPERIENCE_ORB_PACKET;

        public Vector3 Position { get; set; }
        public int Amount { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVector3(this.Position);
            this.WriteVarInt(this.Amount);
        }

        protected override void DecodePayload()
        {

        }
    }
}
