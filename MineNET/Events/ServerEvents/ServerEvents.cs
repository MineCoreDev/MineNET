namespace MineNET.Events.ServerEvents
{
    public class ServerEvents
    {
        public delegate void OnServerStartEventHandler(ServerStartEventArgs args);
        public static event OnServerStartEventHandler ServerStart;
        public static void OnServerStart(ServerStartEventArgs args)
        {
            ServerStart?.Invoke(args);
        }

        public delegate void OnServerStopEventHandler(ServerStopEventArgs args);
        public static event OnServerStopEventHandler ServerStop;
        public static void OnServerStop(ServerStopEventArgs args)
        {
            ServerStop?.Invoke(args);
        }
    }
}
