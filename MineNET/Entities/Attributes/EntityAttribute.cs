using System;
using MineNET.Utils;

namespace MineNET.Entities.Attributes
{
    public class EntityAttribute : ICloneable<EntityAttribute>
    {
        public const int ABSORPTION = 0;
        public const int SATURATION = 1;
        public const int EXHAUSTION = 2;
        public const int KNOCKBACK_RESISTANCE = 3;
        public const int HEALTH = 4;
        public const int MOVEMENT_SPEED = 5;
        public const int FOLLOW_RANGE = 6;
        public const int HUNGER = 7;
        public const int FOOD = 7;
        public const int ATTACK_DAMAGE = 8;
        public const int EXPERIENCE_LEVEL = 9;
        public const int EXPERIENCE = 10;

        public static EntityAttribute GetAttribute(int id)
        {
            return EntityAttributePool.GetAttribute(id);
        }

        public EntityAttribute(string name, float max, float min, float defaultValue, bool shouldSend = true)
        {
            this.maxValue = max;
            this.minValue = min;
            this.value = defaultValue;
            this.defaultValue = defaultValue;
            this.Name = name;
            this.ShouldSend = shouldSend;
        }

        private float maxValue;
        public float MaxValue
        {
            get
            {
                return this.maxValue;
            }

            set
            {
                if (value >= this.value)
                {
                    this.maxValue = value;
                }
            }
        }

        private float minValue;
        public float MinValue
        {
            get
            {
                return this.minValue;
            }

            set
            {
                if (value <= this.value)
                {
                    this.minValue = value;
                }
            }
        }

        private float value;
        public float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.maxValue >= value && this.minValue <= value)
                {
                    this.value = value;
                }
            }
        }

        private float defaultValue;
        public float DefaultValue
        {
            get
            {
                return this.defaultValue;
            }

            set
            {
                if (this.maxValue >= value && this.minValue <= value)
                {
                    this.defaultValue = value;
                }
            }
        }

        public string Name { get; set; }

        public bool ShouldSend { get; }

        public EntityAttribute Clone()
        {
            return (EntityAttribute) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
