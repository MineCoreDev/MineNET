using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Inventories.Transactions.Action
{
    public abstract class InventoryAction
    {
        public ItemStack SourceItem { get; protected set; }

        public ItemStack TargetItem { get; protected set; }

        public InventoryAction(ItemStack sourceItem, ItemStack targetItem)
        {
            this.SourceItem = sourceItem;
            this.TargetItem = targetItem;
        }

        public virtual void AddInventory(InventoryTransaction transaction)
        {

        }

        public virtual bool OnPreExecute(Player player)
        {
            return true;
        }

        public abstract bool IsValid(Player player);

        public abstract bool Execute(Player player);

        public abstract void OnExecuteSuccess(Player player);

        public abstract void OnExecuteFail(Player player);
    }
}
