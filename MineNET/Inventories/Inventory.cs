using System.Collections.Generic;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Tags;

namespace MineNET.Inventories
{
    public interface Inventory
    {
        int Size { get; }

        byte Type { get; }

        int MaxStackSize { get; }

        string Name { get; }

        Item GetItem(int index);

        bool SetItem(int index, Item item, bool send = true);

        Item[] AddItem(params Item[] items);

        bool CanAddItem(Item item);

        Item[] RemoveItem(params Item[] items);

        bool Contains(Item item);

        bool Clear(int index, bool send);

        void ClearAll();

        void OnSlotChange(int index, Item item, bool send);

        void SendSlot(int index, params Player[] players);

        void SendContents(params Player[] players);

        InventoryHolder Holder { get; }

        List<Player> Viewers { get; }


        bool Open(Player player);

        void OnOpen(Player player);

        void Close(Player player);

        void OnClose(Player player);

        void LoadNBT(CompoundTag nbt);

        CompoundTag SaveNBT();
    }
}
