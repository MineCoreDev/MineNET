using MineNET.Entities.Players;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using System;

namespace MineNET.Entities
{
    public class Effect : ICloneable<Effect>
    {
        public const int SPEED = 1;
        public const int SLOWNESS = 2;
        public const int HASTE = 3;
        public const int MINING_FATIGUE = 4;
        public const int STRENGTH = 5;
        public const int INSTANT_HEALTH = 6;
        public const int INSTANT_DAMAGE = 7;
        public const int JUMP_BOOST = 8;
        public const int NAUSEA = 9;
        public const int REGENERATION = 10;
        public const int RESISTANCE = 11;
        public const int FIRE_RESISTANCE = 12;
        public const int WATER_BREATHING = 13;
        public const int INVISIBILITY = 14;
        public const int BLINDNESS = 15;
        public const int NIGHT_VISION = 16;
        public const int HUNGER = 17;
        public const int WEAKNESS = 18;
        public const int POISON = 19;
        public const int WITHER = 20;
        public const int HEALTH_BOOST = 21;
        public const int ABSORPTION = 22;
        public const int SATURATION = 23;
        public const int LEVITATION = 24; //TODO
        public const int FATAL_POISON = 25;

        public static Effect GetEffect(int id, int duration = 20 * 30, int amplifier = 0, bool visible = true)
        {
            Effect effect;
            MineNET_Registries.Effect.TryGetValue(id, out effect);
            effect = effect?.Clone();

            if (effect == null)
            {
                return null;
            }

            effect.Duration = duration;
            effect.Amplifier = amplifier;
            effect.Visible = visible;
            return effect;
        }

        public static Effect GetEffect(string name, int duration = 20 * 30, int amplifier = 0, bool visible = true)
        {
            name = name.ToUpper();
            int id = 0;
            try
            {
                Effect effect = new Effect(0, "", new Color());
                id = (int) effect.GetType().GetField(name).GetValue(effect);
            }
            catch { }
            return Effect.GetEffect(id, duration, amplifier, visible);
        }

        public int ID { get; }
        public int Duration { get; set; }
        public int Amplifier { get; set; }
        public bool Visible { get; set; }

        public string Name { get; }
        public Color Color { get; }
        public bool IsBad { get; }
        public bool Ambient { get; set; } = false;

        public Effect(int id, string name, Color color, bool isBad = false)
        {
            this.ID = id;
            this.Name = name;
            this.Color = color;
            this.IsBad = isBad;
        }

        public void Add(EntityLiving entity, bool modify)
        {
            if (entity is Player)
            {
                MobEffectPacket pk = new MobEffectPacket();
                pk.EntityRuntimeId = entity.EntityID;
                pk.EffectId = this.ID;
                pk.Amplifier = this.Amplifier;
                pk.Particles = this.Visible;
                pk.Duration = this.Duration;

                if (modify)
                {
                    pk.EventId = MobEffectPacket.EVENT_MODIFY;
                }
                else
                {
                    pk.EventId = MobEffectPacket.EVENT_ADD;
                }

                ((Player) entity).SendPacket(pk);
            }
        }

        public void Remove(EntityLiving entity)
        {
            if (entity is Player)
            {
                MobEffectPacket pk = new MobEffectPacket();
                pk.EntityRuntimeId = entity.EntityID;
                pk.EffectId = this.ID;
                pk.Amplifier = 0;
                pk.Particles = false;
                pk.Duration = 0;
                pk.EventId = MobEffectPacket.EVENT_REMOVE;

                ((Player) entity).SendPacket(pk);
            }
        }

        public virtual Effect Clone()
        {
            return (Effect) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
