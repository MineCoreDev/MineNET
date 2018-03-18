using MineNET.Entities.Players;
using MineNET.Inventories.Data;
using MineNET.Items;
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
                player.namedTag.PutList(new ListTag<CompoundTag>("EnderChestInventory"));
                for (int i = 0; i < this.Size; ++i)
                {
                    player.namedTag.GetList<CompoundTag>("EnderChestInventory").Add(NBTIO.WriteItem(Item.Get(0)));
                }
            }
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem(player.namedTag.GetList<CompoundTag>("EnderChestInventory")[i]);
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
