using System;

namespace MineNET.Events.InventoryEvents
{
    public sealed class InventoryEvents
    {
        public event EventHandler<InventoryCloseEventArgs> InventoryClose;
        public void OnInventoryClose(object sender, InventoryCloseEventArgs e)
        {
            this.InventoryClose?.Invoke(sender, e);
        }

        public event EventHandler<InventoryOpenEventArgs> InventoryOpen;
        public void OnInventoryOpen(object sender, InventoryOpenEventArgs e)
        {
            this.InventoryOpen?.Invoke(sender, e);
        }

        public event EventHandler<InventoryTransactionEventArgs> InventoryTransaction;
        public void OnInventoryTransaction(object sender, InventoryTransactionEventArgs e)
        {
            this.InventoryTransaction?.Invoke(sender, e);
        }
    }
}
