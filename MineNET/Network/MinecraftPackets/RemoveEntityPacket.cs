namespace MineNET.Network.MinecraftPackets
{
    public class RemoveEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.REMOVE_ENTITY_PACKET;

        public long EntityUniqueId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.EntityUniqueId);
        }
    }
}
