using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class PlayerOffhandInventory : BaseInventory
    {
        public PlayerOffhandInventory(Player player) : base(player)
        {
            if (!player.NamedTag.Exist("Offhand"))
            {
                ListTag newTag = new ListTag("Offhand", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    newTag.Add(NBTIO.WriteItem(Item.Get(0, 0, 0)));
                }
                player.NamedTag.PutList(newTag);
            }

            ListTag items = player.NamedTag.GetList("Offhand");
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }
        }

        public override int Size
        {
            get
            {
                return 1;
            }
        }

        public override byte Type
        {
            get
            {
                return ContainerIds.OFFHAND.GetIndex();
            }
        }

        public Item Item
        {
            get
            {
                return this.GetItem(0);
            }

            set
            {
                this.SetItem(0, value);
            }
        }

        public new Player Holder
        {
            get
            {
                return (Player) base.Holder;
            }

            set
            {
                base.Holder = value;
            }
        }

        public override void SaveNBT()
        {
            ListTag inventory = new ListTag("Offhand", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Holder.NamedTag.PutList(inventory);
        }
    }
}
