using MineNET.Inventories;

namespace MineNET.Entities
{
    public abstract class EntityLiving : Entity, InventoryHolder
    {
        public Inventory Inventory { get; protected set; }
    }
}
