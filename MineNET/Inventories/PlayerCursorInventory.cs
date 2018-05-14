using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class PlayerCursorInventory : BaseInventory
    {
        public PlayerCursorInventory(Player player) : base(player)
        {
            /*if (!player.NamedTag.Exist("Cursor"))
            {
                ListTag initItems = new ListTag("Cursor", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    initItems.Add(NBTIO.WriteItem(Item.Get(0, 0, 0), i));
                }
                player.NamedTag.PutList(initItems);
            }

            ListTag items = player.NamedTag.GetList("Cursor");
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }*/
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
                return ContainerIds.CURSOR.GetIndex();
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
            ListTag inventory = new ListTag("Cursor", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Holder.NamedTag.PutList(inventory);
        }
    }
}
