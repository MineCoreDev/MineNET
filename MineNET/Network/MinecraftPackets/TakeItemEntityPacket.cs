namespace MineNET.Network.MinecraftPackets
{
    public class TakeItemEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.TAKE_ITEM_ENTITY_PACKET;

        public long TargetRuntimeId { get; set; }
        public long EntityRuntimeId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.TargetRuntimeId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
        }

        protected override void DecodePayload()
        {

        }
    }
}
