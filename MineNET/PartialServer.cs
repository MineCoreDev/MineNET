using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Entities.Attributes;
using MineNET.Entities.Players;
using MineNET.Network;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;
using MineNET.Plugins;
using MineNET.Utils;
using MineNET.Utils.Config;

namespace MineNET
{
    public sealed partial class Server
    {
        private static Server instance;

        private ConsoleInput consoleInput;

        private MineNETConfig mineNETConfig;
        private ServerConfig serverConfig;

        private NetworkManager networkManager;
        private CommandManager commandManager;
        private PluginManager pluginManager;

        private Dictionary<long, PlayerListEntry> playerListEntries = new Dictionary<long, PlayerListEntry>();

        private Logger logger;

        private bool isShutdown;

        private void Init()
        {
            this.InitConfig();
            this.InitFolder();

            if (this.mineNETConfig.EnableConsoleOutput)
            {
                this.logger = new Logger();
                this.logger.Init();
                this.UpdateLogger();
            }

            this.Update();


            if (this.mineNETConfig.EnableConsoleInput)
            {
                this.consoleInput = new ConsoleInput();
            }

            Logger.Info("%server_start");

            this.commandManager = new CommandManager();
            this.pluginManager = new PluginManager();
            new EntityAttributePool();
            this.networkManager = new NetworkManager();
        }

        private void InitConfig()
        {
            string mPath = $"{ExecutePath}\\MineNET.yml";
            string sPath = $"{ExecutePath}\\ServerProperties.yml";
            this.mineNETConfig = YamlStaticConfig.Load<MineNETConfig>(mPath);
            this.serverConfig = YamlStaticConfig.Load<ServerConfig>(sPath);
        }

        private void InitFolder()
        {
            string wPath = $"{Server.ExecutePath}\\worlds";
            if (!Directory.Exists(wPath))
            {
                Directory.CreateDirectory(wPath);
            }
            string pPath = $"{Server.ExecutePath}\\players";
            if (!Directory.Exists(pPath))
            {
                Directory.CreateDirectory(pPath);
            }
        }

        private async void Update()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1000 / 20);
                if (this.mineNETConfig.EnableConsoleInput)
                {
                    this.CommandHandle();
                }

                Player[] players = this.GetPlayers();
                for (int i = 0; i < players?.Length; ++i)
                {
                    players[i].OnUpdate();
                }
            }
        }

        private async void UpdateLogger()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1000 / 20);
                this.logger.Update();
            }
        }

        private void CommandHandle()
        {
            string cmd = this.consoleInput.GetCommand();
            if (!string.IsNullOrEmpty(cmd))
            {
                this.commandManager.HandleConsoleCommand(cmd);
            }
        }

        private void Kill()
        {
            Logger.Info("%server_stoped");
            this.Killed();
        }

        private async void Killed()
        {
            await Task.Delay(1000);
            this.isShutdown = true;
        }

        private async void AddPlayerList(Player player, PlayerListEntry entry)
        {
            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].HasSpawned)
                {
                    PlayerListPacket playerListPacket = new PlayerListPacket();
                    playerListPacket.Type = PlayerListPacket.TYPE_ADD;
                    playerListPacket.Entries = new PlayerListEntry[] { entry };
                    await Task.Run(() =>
                    {
                        players[i].SendPacket(playerListPacket);
                    });
                }
            }
        }

        private void RemovePlayerList(PlayerListEntry entry)
        {
            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                PlayerListPacket playerListPacket = new PlayerListPacket();
                playerListPacket.Type = PlayerListPacket.TYPE_REMOVE;
                playerListPacket.Entries = new PlayerListEntry[] { entry };
                players[i].SendPacket(playerListPacket);
            }
        }
    }
}
