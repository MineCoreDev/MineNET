using MineNET.Blocks;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerInteractEventArgs : PlayerEventArgs, ICancelable
    {
        public ItemStack Item { get; }
        public Block Target { get; }
        public BlockFace BlockFace { get; }

        public bool IsCancel { get; set; }

        public PlayerInteractEventArgs(Player player, ItemStack item, Block target, BlockFace face) : base(player)
        {
            this.Item = item;
            this.Target = target;
            this.BlockFace = face;
        }
    }
}
