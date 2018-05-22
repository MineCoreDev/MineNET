using MineNET.Network.Packets.Data;

namespace MineNET.Network.Packets
{
    public class SetScorePacket : DataPacket
    {
        public const byte TYPE_MODIFY_SCORE = 0;
        public const byte TYPE_RESET_SCORE = 1;

        public const int ID = ProtocolInfo.SET_SCORE_PACKET;

        public override byte PacketID
        {
            get
            {
                return SetScorePacket.ID;
            }
        }

        public byte Type { get; set; }
        public ScorePacketEntry[] Entries { get; set; } = new ScorePacketEntry[0];

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Type);
            this.WriteUVarInt((uint) this.Entries.Length);
            for (int i = 0; i < this.Entries.Length; ++i)
            {
                ScorePacketEntry entry = this.Entries[i];
                this.WriteUUID(entry.UUID);
                this.WriteString(entry.ObjectiveName);
                this.WriteLInt((uint) entry.Score);
            }
        }
    }
}
