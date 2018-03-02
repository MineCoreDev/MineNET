using MineNET.Inventories;

namespace MineNET.Entities
{
    public class EntityLiving : Entity, InventoryHolder
    {
        public virtual Inventory GetInventory()
        {
            return null;
        }
    }
}
