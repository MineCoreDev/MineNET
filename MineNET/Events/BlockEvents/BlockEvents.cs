namespace MineNET.Events.BlockEvents
{
    public class BlockEvents : MineNETEvents
    {
        public static event EventHandler<BlockPlaceEventArgs> BlockPlace;
        public static void OnBlockPlace(BlockPlaceEventArgs args)
        {
            BlockPlace?.Invoke(args);
        }
    }
}
