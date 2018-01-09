using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

namespace MineNET.Network
{
    public sealed class NetworkManager
    {
        public RakNetServer raknet;
        public MineNETServerHandler mineNetServerHandler;
        public ServerHandler serverHandler;

        public Timer serverNameUpdater;
        public Timer serverHandlerUpdater;

        public NetworkManager()
        {
            this.raknet = new RakNetServer(Server.GetConfig().ServerPort);

            Server.GetLogger().Info(Lang.Resources.server_net_started, Server.GetConfig().ServerPort);

            mineNetServerHandler = new MineNETServerHandler();
            serverHandler = new ServerHandler(this.raknet, this.mineNetServerHandler);

            this.serverNameUpdater = new Timer((obj) =>
            {
                serverHandler.SendOption("name", Encoding.UTF8.GetBytes("MCPE;" + Server.GetConfig().ServerMotd + ";160;1.2.8;0;20;MineNET;Survival"));
            }, null, 100, 1000);

            this.serverHandlerUpdater = new Timer((obj) =>
            {
                serverHandler.HandlePacket();
            }, null, 0, 50);

            Server.GetLogger().Info(Lang.Resources.server_net_packetHandlerStart);
        }
    }
}
