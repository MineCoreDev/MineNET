namespace MineNET.Network.MinecraftPackets
{
    public class EntityFallPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ENTITY_FALL_PACKET;

        public long EntityRuntimeId { get; set; }
        public float FallDistance { get; set; }
        public bool Unknown { get; set; }

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.FallDistance = this.ReadLFloat();
            this.Unknown = this.ReadBool();
        }
    }
}
