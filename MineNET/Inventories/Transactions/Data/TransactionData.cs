using MineNET.Items;

namespace MineNET.Inventories.Transactions.Data
{
    public interface TransactionData
    {
        int ActionType { get; set; }
        int HotbarSlot { get; set; }
        Item ItemMainHand { get; set; }
    }
}
