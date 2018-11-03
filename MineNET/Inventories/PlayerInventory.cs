using System.Collections.Generic;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;

namespace MineNET.Inventories
{
    public class PlayerInventory : EntityInventory
    {
        public PlayerCursorInventory PlayerCursorInventory { get; }
        public PlayerEnderChestInventory PlayerEnderChestInventory { get; }
        public Inventory OpendInventory { get; private set; } = null;

        public byte OpendWindowId { get; private set; } = 4;

        public CraftingGridInventory CraftingGridInventory { get; }

        public PlayerInventory(Player player) : base(player, 36)
        {
            this.PlayerCursorInventory = new PlayerCursorInventory(player);
            this.PlayerEnderChestInventory = new PlayerEnderChestInventory(player);

            this.CraftingGridInventory = new CraftingGridInventory(player);
        }

        public override void SendSlot(int index, params Player[] players)
        {
            InventorySlotPacket pk = new InventorySlotPacket
            {
                Slot = (uint)index,
                Item = this.GetItem(index),
                InventoryId = this.Type
            };
            Player player = this.Holder;
            player.SendPacket(pk);
        }

        public override void SendContents(params Player[] players)
        {
            InventoryContentPacket pk = new InventoryContentPacket
            {
                Items = new ItemStack[this.Size]
            };
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
            Player player = this.Holder;
            InventoryContentPacket pk = new InventoryContentPacket
            {
                InventoryId = ContainerIds.CREATIVE.GetIndex(),
                Items = Item.GetCreativeItems()
            };
            player.SendPacket(pk);
        }

        internal void OpenInventory(Inventory inventory)
        {
            this.OpendInventory = inventory;
            inventory.Open(this.Holder);
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
            else if (id == this.EntityOffhandInventory.Type)
            {
                return this.EntityOffhandInventory;
            }
            else if (id == this.ArmorInventory.Type)
            {
                return this.ArmorInventory;
            }
            else if (id == this.OpendWindowId)
            {
                return this.OpendInventory;
            }
            else
            {
                return null;
            }
        }

        public override void LoadNBT(CompoundTag nbt)
        {
            base.LoadNBT(nbt);

            this.PlayerCursorInventory.LoadNBT(nbt);
            this.PlayerEnderChestInventory.LoadNBT(nbt);
        }

        public override CompoundTag SaveNBT()
        {
            CompoundTag nbt = base.SaveNBT();

            Dictionary<string, Tag> tags = this.PlayerCursorInventory.SaveNBT().Tags;
            foreach (string key in tags.Keys)
            {
                nbt.PutTag(key, tags[key]);
            }
            tags = this.PlayerEnderChestInventory.SaveNBT().Tags;
            foreach (string key in tags.Keys)
            {
                nbt.PutTag(key, tags[key]);
            }
            return nbt;
        }
    }
}
