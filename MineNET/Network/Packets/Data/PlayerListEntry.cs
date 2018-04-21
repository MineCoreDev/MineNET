using MineNET.Entities.Data;
using MineNET.Values;

namespace MineNET.Network.Packets.Data
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
            this.PlatForm = platForm;
            this.Skin = skin;
            this.XboxUserId = xboxUserId;
        }

        public UUID UUID { get; set; }
        public long EntityUniqueId { get; set; }
        public string Name { get; set; }
        public int PlatForm { get; set; }
        public Skin Skin { get; set; }
        public string XboxUserId { get; set; }
    }
}
