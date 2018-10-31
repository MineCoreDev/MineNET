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
        public ContainerInventory(InventoryHolder holder, Dictionary<int, ItemStack> items = null) : base(holder, items)
        {

        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);

            ContainerOpenPacket pk = new ContainerOpenPacket();
            pk.WindowId = this.Type;
            pk.Type = this.Type;
            InventoryHolder holder = this.Holder;
            pk.Position = new BlockCoordinate3D((int) holder.X, (int) holder.Y, (int) holder.Z);
            player.SendPacket(pk);
        }

        public override void OnClose(Player player)
        {
            ContainerClosePacket pk = new ContainerClosePacket();
            pk.WindowId = this.Type;
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
                    list.Add(NBTIO.WriteItem(new ItemStack(Item.Get(0), 0, 0)));
                }
                nbt.PutList(list);
            }

            ListTag items = nbt.GetList("items");
            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack item = NBTIO.ReadItem((CompoundTag) items[i]);
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
