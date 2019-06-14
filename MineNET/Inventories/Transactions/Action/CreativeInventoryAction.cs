using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Inventories.Transactions.Action
{
    public class CreativeInventoryAction : InventoryAction
    {
        public int ActionType { get; protected set; }

        public CreativeInventoryAction(Item sourceItem, Item targetItem, int actionType) : base(sourceItem, targetItem)
        {
            this.ActionType = actionType;
        }

        public override bool IsValid(Player player)
        {
            return player.IsCreative;
        }

        public override bool Execute(Player player)
        {
            return true;
        }

        public override void OnExecuteSuccess(Player player)
        {

        }

        public override void OnExecuteFail(Player player)
        {

        }
    }
}
