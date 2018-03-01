using MineNET.Data;

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

        public const int PERMISSION_NORMAL = 0;
        public const int PERMISSION_OPERATOR = 1;
        public const int PERMISSION_HOST = 2;
        public const int PERMISSION_AUTOMATION = 3;
        public const int PERMISSION_ADMIN = 4;

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

        public uint Flags { get; set; } = 0;

        public uint CommandPermission { get; set; } = AdventureSettingsPacket.PERMISSION_NORMAL;

        public uint Flags2 { get; set; } = 0;

        public PlayerPermissions PlayerPermission { get; set; } = PlayerPermissions.MEMBER;

        public uint CustomFlags { get; set; } = 0;

        public long EntityUniqueId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.Flags);
            this.WriteUVarInt(this.CommandPermission);
            this.WriteUVarInt(this.Flags2);
            this.WriteUVarInt((uint) this.PlayerPermission);
            this.WriteUVarInt(this.CustomFlags);
            this.WriteLLong((ulong) this.EntityUniqueId);
        }

        public bool GetFlag(uint flag)
        {
            if ((flag & AdventureSettingsPacket.BITFLAG_SECOND_SET) != 0)
            {
                return (this.Flags2 & flag) != 0;
            }
            return (this.Flags & flag) != 0;
        }

        public void SetFlag(uint flag, bool value)
        {
            if ((flag & AdventureSettingsPacket.BITFLAG_SECOND_SET) != 0)
            {
                this.Flags2 |= flag;
            }
            else
            {
                this.Flags |= flag;
            }
            if (value)
            {
                this.Flags2 &= ~flag;
            }
            else
            {
                this.Flags &= ~flag;
            }
        }
    }
}
