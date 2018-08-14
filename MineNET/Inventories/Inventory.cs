using System.Collections.Generic;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Inventories
{
    public interface Inventory
    {
        int Size
        {
            get;
        }

        byte Type
        {
            get;
        }

        int MaxStackSize
        {
            get;
        }

        ItemStack GetItem(int index);

        bool SetItem(int index, ItemStack item, bool send = true);

        ItemStack[] AddItem(params ItemStack[] items);

        bool CanAddItem(ItemStack item);

        ItemStack[] RemoveItem(params ItemStack[] items);

        bool Contains(ItemStack item);

        bool Clear(int index, bool send);

        void ClearAll();

        void OnSlotChange(int index, ItemStack item, bool send);

        void SendSlot(int index, params Player[] players);

        void SendContents(params Player[] players);

        InventoryHolder Holder
        {
            get;
        }

        List<Player> Viewers
        {
            get;
        }


        bool Open(Player player);

        void OnOpen(Player player);

        void Close(Player player);

        void OnClose(Player player);

        void SaveNBT();
    }
}
