using System;

namespace MineNET.Entities.Attributes
{
    public struct EntityAttribute : ICloneable<EntityAttribute>
    {
        public static readonly EntityAttribute ABSORPTION = new EntityAttribute("minecraft:absorption", 340282346638528859811704183484516925440.0f, 0f, 0f);
        public static readonly EntityAttribute SATURATION = new EntityAttribute("minecraft:player.saturation", 20.0f, 0f, 5.0f);
        public static readonly EntityAttribute EXHAUSTION = new EntityAttribute("minecraft:player.exhaustion", 4.0f, 0f, 0.41f);
        public static readonly EntityAttribute KNOCKBACK_RESISTANCE = new EntityAttribute("minecraft:knockback_resistance", 1.0f, 0.0f, 0.0f);
        public static readonly EntityAttribute HEALTH = new EntityAttribute("minecraft:health", 20.0f, 0.0f, 20.0f);
        public static readonly EntityAttribute MOVEMENT_SPEED = new EntityAttribute("minecraft:movement", 340282346638528859811704183484516925440.0f, 0.0f, 0.10f);
        public static readonly EntityAttribute FOLLOW_RANGE = new EntityAttribute("minecraft:follow_range", 2048.0f, 0.0f, 16.0f, false);
        public static readonly EntityAttribute HUNGER = new EntityAttribute("minecraft:player.hunger", 20.0f, 0.0f, 20.0f);
        public static readonly EntityAttribute ATTACK_DAMAGE = new EntityAttribute("minecraft:attack_damage", 340282346638528859811704183484516925440.0f, 0.0f, 1.0f, false);
        public static readonly EntityAttribute EXPERIENCE_LEVEL = new EntityAttribute("minecraft:player.level", 24791.0f, 0.0f, 0.0f);
        public static readonly EntityAttribute EXPERIENCE = new EntityAttribute("minecraft:player.experience", 1.0f, 0.0f, 0.0f);
        public static readonly EntityAttribute LUCK = new EntityAttribute("minecraft:luck", 1024.0f, 0.0f, 0.0f, false);
        public static readonly EntityAttribute FALL_DAMAGE = new EntityAttribute("minecraft:fall_damage", 340282346638528859811704183484516925440.0f, 0.0f, 1.0f, false);

        public EntityAttribute(string name, float max, float min, float defaultValue, bool shouldSend = true)
        {
            this.maxValue = max;
            this.minValue = min;
            this.value = defaultValue;
            this.defaultValue = defaultValue;
            this.Name = name;
            this.ShouldSend = shouldSend;
        }

        public string Name { get; set; }

        public bool ShouldSend { get; }

        private float maxValue;
        public float MaxValue
        {
            get
            {
                return this.maxValue;
            }

            set
            {
                this.maxValue = value;
                if (value < this.value)
                {
                    this.value = value;
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
                this.minValue = value;
                if (value > this.value)
                {
                    this.value = value;
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
                if (this.minValue <= value && value <= this.maxValue)
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
                if (this.minValue <= value && value <= this.maxValue)
                {
                    this.defaultValue = value;
                }
            }
        }

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
