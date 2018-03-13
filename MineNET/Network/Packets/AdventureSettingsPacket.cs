using MineNET.Network.Packets.Data;

namespace MineNET.Network.Packets
{
    public class AdventureSettingsPacket : DataPacket
    {
        public const int ID = ProtocolInfo.ADVENTURE_SETTINGS_PACKET;

        public override byte PacketID
        {
            get
            {
                return AdventureSettingsPacket.ID;
            }
        }

        public AdventureSettingsEntry Entry { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.Entry.Flags);
            this.WriteUVarInt((uint) this.Entry.CommandPermission);
            this.WriteUVarInt(this.Entry.Flags2);
            this.WriteUVarInt((uint) this.Entry.PlayerPermission);
            this.WriteUVarInt(this.Entry.CustomFlags);
            this.WriteLLong((ulong) this.Entry.EntityUniqueId);
        }
    }
}
