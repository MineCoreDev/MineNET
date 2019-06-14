using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;

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

        public override string Name
        {
            get
            {
                return "Cursor";
            }
        }

        public Item Curosr
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
    }
}
