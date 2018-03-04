using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Entities;
using MineNET.Entities.Attributes;
using MineNET.Events.ServerEvents;
using MineNET.Network;
using MineNET.Plugins;
using MineNET.Utils;
using MineNET.Utils.Config;

namespace MineNET
{
    public sealed class Server
    {
        static Server instance;
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

        ConsoleInput consoleInput;

        MineNETConfig mineNETConfig;
        ServerConfig serverConfig;

        NetworkManager networkManager;
        public NetworkManager NetworkManager
        {
            get
            {
                return networkManager;
            }
        }
        CommandManager commandManager;
        public CommandManager CommandManager
        {
            get
            {
                return commandManager;
            }
        }
        PluginManager pluginManager;
        public PluginManager PluginManager
        {
            get
            {
                return pluginManager;
            }
        }

        Logger logger;
        public Logger Logger
        {
            get
            {
                return logger;
            }
        }

        bool isShutdown;
        public bool IsShutdown()
        {
            return isShutdown;
        }

        public void Start()
        {
            instance = this;

            Stopwatch s = new Stopwatch();
            s.Start();

            Init();

            s.Stop();
            Logger.Info("%server_started");
            Logger.Info("%server_started2", s.Elapsed.ToString());

            ServerEvents.OnServerStart(new ServerStartEventArgs());
        }

        void Init()
        {
            InitConfig();

            try
            {
                if (mineNETConfig.EnableConsoleOutput)
                {
                    logger = new Logger();
                    logger.Init();
                    UpdateLogger();
                }

                Update();
            }
            catch (Exception e)
            {
                throw e;
            }


            if (mineNETConfig.EnableConsoleInput)
            {
                consoleInput = new ConsoleInput();
            }

            Logger.Info("%server_start");

            commandManager = new CommandManager();
            pluginManager = new PluginManager();
            new EntityAttributePool();
            networkManager = new NetworkManager();
        }

        void InitConfig()
        {
            string mPath = $"{ExecutePath}\\MineNET.yml";
            string sPath = $"{ExecutePath}\\ServerProperties.yml";
            mineNETConfig = YamlStaticConfig.Load<MineNETConfig>(mPath);
            serverConfig = YamlStaticConfig.Load<ServerConfig>(sPath);
        }

        async void Update()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1000 / 20);
                if (mineNETConfig.EnableConsoleInput)
                {

                    CommandHandle();
                }
            }
        }

        async void UpdateLogger()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1000 / 20);
                logger.Update();
            }
        }

        public void Stop()
        {
            Logger.Info("%server_stop");
            mineNETConfig.Save<MineNETConfig>();
            serverConfig.Save<ServerConfig>();

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

            mineNETConfig?.Save<MineNETConfig>();
            serverConfig?.Save<ServerConfig>();

            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Close("disconnect.closed");//TODO: Option Add
            }

            ServerEvents.OnServerStop(new ServerStopEventArgs());

            Kill();
        }

        public void Kill()
        {
            Logger.Info("%server_stoped");
            Killed();
        }

        public Player[] GetPlayers()
        {
            return networkManager?.players.Values.ToArray();
        }

        async void Killed()
        {
            await Task.Delay(1000);
            isShutdown = true;
        }

        void CommandHandle()
        {
            string cmd = consoleInput.GetCommand();
            if (!string.IsNullOrEmpty(cmd))
            {
                commandManager.HandleConsoleCommand(cmd);
            }
        }
    }
}
