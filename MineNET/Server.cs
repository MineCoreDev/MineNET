using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Network;
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

        public static string ExecutePath
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }

        ConsoleInput consoleInput;

        MineNETConfig mineNETConfig;
        public MineNETConfig MineNETConfig
        {
            get
            {
                return mineNETConfig;
            }
        }

        ServerConfig serverConfig;
        public ServerConfig ServerConfig
        {
            get
            {
                return serverConfig;
            }
        }

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
            Logger.Info(LangManager.GetString("server_started"));
            Logger.Info(LangManager.GetString("server_started2"), s.Elapsed.ToString());
        }

        void Init()
        {
            InitConfig();

            UpdateLogger();
            Update();

            consoleInput = new ConsoleInput();

            Logger.Init();
            Logger.Info(LangManager.GetString("server_start"));

            networkManager = new NetworkManager();
            commandManager = new CommandManager();
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
                CommandHandle();
            }
        }

        async void UpdateLogger()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1000 / 20);
                Logger.Update();
            }
        }

        public void Stop()
        {
            Logger.Info(LangManager.GetString("server_stop"));
            mineNETConfig.Save<MineNETConfig>();
            serverConfig.Save<ServerConfig>();
            Kill();
        }

        public void Kill()
        {
            Logger.Info(LangManager.GetString("server_stoped"));
            Killed();
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
