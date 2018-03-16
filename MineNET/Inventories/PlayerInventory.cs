using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
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

        private Inventory other;

        public PlayerInventory(Player player) : base(player)
        {
            if (!player.namedTag.Exist("Inventory"))
            {
                player.namedTag.PutList(new ListTag<CompoundTag>("Inventory"));
                for (int i = 0; i < this.Size; ++i)
                {
                    player.namedTag.GetList<CompoundTag>("Inventory").Add(NBTIO.WriteItem(Item.Get(0), i));
                }
            }
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem(player.namedTag.GetList<CompoundTag>("Inventory")[i]);
                this.SetItem(i, item, false);
            }

            if (!player.namedTag.Exist("Mainhand"))
            {
                player.namedTag.PutInt("Mainhand", 0);
            }
            this.mainHand = player.namedTag.GetInt("Mainhand");

            this.cursor = new PlayerCursorInventory(player);
            this.offhand = new PlayerOffhandInventory(player);
            this.armor = new ArmorInventory(player);
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
                return (Player) this.holder;
            }

            protected set
            {
                this.holder = value;
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
                pk.Item = this.GetItemMainHand();
                pk.InventorySlot = (byte) this.GetMainHandSlot();
                pk.WindowId = this.Type;
                players[i].SendPacket(pk);
            }
        }

        public int GetMainHandSlot()
        {
            return this.mainHand;
        }

        public Item GetItemMainHand()
        {
            return this.GetItem(this.GetMainHandSlot());
        }

        public bool SetItemMainHand(Item item)
        {
            return this.SetItem(this.mainHand, item.Clone());
        }

        public Item GetItemOffHand()
        {
            return this.offhand.GetItem();
        }

        public bool SetItemOffHand(Item item)
        {
            return this.offhand.SetItem(item);
        }

        public Item GetHelmet()
        {
            return this.armor.GetHelmet();
        }

        public bool SetHelmet(Item item)
        {
            return this.armor.SetHelmet(item);
        }

        public Item GetChestPlate()
        {
            return this.armor.GetChestPlate();
        }

        public bool SetChestPlate(Item item)
        {
            return this.armor.SetChestPlate(item);
        }

        public Item GetLeggings()
        {
            return this.armor.GetLeggings();
        }

        public bool SetLeggings(Item item)
        {
            return this.armor.SetLeggings(item);
        }

        public Item GetBoots()
        {
            return this.armor.GetBoots();
        }

        public bool SetBoots(Item item)
        {
            return this.armor.SetBoots(item);
        }

        public void OpenInventory(Inventory inventory)
        {
            this.other = inventory;
        }

        public void CloseInventory()
        {
            this.other = null;
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
            else if (id == this.other.Type)
            {
                return this.other;
            }
            else
            {
                return null;
            }
        }

        public PlayerCursorInventory GetPlayerCursorInventory()
        {
            return this.cursor;
        }

        public PlayerOffhandInventory GetPlayerOffhandInventory()
        {
            return this.offhand;
        }

        public ArmorInventory GetArmorInventory()
        {
            return this.armor;
        }
    }
}
