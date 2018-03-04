namespace MineNET.Events.ServerEvents
{
    public class ServerEvents : MineNETEvents
    {
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

        public static event EventHandler<DataPacketSendArgs> PacketSend;
        public static void OnPacketSend(DataPacketSendArgs args)
        {
            PacketSend?.Invoke(args);
        }

        public static event EventHandler<DataPacketReceiveArgs> PacketReceive;
        public static void OnPacketReceive(DataPacketReceiveArgs args)
        {
            PacketReceive?.Invoke(args);
        }
    }
}
