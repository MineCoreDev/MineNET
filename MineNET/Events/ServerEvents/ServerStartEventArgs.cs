namespace MineNET.Events.ServerEvents
{
    public class ServerStartEventArgs : ServerEventArgs
    {
        public ServerStartEventArgs()
        {
            this.Server = Server.Instance;
        }
    }
}
