using System;
using MineNET.Entities.Data;

namespace MineNET.Data
{
    public class PlayerListEntry
    {
        public PlayerListEntry(Guid guid)
        {
            this.Guid = guid;
        }

        public PlayerListEntry(Guid guid, long entityUniqueId, string name, int platForm, Skin skin, string xboxUserId)
        {
            this.Guid = guid;
            this.EntityUniqueId = entityUniqueId;
            this.Name = name;
            this.PlatForm = platForm;
            this.Skin = skin;
            this.XboxUserId = xboxUserId;
        }

        public Guid Guid { get; set; }
        public long EntityUniqueId { get; set; }
        public string Name { get; set; }
        public int PlatForm { get; set; }
        public Skin Skin { get; set; }
        public string XboxUserId { get; set; }
    }
}
