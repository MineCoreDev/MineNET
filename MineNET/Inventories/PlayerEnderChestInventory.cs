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
        public Player Player { get; }

        public PlayerEnderChestInventory(Player player) : base(null)
        {
            this.Player = player;

            if (!player.NamedTag.Exist("EnderChestInventory"))
            {
                ListTag newTag = new ListTag("EnderChestInventory", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    newTag.Add(NBTIO.WriteItem(new ItemStack(Item.Get(0), 0, 0)));
                }
                player.NamedTag.PutList(newTag);
            }

            ListTag items = player.NamedTag.GetList("EnderChestInventory");
            for (int i = 0; i < this.Size; ++i)
            {
                ItemStack item = NBTIO.ReadItem((CompoundTag) items[i]);
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

            //TODO:
        }

        public override void OnClose(Player player)
        {
            base.OnClose(player);

            //TODO:
        }

        public void SetHolder(InventoryHolder holder)
        {
            this.Holder = holder;
        }

        public override void SaveNBT()
        {
            ListTag inventory = new ListTag("EnderChestInventory", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Player.NamedTag.PutList(inventory);
        }
    }
}
