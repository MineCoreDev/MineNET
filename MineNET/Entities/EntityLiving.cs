using MineNET.Inventories;

namespace MineNET.Entities
{
    public abstract class EntityLiving : Entity, InventoryHolder
    {
        Inventory InventoryHolder.Inventory { get; set; }

        public Inventory Inventory { get; protected set; }
    }
}
