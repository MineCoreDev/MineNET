using System;

namespace MineNET.Data
{
    public class PlayerListEntry
    {
        public PlayerListEntry(Guid guid)
        {
            this.guid = guid;
        }

        public PlayerListEntry(Guid guid, long entityUniqueId, string name, Skin skin, string xboxUserId)
        {
            this.guid = guid;
            this.entityUniqueId = entityUniqueId;
            this.name = name;
            this.skin = skin;
            this.xboxUserId = xboxUserId;
        }

        Guid guid;
        public Guid Guid
        {
            get
            {
                return this.guid;
            }

            set
            {
                this.guid = value;
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

        string name;
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        Skin skin;
        public Skin Skin
        {
            get
            {
                return this.Skin;
            }

            set
            {
                this.skin = value;
            }
        }

        string xboxUserId;
        public string XboxUserId
        {
            get
            {
                return this.xboxUserId;
            }

            set
            {
                this.xboxUserId = value;
            }
        }
    }
}
