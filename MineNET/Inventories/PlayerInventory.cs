using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class PlayerInventory : EntityInventory
    {
        private PlayerCursorInventory cursor;

        private PlayerEnderChestInventory ender;

        private Inventory opend = null;

        public PlayerInventory(Player player) : base(player, 36)
        {
            if (!player.NamedTag.Exist("Inventory"))
            {
                ListTag initItems = new ListTag("Inventory", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    initItems.Add(NBTIO.WriteItem(Item.Get(0, 0, 0), i));
                }
                player.NamedTag.PutList(initItems);
            }

            ListTag items = player.NamedTag.GetList("Inventory");
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }

            this.cursor = new PlayerCursorInventory(player);

            this.ender = new PlayerEnderChestInventory(player);
        }

        public override void SendSlot(int index, params Player[] players)
        {
            InventorySlotPacket pk = new InventorySlotPacket();
            pk.Slot = (uint) index;
            pk.Item = this.GetItem(index);
            pk.InventoryId = this.Type;
            Player player = this.Holder;
            player.SendPacket(pk);
        }

        public override void SendContents(params Player[] players)
        {
            InventoryContentPacket pk = new InventoryContentPacket();
            pk.Items = new Item[this.Size];
            for (int i = 0; i < this.Size; ++i)
            {
                pk.Items[i] = this.GetItem(i);
            }
            pk.InventoryId = this.Type;
            Player player = this.Holder;
            player.SendPacket(pk);
        }

        public override void OnSlotChange(int index, Item item, bool send)
        {
            base.OnSlotChange(index, item, send);

            if (send && index == this.MainHandSlot)
            {
                this.SendMainHand(this.Holder);
            }
        }

        public new Player Holder
        {
            get
            {
                return (Player) base.Holder;
            }

            protected set
            {
                base.Holder = value;
            }
        }

        public void SendCreativeItems()
        {
            Player player = this.Holder;
            InventoryContentPacket pk = new InventoryContentPacket();
            pk.InventoryId = ContainerIds.CREATIVE.GetIndex();
            pk.Items = Item.GetCreativeItems();
            player.SendPacket(pk);
        }

        internal void OpenInventory(Inventory inventory)
        {
            this.opend = inventory;
        }

        internal void CloseInventory()
        {
            this.opend = null;
        }

        public Inventory GetInventory(byte id)
        {
            if (id == this.Type)
            {
                return this;
            }
            else if (id == this.PlayerCursorInventory.Type)
            {
                return this.PlayerCursorInventory;
            }
            else if (id == this.PlayerOffhandInventory.Type)
            {
                return this.PlayerOffhandInventory;
            }
            else if (id == this.ArmorInventory.Type)
            {
                return this.ArmorInventory;
            }
            else if (id == this.OpendInventory.Type)
            {
                return this.OpendInventory;
            }
            else
            {
                return null;
            }
        }

        public PlayerCursorInventory PlayerCursorInventory
        {
            get
            {
                return this.cursor;
            }
        }

        public PlayerEnderChestInventory PlayerEnderChestInventory
        {
            get
            {
                return this.ender;
            }
        }

        public Inventory OpendInventory
        {
            get
            {
                return this.opend;
            }
        }

        public override void SaveNBT()
        {
            base.SaveNBT();

            ListTag inventory = new ListTag("Inventory", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Holder.NamedTag.PutList(inventory);

            this.PlayerCursorInventory.SaveNBT();

            this.PlayerEnderChestInventory.SaveNBT();
        }
    }
}
