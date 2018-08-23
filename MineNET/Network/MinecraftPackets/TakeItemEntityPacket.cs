namespace MineNET.Network.MinecraftPackets
{
    public class TakeItemEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.TAKE_ITEM_ENTITY_PACKET;

        public long TargetRuntimeId { get; set; }
        public long EntityRuntimeId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.TargetRuntimeId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
        }
    }
}
