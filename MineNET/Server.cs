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

        public void Stop()
        {
            Logger.Info("%server_stop");
            this.mineNETConfig.Save<MineNETConfig>();
            this.serverConfig.Save<ServerConfig>();

            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Close(mineNETConfig.ServerStopText);
            }

            this.NetworkManager.Server.UDPClientClose();

            ServerEvents.OnServerStop(new ServerStopEventArgs());

            this.Kill();
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
            if (players != null)
            {
                for (int i = 0; i < players.Length; ++i)
                {
                    players[i].Close(mineNETConfig.ServerStopText);
                }
            }

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
            return this.networkManager?.players.Values.ToArray();
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
    }
}
