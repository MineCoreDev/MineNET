using MineNET.Entities.Players;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Data
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

        public void Update(Player player)
        {
            AdventureSettingsPacket pk = new AdventureSettingsPacket();
            pk.Entry = this;
            player.SendPacket(pk);

            //TODO Send All....
            Player[] players = Server.Instance.GetPlayers();
            for (int i = 0; i < players.Length; i++)
            {
                AdventureSettingsPacket p = new AdventureSettingsPacket();
                AdventureSettingsPacket p2 = new AdventureSettingsPacket();
                p.Entry = this;
                p2.Entry = players[i].AdventureSettingsEntry;
                players[i].SendPacket(p);
                player.SendPacket(p2);
            }
        }
    }
}
