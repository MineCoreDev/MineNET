using System.Collections.Generic;

namespace MineNET.Entities.Attributes
{
    public static class EntityAttributePool
    {
        public static SortedList<int, EntityAttribute> Pool { get; } = new SortedList<int, EntityAttribute>();

        static EntityAttributePool()
        {
            Init();
        }

        public static void Init()
        {
            Pool[EntityAttribute.ABSORPTION] = new EntityAttribute("minecraft:absorption", 340282346638528859811704183484516925440.0f, 0f, 0f);
            Pool[EntityAttribute.SATURATION] = new EntityAttribute("minecraft:player.saturation", 20.0f, 0f, 5.0f);
            Pool[EntityAttribute.EXHAUSTION] = new EntityAttribute("minecraft:player.exhaustion", 5.0f, 0f, 0.41f);
            Pool[EntityAttribute.KNOCKBACK_RESISTANCE] = new EntityAttribute("minecraft:knockback_resistance", 1.0f, 0.0f, 0.0f);
            Pool[EntityAttribute.HEALTH] = new EntityAttribute("minecraft:health", 20.0f, 0.0f, 20.0f);
            Pool[EntityAttribute.MOVEMENT_SPEED] = new EntityAttribute("minecraft:movement", 340282346638528859811704183484516925440.0f, 0.0f, 0.10f);
            Pool[EntityAttribute.FOLLOW_RANGE] = new EntityAttribute("minecraft:follow_range", 2048.0f, 0.0f, 16.0f, false);
            Pool[EntityAttribute.FOOD] = new EntityAttribute("minecraft:player.hunger", 20.0f, 0.0f, 20.0f);
            Pool[EntityAttribute.ATTACK_DAMAGE] = new EntityAttribute("minecraft:attack_damage", 340282346638528859811704183484516925440.0f, 0.0f, 1.0f, false);
            Pool[EntityAttribute.EXPERIENCE_LEVEL] = new EntityAttribute("minecraft:player.level", 24791.0f, 0.0f, 0.0f);
            Pool[EntityAttribute.EXPERIENCE] = new EntityAttribute("minecraft:player.experience", 1.0f, 0.0f, 0.0f);
        }

        public static void RegisterAttribute(int id, EntityAttribute attribute)
        {
            Pool[id] = attribute;
        }

        public static void UnRegisterAttribute(int id)
        {
            if (Pool.ContainsKey(id))
            {
                Pool.Remove(id);
            }
        }

        public static EntityAttribute GetAttribute(int id)
        {
            return Pool[id];
        }
    }
}
