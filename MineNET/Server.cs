using System;
using System.Diagnostics;
using System.Linq;
using MineNET.Commands;
using MineNET.Entities.Players;
using MineNET.Events.ServerEvents;
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
        public static Server Instance
        {
            get
            {
                return Server.instance;
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

        public YamlConfig BanConfig { get; private set; }
        public YamlConfig BanIpConfig { get; private set; }
        public YamlConfig OpsConfig { get; private set; }
        public YamlConfig WhitelistConfig { get; private set; }

        public NetworkManager NetworkManager { get; private set; }

        public CommandManager CommandManager { get; private set; }

        public PluginManager PluginManager { get; private set; }

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

        public void Start()
        {
            instance = this;

            Stopwatch s = new Stopwatch();
            s.Start();

            this.Init();

            s.Stop();
            Logger.Info("%server_started");
            Logger.Info("%server_started2", s.Elapsed.ToString());

            ServerEvents.OnServerStart(new ServerStartEventArgs());
        }

        public void Stop(string reason = "")
        {
            Logger.Info("%server_stop");
            this.mineNETConfig.Save<MineNETConfig>();
            this.serverConfig.Save<ServerConfig>();

            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (string.IsNullOrEmpty(reason))
                {
                    players[i].Close(this.mineNETConfig.ServerStopText);
                }
                else
                {
                    players[i].Close(reason);
                }
            }

            this.UnloadWorld();

            this.NetworkManager.Server.UDPClientClose();

            ServerEvents.OnServerStop(new ServerStopEventArgs());

            this.Kill();
        }

        public void ErrorStop(Exception e, bool sendExceptionMessage = false)
        {
            this.logger = new Logger();
            Logger.Fatal(e.ToString());
            Logger.Error("%server_stop_error");
            Logger.Info("%server_stop");

            this.mineNETConfig?.Save<MineNETConfig>();
            this.serverConfig?.Save<ServerConfig>();

            Player[] players = this.GetPlayers();
            if (players != null)
            {
                for (int i = 0; i < players.Length; ++i)
                {
                    if (sendExceptionMessage)
                    {
                        players[i].Close(e.ToString());
                    }
                    else
                    {
                        players[i].Close(this.mineNETConfig.ServerStopText);
                    }
                }
            }

            this.UnloadWorld();

            this.NetworkManager?.Server?.UDPClientClose();

            ServerEvents.OnServerStop(new ServerStopEventArgs());

            this.Kill();
        }

        public void AddPlayer(Player player, PlayerListEntry entry, AdventureSettingsEntry adventureSettingsEntry)
        {
            if (!this.playerListEntries.ContainsValue(entry))
            {
                this.playerListEntries[player.EntityID] = entry;
                this.SendPlayerLists(player);
                this.AddPlayerList(entry);

                this.adventureSettingsEntry[player.EntityID] = adventureSettingsEntry;
                this.SendAdventureSettings(player);
                this.AddAdventureSettings(adventureSettingsEntry);
            }
        }

        public void RemovePlayer(long entityID)
        {
            if (this.playerListEntries.ContainsKey(entityID))
            {
                PlayerListEntry entry = this.playerListEntries[entityID];
                this.RemovePlayerList(entry);
                this.playerListEntries.Remove(entityID);
                this.adventureSettingsEntry.Remove(entityID);
            }
        }

        public void SendPlayerLists(Player player)
        {
            PlayerListPacket pk = new PlayerListPacket();
            pk.Type = PlayerListPacket.TYPE_ADD;
            pk.Entries = this.playerListEntries.Values.ToArray();
            player.SendPacket(pk);
        }

        public void SendAdventureSettings(Player player)
        {
            foreach (AdventureSettingsEntry entry in this.adventureSettingsEntry.Values)
            {
                AdventureSettingsPacket pk = new AdventureSettingsPacket();
                pk.Entry = entry;
                player.SendPacket(pk);
            }
        }

        public Player[] GetPlayers()
        {
            return this.NetworkManager?.players.Values.ToArray();
        }

        public Player GetPlayer(string name)
        {
            Player found = null;
            Player[] players = this.GetPlayers();
            int delta = 100;
            name = name.ToLower();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].Name.ToLower().StartsWith(name))
                {
                    int curDelta = players[i].Name.Length - name.Length;
                    if (curDelta < delta)
                    {
                        found = players[i];
                        delta = curDelta;
                    }
                    if (curDelta == 0)
                    {
                        break;
                    }
                }
            }

            return found;
        }

        public void BroadcastPacket(DataPacket pk, Player[] players = null)
        {
            if (players == null)
            {
                players = this.GetPlayers();
            }
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendPacket(pk);
            }
        }

        public void BroadcastMessage(string message, Player[] players = null)
        {
            if (players == null)
            {
                players = this.GetPlayers();
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendMessage(message);
            }
        }

        public void BroadcastMessage(TranslationMessage message, Player[] players = null)
        {
            if (players == null)
            {
                players = this.GetPlayers();
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendMessage(message);
            }
        }

        public void BroadcastMessageAndLoggerSend(TranslationMessage message, Player[] players = null)
        {
            if (players == null)
            {
                players = this.GetPlayers();
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendMessage(message);
            }

            if (message.TranslationFills == null)
            {
                Logger.Info($"%{message.TranslationKey}");
            }
            else
            {
                Logger.Info($"%{message.TranslationKey}", message.TranslationFills);
            }
        }

        public void BroadcastChat(string message, Player[] players = null)
        {
            if (players == null)
            {
                players = this.GetPlayers();
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendMessage(message);
            }

            Logger.Info(message);
        }

        public PlayerListEntry GetPlayerListEntry(Player player)
        {
            return this.GetPlayerListEntry(player.EntityID);
        }

        public PlayerListEntry GetPlayerListEntry(long entityID)
        {
            if (this.playerListEntries.ContainsKey(entityID))
            {
                return this.playerListEntries[entityID];
            }
            return null;
        }

        public AdventureSettingsEntry GetAdventureSettingsEntry(Player player)
        {
            return this.GetAdventureSettingsEntry(player.EntityID);
        }

        public AdventureSettingsEntry GetAdventureSettingsEntry(long entityID)
        {
            if (this.adventureSettingsEntry.ContainsKey(entityID))
            {
                return this.adventureSettingsEntry[entityID];
            }
            return null;
        }

        public void UpdateAdventureSettings(AdventureSettingsEntry entry)
        {
            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                AdventureSettingsPacket pk = new AdventureSettingsPacket();
                pk.Entry = entry;
                players[i].SendPacket(pk);
            }
        }
    }
}
