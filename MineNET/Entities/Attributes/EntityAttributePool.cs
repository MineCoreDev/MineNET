using System.Collections.Generic;

namespace MineNET.Entities.Attributes
{
    public class EntityAttributePool
    {
        public static EntityAttributePool Instance
        {
            get;
            private set;
        }

        public virtual SortedList<int, EntityAttribute> Pool { get; } = new SortedList<int, EntityAttribute>();

        public EntityAttributePool()
        {
            Instance = this;
            this.Init();
        }

        public virtual void Init()
        {
            this.Pool[EntityAttribute.ABSORPTION] = new EntityAttribute("minecraft:absorption", 340282346638528859811704183484516925440.0f, 0f, 0f);
            this.Pool[EntityAttribute.SATURATION] = new EntityAttribute("minecraft:player.saturation", 20.0f, 0f, 5.0f);
            this.Pool[EntityAttribute.EXHAUSTION] = new EntityAttribute("minecraft:player.exhaustion", 5.0f, 0f, 0.41f);
            this.Pool[EntityAttribute.KNOCKBACK_RESISTANCE] = new EntityAttribute("minecraft:knockback_resistance", 1.0f, 0.0f, 0.0f);
            this.Pool[EntityAttribute.HEALTH] = new EntityAttribute("minecraft:health", 20.0f, 0.0f, 20.0f);
            this.Pool[EntityAttribute.MOVEMENT_SPEED] = new EntityAttribute("minecraft:movement", 340282346638528859811704183484516925440.0f, 0.0f, 0.10f);
            this.Pool[EntityAttribute.FOLLOW_RANGE] = new EntityAttribute("minecraft:follow_range", 2048.0f, 0.0f, 16.0f, false);
            this.Pool[EntityAttribute.FOOD] = new EntityAttribute("minecraft:player.hunger", 20.0f, 0.0f, 20.0f);
            this.Pool[EntityAttribute.ATTACK_DAMAGE] = new EntityAttribute("minecraft:attack_damage", 340282346638528859811704183484516925440.0f, 0.0f, 1.0f, false);
            this.Pool[EntityAttribute.EXPERIENCE_LEVEL] = new EntityAttribute("minecraft:player.level", 24791.0f, 0.0f, 0.0f);
            this.Pool[EntityAttribute.EXPERIENCE] = new EntityAttribute("minecraft:player.experience", 1.0f, 0.0f, 0.0f);
        }

        public virtual void RegisterAttribute(int id, EntityAttribute attribute)
        {
            this.Pool[id] = attribute;
        }

        public virtual EntityAttribute GetAttribute(int id)
        {
            return this.Pool[id];
        }
    }
}
