using System;
using System.Collections.Generic;
using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Events.InventoryEvents;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Inventories
{
    public abstract class BaseInventory : Inventory
    {
        private List<Player> viewers = new List<Player>();

        protected Dictionary<int, ItemStack> slots = new Dictionary<int, ItemStack>();

        public BaseInventory(InventoryHolder holder, Dictionary<int, ItemStack> items = null)
        {
            this.Holder = holder;
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

        public abstract String Name
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

        public virtual ItemStack GetItem(int index)
        {
            if (!this.slots.ContainsKey(index))
            {
                return new ItemStack(Item.Get(BlockIDs.AIR), 0, 0);
            }
            return this.slots[index];
        }

        public virtual bool SetItem(int index, ItemStack item, bool send = true)
        {
            item = item.Clone();
            if (index < 0 || this.Size <= index)
            {
                return false;
            }
            else if (item.Item.ID == BlockIDs.AIR)
            {
                return this.Clear(index, send);
            }
            ItemStack old = this.GetItem(index);
            this.slots[index] = item;
            this.OnSlotChange(index, old, send);
            return true;
        }

        public virtual ItemStack[] AddItem(params ItemStack[] items)
        {
            List<ItemStack> itemSlots = new List<ItemStack>();
            for (int i = 0; i < items.Length; ++i)
            {
                if (items[i].Item.ID != BlockIDs.AIR && items[i].Count > 0)
                {
                    itemSlots.Add(items[i].Clone());
                }
            }

            List<int> emptySlots = new List<int>();
            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack item = this.GetItem(i);
                if (item.Item.ID == BlockIDs.AIR || item.Count <= 0)
                {
                    emptySlots.Add(i);
                }

                for (int j = 0; j < itemSlots.Count; ++j)
                {
                    ItemStack slot = itemSlots[j];
                    if (slot.Equals(item, true, false) && item.Count < item.Item.MaxStackSize)
                    {
                        int amount = Math.Min(item.Item.MaxStackSize - item.Count, slot.Count);
                        amount = Math.Min(amount, this.MaxStackSize);
                        if (amount > 0)
                        {
                            slot.Count -= amount;
                            item.Count += amount;
                            this.SetItem(i, item);
                            if (slot.Count <= 0)
                            {
                                itemSlots.Remove(slot);
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
                for (int i = 0; i < emptySlots.Count; ++i)
                {
                    if (itemSlots.Count > 0)
                    {
                        ItemStack slot = itemSlots[0];
                        int amount = Math.Min(slot.Item.MaxStackSize, slot.Count);
                        amount = Math.Min(amount, this.MaxStackSize);
                        slot.Count -= amount;
                        ItemStack item = slot.Clone();
                        item.Count = amount;
                        this.SetItem(emptySlots[i], item);
                        if (slot.Count <= 0)
                        {
                            itemSlots.Remove(slot);
                        }
                    }
                }
            }
            return itemSlots.ToArray();
        }

        public virtual bool CanAddItem(ItemStack item)
        {
            item = item.Clone();
            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack slot = this.GetItem(i);
                if (item.Equals(slot))
                {
                    int diff;
                    if ((diff = slot.Item.MaxStackSize - slot.Count) > 0)
                    {
                        item.Count -= diff;
                    }
                }
                else if (slot.Item.ID == BlockIDs.AIR)
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

        public virtual ItemStack[] RemoveItem(params ItemStack[] items)
        {
            List<ItemStack> itemSlots = new List<ItemStack>();
            for (int i = 0; i < this.Size; ++i)
            {
                if (items[i].Item.ID != BlockIDs.AIR && items[i].Count > 0)
                {
                    itemSlots.Add(items[i].Clone());
                }
            }

            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack item = this.GetItem(i);
                if (item.Item.ID == BlockIDs.AIR || item.Count <= 0)
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

        public virtual bool Contains(ItemStack item)
        {
            int count = Math.Max(1, item.Count);
            foreach (ItemStack slot in this.slots.Values)
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
            ItemStack old = this.GetItem(index);
            this.slots[index] = new ItemStack(Item.Get(0), 0, 0);
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

        public virtual void OnSlotChange(int index, ItemStack item, bool send)
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
            pk.Items = new ItemStack[this.Size];
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

        public InventoryHolder Holder { get; protected set; }

        public List<Player> Viewers
        {
            get
            {
                return this.viewers;
            }
        }

        public virtual bool Open(Player player)
        {
            InventoryOpenEventArgs args = new InventoryOpenEventArgs(this, player);
            Server.Instance.Event.Inventory.OnInventoryOpen(this, args);
            if (args.IsCancel)
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
            InventoryCloseEventArgs args = new InventoryCloseEventArgs(this, player);
            Server.Instance.Event.Inventory.OnInventoryClose(this, args);
            this.OnClose(player);
        }

        public virtual void OnClose(Player player)
        {
            this.viewers.Remove(player);
        }

        public virtual void LoadNBT(CompoundTag nbt)
        {
            if (!nbt.Exist(this.Name))
            {
                ListTag list = new ListTag(this.Name, NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    list.Add(NBTIO.WriteItem(new ItemStack(Item.Get(0), 0, 0)));
                }
                nbt.PutList(list);
            }

            ListTag items = nbt.GetList(this.Name);
            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }
        }

        public virtual CompoundTag SaveNBT()
        {
            CompoundTag nbt = new CompoundTag();
            ListTag list = new ListTag(this.Name, NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                list.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            nbt.PutList(list);
            return nbt;
        }
    }
}
