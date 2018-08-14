using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Inventories.Transactions.Action
{
    public class SlotChangeAction : InventoryAction
    {
        public Inventory Inventory { get; protected set; }
        public int InventorySlot { get; protected set; }

        public SlotChangeAction(Inventory inventory, int inventorySlot, ItemStack sourceItem, ItemStack targetItem) : base(sourceItem, targetItem)
        {
            this.Inventory = inventory;
            this.InventorySlot = inventorySlot;
        }

        public override void AddInventory(InventoryTransaction transaction)
        {
            transaction.AddInventory(this.Inventory);
        }

        public override bool IsValid(Player player)
        {
            return this.Inventory.GetItem(this.InventorySlot) == this.SourceItem;
        }

        public override bool Execute(Player player)
        {
            return this.Inventory.SetItem(this.InventorySlot, this.TargetItem);
        }

        public override void OnExecuteSuccess(Player player)
        {
            this.Inventory.SendSlot(this.InventorySlot, this.Inventory.Viewers.ToArray());
        }

        public override void OnExecuteFail(Player player)
        {
            this.Inventory.SendSlot(this.InventorySlot, player);
        }
    }
}
