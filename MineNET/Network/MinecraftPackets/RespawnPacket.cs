using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class RespawnPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESPAWN_PACKET;

        public Vector3 Position { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVector3(this.Position);
        }

        protected override void DecodePayload()
        {
            this.Position = this.ReadVector3();
        }
    }
}
