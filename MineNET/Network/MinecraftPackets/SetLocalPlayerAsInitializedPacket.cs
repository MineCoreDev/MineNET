namespace MineNET.Network.MinecraftPackets
{
    public class SetLocalPlayerAsInitializedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_LOCAL_PLAYER_AS_INITIALIZED_PACKET;

        public long EntityRuntimeId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
        }

        protected override void DecodePayload()
        {
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
        }
    }
}
