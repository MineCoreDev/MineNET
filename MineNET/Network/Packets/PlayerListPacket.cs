using MineNET.Data;

namespace MineNET.Network.Packets
{
    public class PlayerListPacket : DataPacket
    {
        public const int ID = ProtocolInfo.PLAYER_LIST_PACKET;

        public override byte PacketID
        {
            get
            {
                return PlayerListPacket.ID;
            }
        }

        public const byte TYPE_ADD = 0;
        public const byte TYPE_REMOVE = 1;

        public byte Type { get; set; }

        public PlayerListEntry[] Entries { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Type);
            this.WriteSVarLong(this.Entries.Length);
            for (int i = 0; i < this.Entries.Length; ++i)
            {
                this.WriteGUID(this.Entries[i].Guid);
                if (this.Type == PlayerListPacket.TYPE_ADD)
                {
                    this.WriteEntityUniqueId(this.Entries[i].EntityUniqueId);
                    this.WriteString(this.Entries[i].Name);
                    //this.WriteSkin(this.Entries[i].Skin);
                    //this.WriteString(this.Entries[i].XboxUserId);
                }
            }
        }
    }
}
