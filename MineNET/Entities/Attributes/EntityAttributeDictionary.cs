using System.Collections.Generic;
using System.Linq;
using MineNET.Entities.Players;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Entities.Attributes
{
    public class EntityAttributeDictionary
    {
        private Dictionary<string, EntityAttribute> Attributes { get; } = new Dictionary<string, EntityAttribute>();

        public long EntityID { get; }

        public EntityAttributeDictionary(long entityID)
        {
            this.EntityID = entityID;
        }

        public void AddAttribute(params EntityAttribute[] attributes)
        {
            for (int i = 0; i < attributes.Length; ++i)
            {
                this.Attributes[attributes[i].Name] = attributes[i];
            }
        }

        public void SetAttribute(EntityAttribute attribute)
        {
            this.Attributes[attribute.Name] = attribute;
        }

        public void RemoveAttribute(params string[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
            {
                if (this.Attributes.ContainsKey(keys[i]))
                {
                    this.Attributes.Remove(keys[i]);
                }
            }
        }

        public EntityAttribute GetAttribute(string name)
        {
            return this.Attributes[name];
        }

        public void Update(Player player)
        {
            UpdateAttributesPacket pk = new UpdateAttributesPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.Attributes = this;
            player.SendPacket(pk);
        }

        public EntityAttribute[] ToArray
        {
            get
            {
                return this.Attributes.Values.ToArray();
            }
        }
    }
}
