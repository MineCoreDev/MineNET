using System.Collections.Generic;
using MineNET.Entities.Players;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Data
{
    public class PlayerListEntry
    {
        public PlayerListEntry(UUID uuid)
        {
            this.UUID = uuid;
        }

        public PlayerListEntry(UUID uuid, long entityUniqueId, string name, int platForm, Skin skin, string xboxUserId)
        {
            this.UUID = uuid;
            this.EntityUniqueId = entityUniqueId;
            this.Name = name;
            this.Skin = skin;
            this.XboxUserId = xboxUserId;
        }

        public UUID UUID { get; set; }
        public long EntityUniqueId { get; set; }
        public string Name { get; set; }
        public Skin Skin { get; set; }
        public string XboxUserId { get; set; }

        public void UpdateAll()
        {
            List<PlayerListEntry> entries = new List<PlayerListEntry>();
            Player[] players = Server.Instance.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                entries.Add(players[i].PlayerListEntry);
            }

            PlayerListPacket pk = new PlayerListPacket();
            pk.Entries = entries.ToArray();
            Server.Instance.BroadcastSendPacket(pk);
        }
    }
}
