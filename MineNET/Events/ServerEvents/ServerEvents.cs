namespace MineNET.Events.ServerEvents
{
    public class ServerEvents : MineNETEvents
    {
        public static event EventHandler<DataPacketReceiveArgs> DataPacketReceive;
        public static void OnPacketReceive(DataPacketReceiveArgs args)
        {
            DataPacketReceive?.Invoke(args);
        }

        public static event EventHandler<DataPacketSendArgs> DataPacketSend;
        public static void OnPacketSend(DataPacketSendArgs args)
        {
            DataPacketSend?.Invoke(args);
        }

        public static event EventHandler<ServerCommandEventArgs> ServerCommand;
        public static void OnServerCommand(ServerCommandEventArgs args)
        {
            ServerCommand?.Invoke(args);
        }

        public static event EventHandler<ServerStartEventArgs> ServerStart;
        public static void OnServerStart(ServerStartEventArgs args)
        {
            ServerStart?.Invoke(args);
        }

        public static event EventHandler<ServerStopEventArgs> ServerStop;
        public static void OnServerStop(ServerStopEventArgs args)
        {
            ServerStop?.Invoke(args);
        }
    }
}
