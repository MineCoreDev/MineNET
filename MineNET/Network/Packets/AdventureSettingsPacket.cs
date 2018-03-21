using MineNET.Network.Packets.Data;

namespace MineNET.Network.Packets
{
    public class AdventureSettingsPacket : DataPacket
    {
        public const int BITFLAG_SECOND_SET = 1 << 16;

        public const int WORLD_IMMUTABLE = 0x01;
        public const int NO_PVP = 0x02;

        public const int AUTO_JUMP = 0x20;
        public const int ALLOW_FLIGHT = 0x40;
        public const int NO_CLIP = 0x80;
        public const int WORLD_BUILDER = 0x100;
        public const int FLYING = 0x200;
        public const int MUTED = 0x400;

        public const int BUILD_AND_MINE = 0x01 | AdventureSettingsPacket.BITFLAG_SECOND_SET;
        public const int DOORS_AND_SWITCHES = 0x02 | AdventureSettingsPacket.BITFLAG_SECOND_SET;
        public const int OPEN_CONTAINERS = 0x04 | AdventureSettingsPacket.BITFLAG_SECOND_SET;
        public const int ATTACK_PLAYERS = 0x08 | AdventureSettingsPacket.BITFLAG_SECOND_SET;
        public const int ATTACK_MOBS = 0x10 | AdventureSettingsPacket.BITFLAG_SECOND_SET;
        public const int OPERATOR = 0x20 | AdventureSettingsPacket.BITFLAG_SECOND_SET;
        public const int TELEPORT = 0x80 | AdventureSettingsPacket.BITFLAG_SECOND_SET;

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
