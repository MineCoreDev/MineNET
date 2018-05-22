namespace MineNET.Network.Packets
{
    public class UpdateBlockSyncedPacket : DataPacket
    {
        public const int ID = ProtocolInfo.UPDATE_BLOCK_SYNCED_PACKET;

        public override byte PacketID
        {
            get
            {
                return UpdateBlockSyncedPacket.ID;
            }
        }

        public long Unknown1 { get; set; }
        public long Unknown2 { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarLong((ulong) this.Unknown1);
            this.WriteUVarLong((ulong) this.Unknown2);
        }
    }
}
