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
                return ID;
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

        uint flags = 0;
        public uint Flags
        {
            get
            {
                return this.flags;
            }

            set
            {
                this.flags = value;
            }
        }

        uint commandPermission = AdventureSettingsPacket.PERMISSION_NORMAL;
        public uint CommandPermission
        {
            get
            {
                return this.commandPermission;
            }

            set
            {
                this.commandPermission = value;
            }
        }

        uint flags2 = 0;
        public uint Flags2
        {
            get
            {
                return this.flags2;
            }

            set
            {
                this.flags2 = value;
            }
        }

        PlayerPermissions playerPermission = PlayerPermissions.MEMBER;
        public PlayerPermissions PlayerPermission
        {
            get
            {
                return this.playerPermission;
            }

            set
            {
                this.playerPermission = value;
            }
        }

        uint customFlags = 0;
        public uint CustpmFlags
        {
            get
            {
                return this.customFlags;
            }

            set
            {
                this.customFlags = value;
            }
        }

        long entityUniqueId;
        public long EntityUniqueId
        {
            get
            {
                return this.entityUniqueId;
            }

            set
            {
                this.entityUniqueId = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.flags);
            this.WriteUVarInt(this.commandPermission);
            this.WriteUVarInt(this.flags2);
            this.WriteUVarInt((uint) this.playerPermission);
            this.WriteUVarInt(this.customFlags);
            this.WriteLLong((ulong) this.entityUniqueId);
        }

        public bool GetFlag(uint flag)
        {
            if ((flag & AdventureSettingsPacket.BITFLAG_SECOND_SET) != 0)
            {
                return (this.flags2 & flag) != 0;
            }
            return (this.flags & flag) != 0;
        }

        public void SetFlag(uint flag, bool value)
        {
            if ((flag & AdventureSettingsPacket.BITFLAG_SECOND_SET) != 0)
            {
                this.flags2 |= flag;
            }
            else
            {
                this.flags |= flag;
            }
            if (value)
            {
                this.flags2 &= ~flag;
            }
            else
            {
                this.flags &= ~flag;
            }
        }
    }
}
