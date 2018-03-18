using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class PlayerOffhandInventory : BaseInventory
    {
        public PlayerOffhandInventory(Player player) : base(player)
        {
            if (!player.namedTag.Exist("Offhand"))
            {
                player.namedTag.PutList(new ListTag<CompoundTag>("Offhand"));
                for (int i = 0; i < this.Size; ++i)
                {
                    player.namedTag.GetList<CompoundTag>("Offhand").Add(NBTIO.WriteItem(Item.Get(0)));
                }
            }
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem(player.namedTag.GetList<CompoundTag>("Offhand")[i]);
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

        public Item GetItem()
        {
            return base.GetItem(0);
        }

        public bool SetItem(Item item)
        {
            return base.SetItem(0, item);
        }
    }
}
