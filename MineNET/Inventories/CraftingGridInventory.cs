using MineNET.Entities.Players;
using MineNET.Inventories.Data;

namespace MineNET.Inventories
{
    public class CraftingGridInventory : BaseInventory
    {
        public CraftingGridInventory(Player player) : base(player)
        {

        }

        public override int Size
        {
            get
            {
                return 9;
            }
        }

        public override byte Type
        {
            get
            {
                return (byte) InventoryType.WORKBENCH;
            }
        }

        public override string Name
        {
            get
            {
                return "CraftingGrid";
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
