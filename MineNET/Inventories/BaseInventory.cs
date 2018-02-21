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

        public void AddItem(params Item[] items)
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
                }
            }
        }

        public bool CanAddItem(Item item)
        {
            return true;
        }

        public void RemoveItem(params Item[] items)
        {

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

        }

        public bool Contains(Item item)
        {
            return true;
        }

        public void OnSlotChange(int index, Item item, bool send)
        {

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
