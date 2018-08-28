using MineNET.Data;

namespace MineNET.Network.MinecraftPackets
{
    public class SetScoreboardIdentityPacket : MinecraftPacket
    {
        public const byte TYPE_REGISTER_IDENTITY = 0;
        public const byte TYPE_CLEAR_IDENTITY = 1;

        public override byte PacketID { get; } = MinecraftProtocol.SET_SCOREBOARD_IDENTITY_PACKET;

        public byte Type { get; set; }
        public ScoreboardIdentityPacketEntry[] Entries { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Type);
            this.WriteUVarInt((uint) this.Entries.Length);
            for (int i = 0; i < this.Entries.Length; ++i)
            {
                this.WriteVarLong(this.Entries[i].ScoreboardId);
                if (this.Type == SetScoreboardIdentityPacket.TYPE_REGISTER_IDENTITY)
                {
                    this.WriteUUID(this.Entries[i].Uuid);
                }
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.Type = this.ReadByte();
            this.Entries = new ScoreboardIdentityPacketEntry[this.ReadUVarInt()];
            for (int i = 0; i < this.Entries.Length; ++i)
            {
                this.Entries[i].ScoreboardId = this.ReadVarLong();
                if (this.Type == SetScoreboardIdentityPacket.TYPE_REGISTER_IDENTITY)
                {
                    this.Entries[i].Uuid = this.ReadUUID();
                }
            }
        }
    }
}
