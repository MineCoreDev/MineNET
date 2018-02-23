using System;
using System.Collections.Generic;
using MineNET.Blocks;
using MineNET.Entities;
using MineNET.Items;

namespace MineNET.Inventories
{
    public abstract class BaseInventory : Inventory
    {
        private int size;

        private string name;

        private List<Player> viewers = new List<Player>();

        private InventoryHolder holder;

        private Dictionary<int, Item> slots = new Dictionary<int, Item>();

        public BaseInventory(InventoryHolder holder, int size, string name, Dictionary<int, Item> items = null)
        {
            this.holder = holder;
            this.size = size;
            this.name = name;
            if (items != null)
            {
                this.slots = items;
            }
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public Item GetItem(int index)
        {
            if (!this.slots.ContainsKey(index))
            {
                return Item.Get(BlockFactory.AIR);
            }
            return this.slots[index];
        }

        public bool SetItem(int index, Item item, bool send = true)
        {
            item = item.Clone();
            if (index < 0 || this.size <= index)
            {
                return false;
            }
            else if (item.ItemID == BlockFactory.AIR)
            {
                return this.Clear(index, send);
            }
            Item old = this.GetItem(index);
            this.slots.Add(index, item);
            this.OnSlotChange(index, old, send);
            return true;
        }

        public Item[] AddItem(params Item[] items)
        {
            List<Item> itemSlots = new List<Item>();
            for (int i = 0; i < this.size; ++i)
            {
                if (items[i].ItemID != BlockFactory.AIR && items[i].Count <= 0)
                {
                    itemSlots.Add(items[i].Clone());
                }
            }

            List<int> emptySlots = new List<int>();
            for (int i = 0; i < this.size; ++i)
            {
                Item item = this.GetItem(i);
                if (item.ItemID == BlockFactory.AIR || item.Count <= 0)
                {
                    emptySlots.Add(i);
                }

                for (int j = 0; j < itemSlots.Count; ++j)
                {
                    if (itemSlots[i].Equals(item) && item.Count < item.MaxStackSize)
                    {
                        int amount = Math.Min(item.MaxStackSize - item.Count, itemSlots[i].Count);
                        if (amount > 0)
                        {
                            itemSlots[i].Count -= amount;
                            item.Count -= amount;
                            this.SetItem(i, item);
                            if (itemSlots[i].Count <= 0)
                            {
                                itemSlots.Remove(itemSlots[i]);
                            }
                        }
                    }
                }
                if (itemSlots.Count < 1)
                {
                    break;
                }
            }
            if (itemSlots.Count > 0 && emptySlots.Count > 0)
            {
                for (int i = 0; i < itemSlots.Count; ++i)
                {
                    if (emptySlots.Count > 0)
                    {
                        Item slot = itemSlots[0];
                        int amount = Math.Min(slot.MaxStackSize, slot.Count);
                        slot.Count -= amount;
                        Item item = slot.Clone();
                        this.SetItem(i, item);
                        if (slot.Count <= 0)
                        {
                            itemSlots.Remove(slot);
                        }
                    }
                }
            }
            return itemSlots.ToArray();
        }

        public bool CanAddItem(Item item)
        {
            item = item.Clone();
            for (int i = 0; i < this.size; ++i)
            {
                Item slot = this.GetItem(i);
                if (item.Equals(slot))
                {
                    int diff;
                    if ((diff = slot.MaxStackSize - slot.Count) > 0)
                    {
                        item.Count -= diff;
                    }
                }
                else if (slot.ItemID == BlockFactory.AIR)
                {
                    item.Count -= 64;
                }

                if (item.Count <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public Item[] RemoveItem(params Item[] items)
        {
            List<Item> itemSlots = new List<Item>();
            for (int i = 0; i < this.size; ++i)
            {
                if (items[i].ItemID != BlockFactory.AIR && items[i].Count > 0)
                {
                    itemSlots.Add(items[i].Clone());
                }
            }

            for (int i = 0; i < this.size; ++i)
            {
                Item item = this.GetItem(i);
                if (item.ItemID == BlockFactory.AIR || item.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < itemSlots.Count; ++j)
                {
                    if (itemSlots[j].Equals(item))
                    {
                        int amount = Math.Min(item.Count, itemSlots[i].Count);
                        itemSlots[i].Count -= amount;
                        item.Count -= amount;
                        this.SetItem(i, item);
                        if (itemSlots[i].Count <= 0)
                        {
                            itemSlots.Remove(itemSlots[i]);
                        }
                    }
                }
                if (itemSlots.Count == 0)
                {
                    break;
                }
            }
            return itemSlots.ToArray();
        }

        public bool Clear(int index, bool send = true)
        {
            if (!this.slots.ContainsKey(index))
            {
                return true;
            }
            Item old = this.GetItem(index);
            this.slots.Remove(index);
            this.OnSlotChange(index, old, send);
            return true;
        }

        public void ClearAll()
        {
            for (int i = 0; i < this.size; ++i)
            {
                this.Clear(i, true);
            }
        }

        public bool Contains(Item item)
        {
            return true;
        }

        public void OnSlotChange(int index, Item item, bool send)
        {
            if (send)
            {
                this.SendSlot(index, this.viewers.ToArray());
            }
        }

        public void SendSlot(int index, params Player[] players)
        {

        }

        public void SendContents(params Player[] players)
        {

        }

        public InventoryHolder Holder
        {
            get
            {
                return this.holder;
            }

            set
            {
                this.holder = value;
            }
        }

        public List<Player> Viewers
        {
            get
            {
                return this.viewers;
            }
        }

        public bool Open(Player player)
        {
            //TODO : InventoryOpenEvent
            this.OnOpen(player);
            return true;
        }

        public virtual void OnOpen(Player player)
        {
            this.viewers.Add(player);
        }

        public void Close(Player player)
        {
            //TODO : InventoryCloseEvent
            this.OnClose(player);
        }

        public virtual void OnClose(Player player)
        {
            this.viewers.Remove(player);
        }
    }
}
