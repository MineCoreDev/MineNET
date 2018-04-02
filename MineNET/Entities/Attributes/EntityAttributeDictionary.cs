using System.Collections.Generic;
using System.Linq;

namespace MineNET.Entities.Attributes
{
    public class EntityAttributeDictionary
    {
        private Dictionary<string, EntityAttribute> attributes = new Dictionary<string, EntityAttribute>();

        public void AddAttribute(params EntityAttribute[] attributes)
        {
            for (int i = 0; i < attributes.Length; ++i)
            {
                this.attributes[attributes[i].Name] = attributes[i];
            }
        }

        public void RemoveAttribute(params string[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
            {
                if (this.attributes.ContainsKey(keys[i]))
                {
                    this.attributes.Remove(keys[i]);
                }
            }
        }

        public EntityAttribute[] ToArray
        {
            get
            {
                return this.attributes.Values.ToArray();
            }
        }
    }
}
