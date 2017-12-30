using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

using Conf = MineNET.Properties;

namespace MineNET.Network
{
    public sealed class NetworkManager
    {
        public RakNetServer raknet;

        public Timer serverNameUpdater;
        public Timer serverHandler;

        public NetworkManager()
        {
            raknet = new RakNetServer(Conf.Server.Default.ServerPort);

            var h = new ServerHandler(raknet, new MineNETServerHandler());

            serverNameUpdater = new Timer((obj) =>
            {
                h.SendOption("name", Encoding.UTF8.GetBytes("MCPE;" + Conf.Server.Default.ServerMotd + ";160;1.2.8;0;20;MineNET;Survival"));
            }, null, 100, 1000);

            serverHandler = new Timer((obj) =>
            {
                h.HandlePacket();
            }, null, 0, 50);
        }
    }
}
