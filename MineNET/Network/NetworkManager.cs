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

        public Timer serverNameUpdater;
        public Timer serverHandler;

        public NetworkManager()
        {
            this.raknet = new RakNetServer(Server.GetConfig().ServerPort);

            Server.GetLogger().Info(Lang.Resources.server_net_started);

            var h = new ServerHandler(this.raknet, new MineNETServerHandler());

            this.serverNameUpdater = new Timer((obj) =>
            {
                h.SendOption("name", Encoding.UTF8.GetBytes("MCPE;" + Server.GetConfig().ServerMotd + ";160;1.2.8;0;20;MineNET;Survival"));
            }, null, 100, 1000);

            this.serverHandler = new Timer((obj) =>
            {
                h.HandlePacket();
            }, null, 0, 50);
        }
    }
}
