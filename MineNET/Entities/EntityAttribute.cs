using System;

namespace MineNET.Entities
{
    public class EntityAttribute : ICloneable
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

        public EntityAttribute(string name, float max, float min, float defaultValue, bool shouldSend = true)
        {
            this.maxValue = max;
            this.minValue = min;
            this.value = defaultValue;
            this.defaultValue = defaultValue;
            this.name = name;
            this.shouldSend = shouldSend;
        }

        float maxValue;
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

        float minValue;
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

        float value;
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

        float defaultValue;
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

        string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        bool shouldSend;
        public bool ShouldSend
        {
            get
            {
                return shouldSend;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static EntityAttribute GetAttribute(int id)
        {
            EntityAttribute att = null;
            if (id == ABSORPTION)
            {
                att = new EntityAttribute("minecraft:absorption", 340282346638528859811704183484516925440.0f, 0f, 0f);
            }
            else if (id == SATURATION)
            {
                att = new EntityAttribute("minecraft:player.saturation", 20.0f, 0f, 5.0f);
            }
            else if (id == EXHAUSTION)
            {
                att = new EntityAttribute("minecraft:player.exhaustion", 5.0f, 0f, 0.41f);
            }
            else if (id == KNOCKBACK_RESISTANCE)
            {
                att = new EntityAttribute("minecraft:knockback_resistance", 1.0f, 0.0f, 0.0f);
            }
            else if (id == HEALTH)
            {
                att = new EntityAttribute("minecraft:health", 20.0f, 0.0f, 20.0f);
            }
            else if (id == MOVEMENT_SPEED)
            {
                att = new EntityAttribute("minecraft:movement", 340282346638528859811704183484516925440.0f, 0.0f, 0.10f);
            }
            else if (id == FOLLOW_RANGE)
            {
                att = new EntityAttribute("minecraft:follow_range", 2048.0f, 0.0f, 16.0f, false);
            }
            else if (id == FOOD)
            {
                att = new EntityAttribute("minecraft:player.hunger", 20.0f, 0.0f, 20.0f);
            }
            else if (id == ATTACK_DAMAGE)
            {
                att = new EntityAttribute("minecraft:attack_damage", 340282346638528859811704183484516925440.0f, 0.0f, 1.0f, false);
            }
            else if (id == EXPERIENCE_LEVEL)
            {
                att = new EntityAttribute("minecraft:player.level", 24791.0f, 0.0f, 0.0f);
            }
            else if (id == EXPERIENCE)
            {
                att = new EntityAttribute("minecraft:player.experience", 1.0f, 0.0f, 0.0f);
            }

            return att;
        }
    }
}
