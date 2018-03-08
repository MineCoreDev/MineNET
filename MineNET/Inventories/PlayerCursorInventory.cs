using MineNET.Data;
using MineNET.Entities.Players;

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
    }
}
