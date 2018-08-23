using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class RespawnPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESPAWN_PACKET;

        public Vector3 Position { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVector3(this.Position);
        }

        public override void Decode()
        {
            base.Decode();

            this.Position = this.ReadVector3();
        }
    }
}
