namespace MineNET.Events.BlockEvents
{
    public class BlockEvents : MineNETEvents
    {
        public static event EventHandler<BlockBreakEventArgs> BlockBreak;
        public static void OnBlockBreak(BlockBreakEventArgs args)
        {
            BlockBreak?.Invoke(args);
        }

        public static event EventHandler<BlockPlaceEventArgs> BlockPlace;
        public static void OnBlockPlace(BlockPlaceEventArgs args)
        {
            BlockPlace?.Invoke(args);
        }
    }
}
