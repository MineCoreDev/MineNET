using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Inventories.Transactions.Action
{
    public class DropItemAction : InventoryAction
    {
        public DropItemAction(Item sourceItem, Item targetItem) : base(sourceItem, targetItem)
        {

        }

        public override bool Execute(Player player)
        {
            return player.DropItem(this.TargetItem);
        }

        public override bool IsValid(Player player)
        {
            return this.SourceItem.ID == BlockIDs.AIR || this.SourceItem.Count <= 0;
        }

        public override void OnExecuteFail(Player player)
        {

        }

        public override void OnExecuteSuccess(Player player)
        {

        }
    }
}
