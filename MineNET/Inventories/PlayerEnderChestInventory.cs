using MineNET.Entities.Players;
using MineNET.Inventories.Data;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;

namespace MineNET.Inventories
{
    public class PlayerEnderChestInventory : ContainerInventory
    {
        public PlayerEnderChestInventory(Player player) : base(player)
        {
            if (!player.namedTag.Exist("EnderChestInventory"))
            {
                ListTag newTag = new ListTag("EnderChestInventory", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    newTag.Add(NBTIO.WriteItem(Item.Get(0)));
                }
                player.namedTag.PutList(newTag);
            }

            ListTag items = player.namedTag.GetList("EnderChestInventory");
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
                return 27;
            }
        }

        public override byte Type
        {
            get
            {
                return InventoryType.CONTAINER.GetIndex();
            }
        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);

            //TODO
        }

        public override void OnClose(Player player)
        {
            base.OnClose(player);

            //TODO
        }
    }
}
