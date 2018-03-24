namespace MineNET.Events.InventoryEvents
{
    public class InventoryEvents : MineNETEvents
    {
        public static event EventHandler<InventoryCloseEventArgs> InventoryClose;
        public static void OnInventoryClose(InventoryCloseEventArgs args)
        {
            InventoryClose?.Invoke(args);
        }

        public static event EventHandler<InventoryOpenEventArgs> InventoryOpen;
        public static void OnInventoryOpen(InventoryOpenEventArgs args)
        {
            InventoryOpen?.Invoke(args);
        }

        public static event EventHandler<InventoryTransactionEventArgs> InventoryTransaction;
        public static void OnInventoryTransaction(InventoryTransactionEventArgs args)
        {
            InventoryTransaction?.Invoke(args);
        }
    }
}
