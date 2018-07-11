namespace MineNET.Network.MinecraftPackets
{
    public class SetLocalPlayerAsInitializedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_LOCAL_PLAYER_AS_INITIALIZED_PACKET;

        public long EntityRuntimeId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityRuntimeId = this.ReadEntityRuntimeId();
        }
    }
}
