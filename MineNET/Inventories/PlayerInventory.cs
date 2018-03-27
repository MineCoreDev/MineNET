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
    public class PlayerInventory : BaseInventory
    {
        private int mainHand = 0;

        private PlayerCursorInventory cursor;
        private PlayerOffhandInventory offhand;
        private ArmorInventory armor;

        private PlayerEnderChestInventory ender;

        private Inventory opend = null;

        public PlayerInventory(Player player) : base(player)
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

            if (!player.NamedTag.Exist("Mainhand"))
            {
                player.NamedTag.PutInt("Mainhand", 0);
            }
            this.mainHand = player.NamedTag.GetInt("Mainhand");

            this.cursor = new PlayerCursorInventory(player);
            this.offhand = new PlayerOffhandInventory(player);
            this.armor = new ArmorInventory(player);

            this.ender = new PlayerEnderChestInventory(player);
        }

        public override int Size
        {
            get
            {
                return 36;
            }
        }

        public override byte Type
        {
            get
            {
                return ContainerIds.INVENTORY.GetIndex();
            }
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

        public void SendMainHand(params Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                MobEquipmentPacket pk = new MobEquipmentPacket();
                pk.EntityRuntimeId = this.Holder.EntityID;
                pk.Item = this.MainHandItem;
                pk.InventorySlot = (byte) this.MainHandSlot;
                pk.WindowId = this.Type;
                players[i].SendPacket(pk);
            }
        }

        public int MainHandSlot
        {
            get
            {
                return this.mainHand;
            }

            set
            {
                this.mainHand = value;
                this.SendMainHand(this.Holder);
            }
        }

        public Item MainHandItem
        {
            get
            {
                return this.GetItem(this.MainHandSlot);
            }

            set
            {
                this.SetItem(this.mainHand, value.Clone());
            }
        }

        public Item OffHandItem
        {
            get
            {
                return this.PlayerOffhandInventory.Item;
            }

            set
            {
                this.PlayerOffhandInventory.Item = value;
            }
        }

        public Item Helmet
        {
            get
            {
                return this.ArmorInventory.Helmet;
            }

            set
            {
                this.ArmorInventory.Helmet = value;
            }
        }

        public Item ChestPlate
        {
            get
            {
                return this.ArmorInventory.ChestPlate;
            }

            set
            {
                this.ArmorInventory.ChestPlate = value;
            }
        }

        public Item Leggings
        {
            get
            {
                return this.ArmorInventory.Leggings;
            }

            set
            {
                this.ArmorInventory.Leggings = value;
            }
        }

        public Item Boots
        {
            get
            {
                return this.ArmorInventory.Boots;
            }

            set
            {
                this.ArmorInventory.Boots = value;
            }
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
            else if (id == this.cursor.Type)
            {
                return this.cursor;
            }
            else if (id == this.offhand.Type)
            {
                return this.offhand;
            }
            else if (id == this.armor.Type)
            {
                return this.armor;
            }
            else if (id == this.opend.Type)
            {
                return this.opend;
            }
            else
            {
                return null;
            }
        }

        public ArmorInventory ArmorInventory
        {
            get
            {
                return this.armor;
            }
        }

        public PlayerCursorInventory PlayerCursorInventory
        {
            get
            {
                return this.cursor;
            }
        }

        public PlayerOffhandInventory PlayerOffhandInventory
        {
            get
            {
                return this.offhand;
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
            ListTag inventory = new ListTag("Inventory", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Holder.NamedTag.PutList(inventory);

            this.Holder.NamedTag.PutInt("MainHand", this.MainHandSlot);

            this.PlayerCursorInventory.SaveNBT();
            this.PlayerOffhandInventory.SaveNBT();
            this.ArmorInventory.SaveNBT();

            this.PlayerEnderChestInventory.SaveNBT();
        }
    }
}
