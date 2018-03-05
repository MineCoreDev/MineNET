using System;
using System.Diagnostics;
using System.Linq;
using MineNET.Commands;
using MineNET.Entities;
using MineNET.Events.ServerEvents;
using MineNET.Network;
using MineNET.Plugins;
using MineNET.Utils;

namespace MineNET
{
    public sealed partial class Server
    {
        public static Server Instance
        {
            get
            {
                return instance;
            }
        }

        public static MineNETConfig MineNETConfig
        {
            get
            {
                return Instance.mineNETConfig;
            }
        }

        public static ServerConfig ServerConfig
        {
            get
            {
                return Instance.serverConfig;
            }
        }

        public static string ExecutePath
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }

        public NetworkManager NetworkManager
        {
            get
            {
                return this.networkManager;
            }
        }

        public CommandManager CommandManager
        {
            get
            {
                return this.commandManager;
            }
        }

        public PluginManager PluginManager
        {
            get
            {
                return this.pluginManager;
            }
        }

        public Logger Logger
        {
            get
            {
                return this.logger;
            }
        }

        public bool IsShutdown()
        {
            return this.isShutdown;
        }

        public void Start(bool gui = false)
        {
            instance = this;

            Stopwatch s = new Stopwatch();
            s.Start();

            Init(gui);

            s.Stop();
            Logger.Info("%server_started");
            Logger.Info("%server_started2", s.Elapsed.ToString());

            ServerEvents.OnServerStart(new ServerStartEventArgs());
        }

        public void Stop()
        {
            Logger.Info("%server_stop");
            this.mineNETConfig.Save<MineNETConfig>();
            this.serverConfig.Save<ServerConfig>();

            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Close("disconnect.closed");//TODO: Option Add
            }

            ServerEvents.OnServerStop(new ServerStopEventArgs());

            Kill();
        }

        public void ErrorStop(Exception e)
        {
            this.logger = new Logger();
            Logger.Fatal(e.ToString());
            Logger.Error("%server_stop_error");
            Logger.Info("%server_stop");

            this.mineNETConfig?.Save<MineNETConfig>();
            this.serverConfig?.Save<ServerConfig>();

            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Close("disconnect.closed");//TODO: Option Add
            }

            ServerEvents.OnServerStop(new ServerStopEventArgs());

            Kill();
        }

        public Player[] GetPlayers()
        {
            return this.networkManager?.players.Values.ToArray();
        }

        public Player GetPlayer(string name)
        {
            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].Name == name)
                    return players[i];
            }

            return null;
        }
    }
}
