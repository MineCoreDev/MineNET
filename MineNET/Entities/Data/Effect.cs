using System;
using System.Drawing;
using MineNET.Entities.Players;
using MineNET.Network.Packets;
using MineNET.Utils;

namespace MineNET.Entities.Data
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
            if (id == Effect.SPEED)
            {
                effect = new Effect(Effect.SPEED, LangManager.GetString("effect.speed"), Color.FromArgb(124, 175, 198));
            }
            else if (id == Effect.SLOWNESS)
            {
                effect = new Effect(Effect.SLOWNESS, LangManager.GetString("effect.slowness"), Color.FromArgb(90, 108, 129), true);
            }
            else if (id == Effect.HASTE)
            {
                effect = new Effect(Effect.HASTE, LangManager.GetString("effect.haste"), Color.FromArgb(217, 192, 67));
            }
            else if (id == Effect.MINING_FATIGUE)
            {
                effect = new Effect(Effect.MINING_FATIGUE, LangManager.GetString("effect.mining_fatigue"), Color.FromArgb(74, 66, 23), true);
            }
            else if (id == Effect.STRENGTH)
            {
                effect = new Effect(Effect.STRENGTH, LangManager.GetString("effect.strength"), Color.FromArgb(147, 36, 35));
            }
            else if (id == Effect.INSTANT_HEALTH)
            {
                effect = new Effect(Effect.INSTANT_HEALTH, LangManager.GetString("effect.instant_health"), Color.FromArgb(248, 36, 35));
            }
            else if (id == Effect.INSTANT_DAMAGE)
            {
                effect = new Effect(Effect.INSTANT_DAMAGE, LangManager.GetString("effect.instant_damage"), Color.FromArgb(67, 10, 9), true);
            }
            else if (id == Effect.JUMP_BOOST)
            {
                effect = new Effect(Effect.JUMP_BOOST, LangManager.GetString("effect.jumpboost"), Color.FromArgb(34, 255, 76));
            }
            else if (id == Effect.NAUSEA)
            {
                effect = new Effect(Effect.NAUSEA, LangManager.GetString("effect.nausea"), Color.FromArgb(85, 29, 74), true);
            }
            else if (id == Effect.REGENERATION)
            {
                effect = new Effect(Effect.REGENERATION, LangManager.GetString("effect.regeneration"), Color.FromArgb(205, 92, 171));
            }
            else if (id == Effect.RESISTANCE)
            {
                effect = new Effect(Effect.RESISTANCE, LangManager.GetString("effect.resistance"), Color.FromArgb(153, 69, 58));
            }
            else if (id == Effect.FIRE_RESISTANCE)
            {
                effect = new Effect(Effect.FIRE_RESISTANCE, LangManager.GetString("effect.fire_resistance"), Color.FromArgb(228, 154, 58));
            }
            else if (id == Effect.WATER_BREATHING)
            {
                effect = new Effect(Effect.WATER_BREATHING, LangManager.GetString("effect.water_breathing"), Color.FromArgb(46, 82, 153));
            }
            else if (id == Effect.INVISIBILITY)
            {
                effect = new Effect(Effect.INVISIBILITY, LangManager.GetString("effect.invisibility"), Color.FromArgb(127, 131, 146));
            }
            else if (id == Effect.BLINDNESS)
            {
                effect = new Effect(Effect.BLINDNESS, LangManager.GetString("effect.blindness"), Color.FromArgb(191, 192, 192), true);
            }
            else if (id == NIGHT_VISION)
            {
                effect = new Effect(Effect.NIGHT_VISION, LangManager.GetString("effect.night_vision"), Color.FromArgb(0, 0, 139));
            }
            else if (id == Effect.HUNGER)
            {
                effect = new Effect(Effect.HUNGER, LangManager.GetString("effect.hunger"), Color.FromArgb(46, 139, 87), true);
            }
            else if (id == Effect.WEAKNESS)
            {
                effect = new Effect(Effect.WEAKNESS, LangManager.GetString("effect.weakness"), Color.FromArgb(72, 77, 72), true);
            }
            else if (id == Effect.POISON)
            {
                effect = new Effect(Effect.POISON, LangManager.GetString("effect.poison"), Color.FromArgb(78, 147, 49), true);
            }
            else if (id == Effect.WITHER)
            {
                effect = new Effect(Effect.WITHER, LangManager.GetString("effect.wither"), Color.FromArgb(53, 42, 39), true);
            }
            else if (id == Effect.HEALTH_BOOST)
            {
                effect = new Effect(Effect.HEALTH_BOOST, LangManager.GetString("effect.health_boost"), Color.FromArgb(248, 125, 35));
            }
            else if (id == Effect.ABSORPTION)
            {
                effect = new Effect(Effect.ABSORPTION, LangManager.GetString("effect.absorption"), Color.FromArgb(36, 107, 251));
            }
            else if (id == Effect.SATURATION)
            {
                effect = new Effect(Effect.SATURATION, LangManager.GetString("effect.saturation"), Color.FromArgb(255, 0, 255));
            }
            else if (id == Effect.LEVITATION)
            {
                effect = new Effect(Effect.LEVITATION, LangManager.GetString("effect.levitation"), Color.FromArgb(206, 255, 255));
            }
            else if (id == Effect.FATAL_POISON)
            {
                effect = new Effect(Effect.FATAL_POISON, LangManager.GetString("effect.fatal_poison"), Color.FromArgb(78, 147, 49), true);
            }
            else
            {
                throw new Exception($"EffectId {id} not found");
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
