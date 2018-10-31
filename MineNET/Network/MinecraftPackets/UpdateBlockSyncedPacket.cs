namespace MineNET.Network.MinecraftPackets
{
    public class UpdateBlockSyncedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_BLOCK_SYNCED_PACKET;

        public ulong Unknown1 { get; set; }
        public ulong Unknown2 { get; set; }

        protected override void EncodePayload()
        {
            this.WriteUVarLong(this.Unknown1);
            this.WriteUVarLong(this.Unknown2);
        }

        protected override void DecodePayload()
        {

        }
    }
}
