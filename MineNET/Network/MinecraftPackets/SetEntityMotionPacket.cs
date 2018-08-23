using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class SetEntityMotionPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_ENTITY_MOTION_PACKET;

        public long EntityRuntimeId { get; set; }
        public Vector3 Motion { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteVector3(this.Motion);
        }
    }
}
