namespace MineNET.Network.MinecraftPackets
{
    public class MapCreateLockedCopyPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MAP_CREATE_LOCKED_COPY_PACKET;

        public long OriginalMapID { get; set; }
        public long NewMapID { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.OriginalMapID);
            this.WriteEntityUniqueId(this.NewMapID);
        }

        protected override void DecodePayload()
        {
            this.OriginalMapID = this.ReadEntityUniqueId();
            this.NewMapID = this.ReadEntityUniqueId();
        }
    }
}
