namespace MineNET.Network.MinecraftPackets
{
    public class UpdateBlockSyncedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_BLOCK_SYNCED_PACKET;

        public ulong Unknown1 { get; set; }
        public ulong Unknown2 { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarLong(this.Unknown1);
            this.WriteUVarLong(this.Unknown2);
        }
    }
}
