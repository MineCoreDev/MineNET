using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Network;
using MineNET.Utils;

namespace MineNET
{
    public sealed class Server
    {
        public static Server instance;

        public ServerConfig config;

        public NetworkManager networkManager;

        private MainLogger logger;

        public Server()
        {
            instance = this;

            var path = $"{MineNETMain.GetPath()}\\ServerConfig.yml";

            this.config = ServerConfig.Load<ServerConfig>(path);

            this.logger = new MainLogger();
            this.logger.Info(Lang.Resources.server_start);

            this.logger.Info(Lang.Resources.server_net_start);

            this.networkManager = new NetworkManager();
        }

        public static Server GetInstance()
        {
            return instance;
        }

        public static ServerConfig GetConfig()
        {
            return GetInstance().config;
        }

        public static MainLogger GetLogger()
        {
            return GetInstance().logger;
        }
    }
}
