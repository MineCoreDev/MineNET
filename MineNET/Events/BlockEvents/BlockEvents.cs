using System;

namespace MineNET.Events.BlockEvents
{
    public class BlockEvents
    {
        public event EventHandler<BlockBreakEventArgs> BlockBreak;
        public void OnBlockBreak(object sender, BlockBreakEventArgs args)
        {
            this.BlockBreak?.Invoke(sender, args);
        }

        public event EventHandler<BlockPlaceEventArgs> BlockPlace;
        public void OnBlockPlace(object sender, BlockPlaceEventArgs args)
        {
            this.BlockPlace?.Invoke(sender, args);
        }
    }
}
