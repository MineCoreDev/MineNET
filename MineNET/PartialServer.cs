using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;
using MineNET.Plugins;
using MineNET.Utils;
using MineNET.Utils.Config;
using MineNET.Worlds;

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
        private Dictionary<long, AdventureSettingsEntry> adventureSettingsEntry = new Dictionary<long, AdventureSettingsEntry>();

        internal Dictionary<string, World> worlds = new Dictionary<string, World>();

        private int tick = 0;

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

            Logger.Info("%server_start");

            this.InitWorld();

            this.Update();

            if (this.mineNETConfig.EnableConsoleInput)
            {
                this.consoleInput = new ConsoleInput();
            }

            this.LoadFiles();

            this.commandManager = new CommandManager();
            this.pluginManager = new PluginManager();
            this.networkManager = new NetworkManager();
        }

        private void InitConfig()
        {
            string serverPath = $"{ExecutePath}\\ServerProperties.yml";
            this.serverConfig = YamlStaticConfig.Load<ServerConfig>(serverPath);

            string minePath = $"{ExecutePath}\\MineNET.yml";
            this.mineNETConfig = YamlStaticConfig.Load<MineNETConfig>(minePath);

            LangManager.Lang = this.mineNETConfig.Language;

            string banPath = $"{ExecutePath}\\banned-players.yml";
            this.BanConfig = YamlConfig.Load(banPath);

            string banIpPath = $"{ExecutePath}\\banned-ips.yml";
            this.BanIpConfig = YamlConfig.Load(banIpPath);

            string opPath = $"{ExecutePath}\\ops.yml";
            this.OpsConfig = YamlConfig.Load(opPath);

            string whitelistPath = $"{ExecutePath}\\whitelist.yml";
            this.WhitelistConfig = YamlConfig.Load(whitelistPath);
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

        private void InitWorld()
        {
            string worldName = this.serverConfig.MainWorldName;
            if (World.Exists(worldName))
            {
                Logger.Info("%server_world_loading", worldName);
                World.LoadWorld(worldName);
            }
            else
            {
                Logger.Info("%server_world_create", worldName);
                World.CreateWorld(worldName);
            }
            Logger.Info("%server_world_loaded", worldName);
        }

        private void UnloadWorld()
        {
            foreach (World w in this.worlds.Values)
            {
                w.Format.WorldData.Save(w);
            }
        }

        private async void LoadFiles()
        {
            await Task.Run(() =>
            {
                Item.LoadCreativeItems();
            });
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
                    players[i].OnUpdate(this.tick);
                }

                if (this.tick % 20 == 0)
                {
                    this.SendPlayersChunk();
                }

                ++this.tick;
            }
        }

        private void SendPlayersChunk()
        {
            Task.Run(() =>
            {
                Player[] sendChunk = this.GetPlayers();
                for (int i = 0; i < sendChunk?.Length; ++i)
                {
                    sendChunk[i].SendChunk();
                }
            });
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

        private async void AddPlayerList(PlayerListEntry entry)
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

        private void AddAdventureSettings(AdventureSettingsEntry entry)
        {
            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].HasSpawned)
                {
                    AdventureSettingsPacket pk = new AdventureSettingsPacket();
                    pk.Entry = entry;
                    players[i].SendPacket(pk);
                }
            }
        }
    }
}
