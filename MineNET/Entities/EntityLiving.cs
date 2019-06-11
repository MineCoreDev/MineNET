using System;
using System.Collections.Generic;
using System.Linq;
using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
using MineNET.Inventories;
using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class EntityLiving : Entity, InventoryHolder
    {
        protected Dictionary<int, Effect> Effects { get; } = new Dictionary<int, Effect>();

        public EntityLiving(Chunk chunk, CompoundTag tag) : base(chunk, tag)
        {
        }

        protected override void EntityInit(CompoundTag nbt)
        {
            base.EntityInit(nbt);

            this.Attributes.AddAttribute(EntityAttribute.HEALTH);
            this.Attributes.AddAttribute(EntityAttribute.ABSORPTION);
            this.Attributes.AddAttribute(EntityAttribute.KNOCKBACK_RESISTANCE);
            this.Attributes.AddAttribute(EntityAttribute.MOVEMENT_SPEED);
        }

        public virtual float Health
        {
            get { return this.Attributes.GetAttribute("minecraft:health").Value; }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:health");
                attribute.Value = value;
                this.Attributes.AddAttribute(attribute);
            }
        }

        public virtual float MaxHealth
        {
            get { return this.Attributes.GetAttribute("minecraft:health").MaxValue; }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:health");
                attribute.MaxValue = value;
                this.Attributes.AddAttribute(attribute);
            }
        }

        public virtual float Absorption
        {
            get { return this.Attributes.GetAttribute("minecraft:absorption").Value; }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:absorption");
                attribute.Value = value;
                this.Attributes.AddAttribute(attribute);
            }
        }

        public EntityInventory Inventory { get; protected set; }

        Inventory InventoryHolder.Inventory
        {
            get
            {
                return this.Inventory;
            }
        }

        public bool HasEffect(int id)
        {
            return this.Effects.ContainsKey(id);
        }

        public Effect GetEffect(int id)
        {
            if (this.HasEffect(id))
            {
                return this.Effects[id];
            }

            return null;
        }

        public Effect[] GetEffects()
        {
            return this.Effects.Values.ToArray();
        }

        public void AddEffect(Effect effect)
        {
            if (effect == null)
            {
                return;
            }

            Effect old = this.GetEffect(effect.ID);
            if (old != null)
            {
                if (Math.Abs(effect.Amplifier) < Math.Abs(old.Amplifier))
                {
                    return;
                }

                if (Math.Abs(effect.Amplifier) == Math.Abs(old.Amplifier) && effect.Duration < old.Duration)
                {
                    return;
                }

                effect.Add(this, true);
            }
            else
            {
                effect.Add(this, false);
            }

            this.Effects[effect.ID] = effect;

            this.RecalculateEffectColor();
        }

        public void RemoveEffect(int id)
        {
            if (!this.HasEffect(id))
            {
                return;
            }

            Effect effect = this.GetEffect(id);
            effect.Remove(this);

            this.Effects.Remove(id);
            this.RecalculateEffectColor();
        }

        public void RemoveAllEffect()
        {
            if (this.Effects.Count < 1)
            {
                return;
            }

            int[] ids = this.Effects.Keys.ToArray();
            for (int i = 0; i < ids.Length; ++i)
            {
                this.RemoveEffect(ids[i]);
            }
        }

        protected void RecalculateEffectColor()
        {
            int[] color = new int[3];
            int count = 0;
            bool ambient = true;
            foreach (int id in this.Effects.Keys)
            {
                Effect effect = this.GetEffect(id);
                if (effect.Visible)
                {
                    Color c = effect.Color;
                    color[0] += c.R * (effect.Amplifier + 1);
                    color[1] += c.G * (effect.Amplifier + 1);
                    color[2] += c.B * (effect.Amplifier + 1);
                    count += effect.Amplifier + 1;
                    if (!effect.Ambient)
                    {
                        ambient = false;
                    }
                }
            }

            if (count > 0)
            {
                int r = (color[0] / count) & 0xff;
                int g = (color[1] / count) & 0xff;
                int b = (color[2] / count) & 0xff;
                Color rgb = new Color(r, g, b);

                this.SetDataProperty(new EntityDataInt(DATA_POTION_COLOR, rgb.RGB), false);
                this.SetDataProperty(new EntityDataByte(DATA_POTION_AMBIENT, (byte) (ambient ? 1 : 0)), true);
            }
            else
            {
                this.SetDataProperty(new EntityDataInt(DATA_POTION_COLOR, 0), false);
                this.SetDataProperty(new EntityDataByte(DATA_POTION_AMBIENT, 0), true);
            }
        }
    }
}