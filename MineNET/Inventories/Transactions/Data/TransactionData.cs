using MineNET.Items;

namespace MineNET.Inventories.Transactions.Data
{
    public interface TransactionData
    {
        int ActionType { get; set; }
        int HotbarSlot { get; set; }
        ItemStack ItemMainHand { get; set; }
    }
}
