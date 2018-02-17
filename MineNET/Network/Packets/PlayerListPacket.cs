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
                return ID;
            }
        }

        public const byte TYPE_ADD = 0;
        public const byte TYPE_REMOVE = 1;

        byte type;
        public byte Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        PlayerListEntry[] entries;
        public PlayerListEntry[] Entries
        {
            get
            {
                return this.entries;
            }

            set
            {
                this.entries = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.type);
            this.WriteUVarInt((uint) this.entries.Length);
            for (int i = 0; i < this.entries.Length; i++)
            {
                //this.WriteGuid(this.entries[i].Guid);
                if (this.type == PlayerListPacket.TYPE_ADD)
                {
                    this.WriteVarLong(this.entries[i].EntityUniqueId);
                    this.WriteSkin(this.entries[i].Skin);
                    this.WriteString(this.entries[i].XboxUserId);
                }
            }
        }
    }
}
