namespace MineNET.Events.ServerEvents
{
    public class ServerStopEventArgs : ServerEventArgs
    {
        public ServerStopEventArgs()
        {
            this.Server = Server.Instance;
        }
    }
}
