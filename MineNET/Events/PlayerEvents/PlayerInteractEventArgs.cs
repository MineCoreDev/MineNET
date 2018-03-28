﻿using MineNET.Blocks;
using MineNET.Blocks.Data;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerInteractEventArgs : PlayerEventArgs, ICancellable
    {
        public Item Item { get; set; }
        public Block Target { get; set; }
        public BlockFace BlockFace { get; set; }

        public bool IsCancel { get; set; }

        public PlayerInteractEventArgs(Player player, Item item, Block target, BlockFace face) : base(player)
        {
            this.Item = item;
            this.Target = target;
            this.BlockFace = face;
        }
    }
}
