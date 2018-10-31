using System.Diagnostics;
using System.IO;
using System.Net;
using MineNET.Commands;
using MineNET.Events;
using MineNET.Events.ServerEvents;
using MineNET.Init;
using MineNET.Manager;
using MineNET.Network;
using MineNET.Plugins;
using MineNET.Utils.Config;
using MineNET.Worlds;

namespace MineNET
{
    public partial class Server
    {
        public void Init(Stopwatch sw)
        {
            this.LoadConfigs();
            this.LoadFiles();
            this.OnServerStart();

            this.LoadWorlds(sw);

            this.StartNetwork(sw);
        }

        private void InitRegistries()
        {
            MineNET_Registries.Init();
            new BlockInit();
            new BlockEntityInit();
            new ItemInit();
            new EffectInit();
            new EntityInit();
        }

        private void OnServerStart()
        {
            this.Clock = new ConstantClockManager();

            GlobalBlockPalette.Init();

            this.InitRegistries();

            this.Event = new EventManager();
            IO.Logger.Info("%server.start");
            this.Plugin = new PluginManager();
            this.Command = new CommandManager();

            this.Event.Server.OnServerStart(this, new ServerStartEventArgs());
        }

        private void StartNetwork(Stopwatch sw)
        {
            this.ServerList = new ServerListInfo();

            if (this.NetworkSocket == null)
            {
                int port = this.ServerProperty.ServerPort;
                IO.Logger.Info("%server.network.start", port);

                this.EndPoint = new IPEndPoint(IPAddress.Any, port);
                INetworkSocket socket = new UDPSocket();
                socket.Init(this.EndPoint);
                this.SetNetworkSocket(socket);
            }

            this.Network = new NetworkManager();
            IO.Logger.Info("%server.network.start.done", sw.Elapsed.ToString(@"mm\:ss\.fff"));
        }

        private void LoadWorlds(Stopwatch sw)
        {
            string worldFolder = ExecutePath + "\\worlds";
            if (Directory.Exists(worldFolder))
            {
                Directory.CreateDirectory(worldFolder);
            }

            string mainWorld = this.ServerProperty.MainWorldName;
            if (!World.Exists(mainWorld))
            {
                World.CreateWorld(mainWorld);
                IO.Logger.Info("%server.world.create", mainWorld);
            }
            else
            {
                World.LoadWorld(mainWorld);
                IO.Logger.Info("%server.world.load", mainWorld);
            }

            string[] subWorlds = this.ServerProperty.LoadWorldNames;
            for (int i = 0; i < subWorlds.Length; ++i)
            {
                if (!World.Exists(subWorlds[i]))
                {
                    World.CreateWorld(subWorlds[i]);
                    IO.Logger.Info("%server.world.create", subWorlds[i]);
                }
                else
                {
                    World.LoadWorld(subWorlds[i]);
                    IO.Logger.Info("%server.world.load", subWorlds[i]);
                }
            }
        }

        private void LoadConfigs()
        {
            this.Config = YamlStaticConfig.Load<MineNETConfig>($"{ExecutePath}\\MineNET.yml");
            this.ServerProperty = YamlStaticConfig.Load<ServerConfig>($"{ExecutePath}\\ServerProperties.yml");
        }

        private void LoadFiles()
        {
            if (!Directory.Exists(PlayerDataPath))
            {
                Directory.CreateDirectory(PlayerDataPath);
            }

            //TODO OP File & WhiteList File & BanList File...
        }
    }
}