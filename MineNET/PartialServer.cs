using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MineNET.Blocks;
using MineNET.Commands;
using MineNET.Entities.Players;
using MineNET.Inventories.Recipe;
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
        #region Field
        private MineNETConfig mineNETConfig;
        private ServerConfig serverConfig;

        private Dictionary<long, Player> playerList = new Dictionary<long, Player>();

        internal Dictionary<string, World> worlds = new Dictionary<string, World>();

        private int tick = 0;

        private bool isShutdown;
        #endregion

        #region Init Method
        private void Init()
        {
            this.InitConfig();
            this.InitFolder();

            if (this.mineNETConfig.EnableConsoleOutput)
            {
                this.Logger = new Logger();
                this.Logger.Init();
                this.UpdateLogger();
            }

            Logger.Info("%server_start");

            this.InitWorld();

            if (this.mineNETConfig.EnableConsoleInput)
            {
                this.ConsoleInput = new ConsoleInput();
            }

            this.Update();
            this.LoadFiles();

            this.CommandManager = new CommandManager();
            this.PluginManager = new PluginManager();
            this.NetworkManager = new NetworkManager();
            this.CraftingManager = new CraftingManager();

            this.PluginManager.EnablePlugins();
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
        #endregion

        #region World Unload Method
        private void UnloadWorld()
        {
            foreach (World w in this.worlds.Values)
            {
                w.Format.WorldData.Save(w);
            }
        }
        #endregion

        #region AsyncLoadFile
        private async void LoadFiles()
        {
            await Task.Run(() =>
            {
                Item.LoadCreativeItems();
                Block.LoadRuntimeIds();
            });
        }
        #endregion

        #region Update Method
        private async void Update()
        {
            while (!IsShutdown())
            {
                ClockConstantController.Start("server");
                if (this.mineNETConfig.EnableConsoleInput)
                {
                    this.CommandHandle();
                }

                Player[] players = this.GetPlayers();
                for (int i = 0; i < players?.Length; ++i)
                {
                    players[i].OnUpdate(this.tick);
                }

                World[] worlds = this.worlds.Values.ToArray();
                for (int i = 0; i < worlds.Length; ++i)
                {
                    worlds[i].OnUpdate(this.tick);
                }

                if (this.tick % 20 == 0)
                {
                    this.SendPlayersChunk();
                }
                await ClockConstantController.Stop("server");

                ++this.tick;
            }
        }

        private async void UpdateLogger()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1);
                this.Logger.Update();
            }
        }
        #endregion

        #region Send Chunk Method
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
        #endregion

        #region Handle Method
        private void CommandHandle()
        {
            string cmd = this.ConsoleInput.GetCommand();
            if (!string.IsNullOrEmpty(cmd))
            {
                this.CommandManager.HandleConsoleCommand(cmd);
            }
        }
        #endregion

        #region Task Kill Method
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
        #endregion

        #region Add / Remove Method
        private async void AddPlayerList(Player player)
        {
            this.playerList[player.EntityID] = player;
            Player[] players = this.playerList.Values.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                PlayerListPacket playerListPacket = new PlayerListPacket();
                playerListPacket.Type = PlayerListPacket.TYPE_ADD;
                playerListPacket.Entries = new PlayerListEntry[] { player.PlayerListEntry };
                await Task.Run(() =>
                {
                    players[i].SendPacket(playerListPacket);
                });
            }
        }

        private void RemovePlayerList(long eid)
        {
            PlayerListEntry entry = this.playerList[eid].PlayerListEntry;
            Player[] players = this.playerList.Values.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                PlayerListPacket playerListPacket = new PlayerListPacket();
                playerListPacket.Type = PlayerListPacket.TYPE_REMOVE;
                playerListPacket.Entries = new PlayerListEntry[] { entry };
                players[i].SendPacket(playerListPacket);
            }
        }

        private void AddAdventureSettings(Player player)
        {
            Player[] players = this.playerList.Values.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                player.AdventureSettingsEntry.Update(players[i]);
            }
        }
        #endregion
    }
}
