using System;
using System.Collections.Generic;
using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Events.InventoryEvents;
using MineNET.Items;
using MineNET.Network.Packets;

namespace MineNET.Inventories
{
    public abstract class BaseInventory : Inventory
    {
        private List<Player> viewers = new List<Player>();

        protected InventoryHolder holder;

        private Dictionary<int, Item> slots = new Dictionary<int, Item>();

        public BaseInventory(InventoryHolder holder, Dictionary<int, Item> items = null)
        {
            this.holder = holder;
            if (items != null)
            {
                this.slots = items;
            }
        }

        public abstract int Size
        {
            get;
        }

        public abstract byte Type
        {
            get;
        }

        public virtual int MaxStackSize
        {
            get
            {
                return 64;
            }
        }

        public virtual Item GetItem(int index)
        {
            if (!this.slots.ContainsKey(index))
            {
                return Item.Get(BlockFactory.AIR);
            }
            return this.slots[index];
        }

        public virtual bool SetItem(int index, Item item, bool send = true)
        {
            item = item.Clone();
            if (index < 0 || this.Size <= index)
            {
                return false;
            }
            else if (item.ID == BlockFactory.AIR)
            {
                return this.Clear(index, send);
            }
            Item old = this.GetItem(index);
            this.slots[index] = item;
            this.OnSlotChange(index, old, send);
            return true;
        }

        public virtual Item[] AddItem(params Item[] items)
        {
            List<Item> itemSlots = new List<Item>();
            for (int i = 0; i < this.Size; ++i)
            {
                if (items[i].ID != BlockFactory.AIR && items[i].Count <= 0)
                {
                    itemSlots.Add(items[i].Clone());
                }
            }

            List<int> emptySlots = new List<int>();
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = this.GetItem(i);
                if (item.ID == BlockFactory.AIR || item.Count <= 0)
                {
                    emptySlots.Add(i);
                }

                for (int j = 0; j < itemSlots.Count; ++j)
                {
                    if (itemSlots[i].Equals(item) && item.Count < item.MaxStackSize)
                    {
                        int amount = Math.Min(item.MaxStackSize - item.Count, itemSlots[i].Count);
                        amount = Math.Min(amount, this.MaxStackSize);
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
                        amount = Math.Min(amount, this.MaxStackSize);
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

        public virtual bool CanAddItem(Item item)
        {
            item = item.Clone();
            for (int i = 0; i < this.Size; ++i)
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
                else if (slot.ID == BlockFactory.AIR)
                {
                    item.Count -= this.MaxStackSize;
                }

                if (item.Count <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual Item[] RemoveItem(params Item[] items)
        {
            List<Item> itemSlots = new List<Item>();
            for (int i = 0; i < this.Size; ++i)
            {
                if (items[i].ID != BlockFactory.AIR && items[i].Count > 0)
                {
                    itemSlots.Add(items[i].Clone());
                }
            }

            for (int i = 0; i < this.Size; ++i)
            {
                Item item = this.GetItem(i);
                if (item.ID == BlockFactory.AIR || item.Count <= 0)
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

        public virtual bool Contains(Item item)
        {
            int count = Math.Max(1, item.Count);
            foreach (Item slot in this.slots.Values)
            {
                if (item.Equals(slot))
                {
                    count -= slot.Count;
                    if (count <= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual bool Clear(int index, bool send = true)
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

        public virtual void ClearAll()
        {
            for (int i = 0; i < this.Size; ++i)
            {
                this.Clear(i, true);
            }
        }

        public virtual void OnSlotChange(int index, Item item, bool send)
        {
            if (send)
            {
                this.SendSlot(index, this.viewers.ToArray());
            }
        }

        public virtual void SendSlot(int index, params Player[] players)
        {
            InventorySlotPacket pk = new InventorySlotPacket();
            pk.Slot = (uint) index;
            pk.Item = this.GetItem(index);
            for (int i = 0; i < players.Length; ++i)
            {
                Player player = players[i];
                pk.InventoryId = this.Type;
                player.SendPacket(pk);
            }
        }

        public virtual void SendContents(params Player[] players)
        {
            InventoryContentPacket pk = new InventoryContentPacket();
            pk.Items = new Item[this.Size];
            for (int i = 0; i < this.Size; ++i)
            {
                pk.Items[i] = this.GetItem(i);
            }
            for (int i = 0; i < players.Length; ++i)
            {
                Player player = players[i];
                pk.InventoryId = this.Type;
                player.SendPacket(pk);
            }
        }

        public InventoryHolder Holder
        {
            get
            {
                return this.holder;
            }

            protected set
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

        public virtual bool Open(Player player)
        {
            InventoryOpenEventArgs inventoryOpenEventArgs = new InventoryOpenEventArgs(this, player);
            InventoryEvents.OnInventoryOpen(inventoryOpenEventArgs);
            if (inventoryOpenEventArgs.IsCancel)
            {
                return false;
            }
            this.OnOpen(player);
            return true;
        }

        public virtual void OnOpen(Player player)
        {
            this.viewers.Add(player);
        }

        public virtual void Close(Player player)
        {
            InventoryCloseEventArgs inventoryCloseEventArgs = new InventoryCloseEventArgs(this, player);
            InventoryEvents.OnInventoryClose(inventoryCloseEventArgs);
            this.OnClose(player);
        }

        public virtual void OnClose(Player player)
        {
            this.viewers.Remove(player);
        }
    }
}
