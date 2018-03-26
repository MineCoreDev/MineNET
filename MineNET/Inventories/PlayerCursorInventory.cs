using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.Packets.Data;

namespace MineNET.Inventories
{
    public class PlayerCursorInventory : BaseInventory
    {
        public PlayerCursorInventory(Player player) : base(player)
        {

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
    }
}
