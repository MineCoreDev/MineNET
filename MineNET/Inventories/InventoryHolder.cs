using MineNET.Values;

namespace MineNET.Inventories
{
    public interface InventoryHolder : IVector3
    {
        Inventory GetInventory();
    }
}
