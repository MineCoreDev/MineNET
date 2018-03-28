using System;
using MineNET.Blocks;

namespace MineNET.Events.BlockEvents
{
    public abstract class BlockEventArgs : EventArgs
    {
        public Block Block { get; set; }

        public BlockEventArgs(Block block)
        {
            this.Block = block;
        }
    }
}
