using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Inventories
{
    public class PlayerInventory : EntityInventory
    {
        public PlayerCursorInventory PlayerCursorInventory { get; }
        public PlayerEnderChestInventory PlayerEnderChestInventory { get; }
        public Inventory OpendInventory { get; private set; } = null;


        public CraftingGridInventory CraftingGridInventory { get; }

        public PlayerInventory(Player player) : base(player, 36)
        {
            if (!player.NamedTag.Exist("Inventory"))
            {
                ListTag initItems = new ListTag("Inventory", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    initItems.Add(NBTIO.WriteItem(new ItemStack(Item.Get(0), 0, 0), i));
                }
                player.NamedTag.PutList(initItems);
            }

            ListTag items = player.NamedTag.GetList("Inventory");
            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }

            this.PlayerCursorInventory = new PlayerCursorInventory(player);
            this.PlayerEnderChestInventory = new PlayerEnderChestInventory(player);

            this.CraftingGridInventory = new CraftingGridInventory(player);
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
            pk.Items = new ItemStack[this.Size];
            for (int i = 0; i < this.Size; ++i)
            {
                pk.Items[i] = this.GetItem(i);
            }
            pk.InventoryId = this.Type;
            Player player = this.Holder;
            player.SendPacket(pk);
        }

        public override void OnSlotChange(int index, ItemStack item, bool send)
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
            /*Player player = this.Holder;
            InventoryContentPacket pk = new InventoryContentPacket();
            pk.InventoryId = ContainerIds.CREATIVE.GetIndex();
            pk.Items = Item.GetCreativeItems();
            player.SendPacket(pk);*/
        }

        internal void OpenInventory(Inventory inventory)
        {
            this.OpendInventory = inventory;
        }

        internal void CloseInventory()
        {
            this.OpendInventory = null;
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
