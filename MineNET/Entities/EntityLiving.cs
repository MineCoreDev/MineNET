using MineNET.Entities.Attributes;
using MineNET.Inventories;

namespace MineNET.Entities
{
    public abstract class EntityLiving : Entity, InventoryHolder
    {
        public Inventory Inventory { get; protected set; }

        public EntityAttributeDictionary Attributes { get; protected set; }

        public override void EntityInit()
        {
            this.Attributes = new EntityAttributeDictionary(this.EntityID);
            this.Attributes.AddAttribute(EntityAttribute.HEALTH);
            this.Attributes.AddAttribute(EntityAttribute.KNOCKBACK_RESISTANCE);
            this.Attributes.AddAttribute(EntityAttribute.MOVEMENT_SPEED);
        }

        public virtual float Health
        {
            get
            {
                return this.Attributes.GetAttribute("minecraft:health").Value;
            }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:health");
                attribute.Value = value;
                this.Attributes.AddAttribute(attribute);
            }
        }

        public virtual float MaxHealth
        {
            get
            {
                return this.Attributes.GetAttribute("minecraft:health").MaxValue;
            }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:health");
                attribute.MaxValue = value;
                this.Attributes.AddAttribute(attribute);
            }
        }
    }
}
