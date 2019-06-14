using System.Collections.Generic;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Inventories
{
    public abstract class ContainerInventory : BaseInventory
    {
        public ContainerInventory(InventoryHolder holder, Dictionary<int, Item> items = null) : base(holder, items)
        {

        }

        public override void SendSlot(int index, params Player[] players)
        {
            InventorySlotPacket pk = new InventorySlotPacket
            {
                Slot = (uint)index,
                Item = this.GetItem(index)
            };
            for (int i = 0; i < players.Length; ++i)
            {
                Player player = players[i];
                pk.InventoryId = player.Inventory.OpendWindowId;
                player.SendPacket(pk);
            }
        }

        public override void SendContents(params Player[] players)
        {
            InventoryContentPacket pk = new InventoryContentPacket
            {
                Items = new Item[this.Size]
            };
            for (int i = 0; i < this.Size; ++i)
            {
                pk.Items[i] = this.GetItem(i);
            }
            for (int i = 0; i < players.Length; ++i)
            {
                Player player = players[i];
                pk.InventoryId = player.Inventory.OpendWindowId;
                player.SendPacket(pk);
            }
        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);

            ContainerOpenPacket pk = new ContainerOpenPacket
            {
                WindowId = player.Inventory.OpendWindowId,
                Type = this.Type
            };
            InventoryHolder holder = this.Holder;
            pk.Position = new BlockCoordinate3D((int) holder.X, (int) holder.Y, (int) holder.Z);
            player.SendPacket(pk);

            this.SendContents(player);
        }

        public override void OnClose(Player player)
        {
            ContainerClosePacket pk = new ContainerClosePacket
            {
                WindowId = player.Inventory.OpendWindowId
            };
            player.SendPacket(pk);

            base.OnClose(player);
        }

        public override void LoadNBT(CompoundTag nbt)
        {
            if (!nbt.Exist("items"))
            {
                ListTag list = new ListTag("items", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    list.Add(NBTIO.WriteItem(Item.Get(0, 0, 0)));
                }
                nbt.PutList(list);
            }

            ListTag items = nbt.GetList("items");
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }
        }

        public override CompoundTag SaveNBT()
        {
            CompoundTag nbt = new CompoundTag();
            ListTag list = new ListTag("items", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                list.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            nbt.PutList(list);
            return nbt;
        }
    }
}
