using MineNET.Entities;
using MineNET.Items;
using System.Collections.Generic;

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

        Item GetItem(int index);

        bool SetItem(int index, Item item, bool send);

        Item[] AddItem(params Item[] items);

        bool CanAddItem(Item item);

        Item[] RemoveItem(params Item[] items);

        bool Clear(int index, bool send);

        void ClearAll();

        bool Contains(Item item);

        void OnSlotChange(int index, Item item, bool send);

        void SendSlot(int index, params Player[] players);

        void SendContents(params Player[] players);

        InventoryHolder Holder
        {
            get;
            set;
        }

        List<Player> Viewers
        {
            get;
        }


        bool Open(Player player);

        void OnOpen(Player player);

        void Close(Player player);

        void OnClose(Player player);
    }
}
