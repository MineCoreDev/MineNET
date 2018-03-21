using MineNET.Data;

namespace MineNET.Network.Packets.Data
{
    public class AdventureSettingsEntry
    {
        public uint Flags { get; set; } = 0;
        public PlayerPermissions CommandPermission { get; set; } = PlayerPermissions.MEMBER;
        public uint Flags2 { get; set; } = 0;
        public PlayerPermissions PlayerPermission { get; set; } = PlayerPermissions.MEMBER;
        public uint CustomFlags { get; set; } = 0;
        public long EntityUniqueId { get; set; }

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
