using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MineCraftPENetwork.Server;

using MineNET.Utils;

namespace MineNET
{
    public sealed class MineNETMain
    {
        private static MineNETMain instance;

        private MineNETConfig config;
        private Server server;

        public MineNETMain()
        {
            instance = this;

            var path = $"{GetPath()}\\MineNETConfig.yml";

            this.server = new Server();

            this.config = MineNETConfig.Load<MineNETConfig>(path);
            if (this.config.UseAutoUpdate.ToBool())
            {
                var url = this.config.VersionFileURL;
            }
        }

        public Server GetServer()
        {
            return this.server;
        }

        public bool IsShutdown()
        {
            return this.server.networkManager.raknet.IsShutdown();
        }

        public static string GetPath()
        {
            return Environment.CurrentDirectory;
        }

        public static MineNETMain GetInstance()
        {
            return instance;
        }

        public static MineNETConfig GetConfig()
        {
            return GetInstance().config;
        }
    }
}
