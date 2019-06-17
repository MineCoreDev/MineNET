namespace MineNET
{
    public class Server
    {
        public static Server Instance { get; private set; }

        public IServerManager ServerManager { get; private set; }

        public Server() : this(new ServerManager())
        {
            Instance = this;
        }

        public Server(IServerManager serverManager)
        {
            ServerManager = serverManager;
        }
    }
}