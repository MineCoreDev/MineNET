using System;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Entities;
using MineNET.Entities.Attributes;
using MineNET.Network;
using MineNET.Plugins;
using MineNET.Utils;
using MineNET.Utils.Config;

namespace MineNET
{
    public sealed partial class Server
    {
        static Server instance;

        ConsoleInput consoleInput;

        MineNETConfig mineNETConfig;
        ServerConfig serverConfig;

        NetworkManager networkManager;
        CommandManager commandManager;
        PluginManager pluginManager;

        Logger logger;

        bool isShutdown;

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

                Player[] players = this.GetPlayers();
                for (int i = 0; i < players?.Length; ++i)
                {
                    if (players[i].HasSpawned)
                    {
                        players[i].OnUpdate();
                    }
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

        void CommandHandle()
        {
            string cmd = consoleInput.GetCommand();
            if (!string.IsNullOrEmpty(cmd))
            {
                commandManager.HandleConsoleCommand(cmd);
            }
        }

        void Kill()
        {
            Logger.Info("%server_stoped");
            Killed();
        }

        async void Killed()
        {
            await Task.Delay(1000);
            isShutdown = true;
        }
    }
}
