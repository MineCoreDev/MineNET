using MineNET.Inventories;

namespace MineNET.Entities.Players
{
    public partial class Player
    {
        private PlayerInventory inventory;

        public Player()
        {
            this.inventory = new PlayerInventory(this);

            this.ShowNameTag = true;
            this.AlwaysShowNameTag = true;

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_CAN_CLIMB);
        }
    }
}
