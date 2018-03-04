using MineNET.Inventories;

namespace MineNET.Entities
{
    public abstract class EntityLiving : Entity, InventoryHolder
    {
        public virtual Inventory GetInventory()
        {
            return null;
        }
    }
}
