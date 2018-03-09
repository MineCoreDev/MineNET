using MineNET.Network.Packets.Data;

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

            this.WritePlayerListEntries(this.Entries, this.Type);
        }
    }
}
