using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        public Logger Logger { get; set; }

        public IConsoleInput ConsoleInput { get; set; }

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

            this.PluginManager.DisablePlugins();

            this.UnloadWorld();

            this.NetworkManager.Server.UDPClientClose();

            ServerEvents.OnServerStop(new ServerStopEventArgs());

            this.Kill();
        }

        public void ErrorStop(Exception e, bool sendExceptionMessage = false)
        {
            this.Logger = new Logger();
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

        public void AddPlayer(Player player)
        {
            this.SendPlayerLists(player);
            this.AddPlayerList(player);
            this.SendAdventureSettings(player);
            this.AddAdventureSettings(player);
        }

        public void RemovePlayer(long entityID)
        {
            if (this.PlayerList.ContainsKey(entityID))
            {
                this.RemovePlayerList(entityID);
                this.PlayerList.Remove(entityID);
            }
        }

        public void SendPlayerLists(Player player)
        {
            List<PlayerListEntry> entries = new List<PlayerListEntry>();
            Player[] players = this.PlayerList.Values.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                entries.Add(players[i].PlayerListEntry);
            }

            PlayerListPacket pk = new PlayerListPacket();
            pk.Type = PlayerListPacket.TYPE_ADD;
            pk.Entries = entries.ToArray();
            player.SendPacket(pk);
        }

        public void SendAdventureSettings(Player player)
        {
            Player[] players = this.PlayerList.Values.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].AdventureSettingsEntry.Update(player);
            }
        }

        public Player[] GetPlayers()
        {
            return this.NetworkManager?.players.Values.ToArray();
        }

        public Player[] GetOnlinePlayers()
        {
            Player[] players = this.NetworkManager?.players.Values.ToArray();
            List<Player> online = new List<Player>();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].HasSpawned)
                {
                    online.Add(players[i]);
                }
            }

            return online.ToArray();
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

        public bool TryGetPlayer(string name, out Player result)
        {
            result = this.GetPlayer(name);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public bool Whitelist
        {
            get
            {
                return Server.ServerConfig.WhiteList;
            }

            set
            {
                Server.ServerConfig.WhiteList = value;
                Server.ServerConfig.Save<ServerConfig>();
            }
        }

        public bool IsWhitelist(Player player)
        {
            return this.IsWhitelist(player.Name);
        }

        public bool IsWhitelist(string name)
        {
            return this.WhitelistConfig.ContainsKey(name);
        }

        public void AddWhitelist(Player player)
        {
            this.AddWhitelist(player.Name);
        }

        public void AddWhitelist(string name)
        {
            this.WhitelistConfig.Set(name, true);
            this.WhitelistConfig.Save();
        }

        public void RemoveWhitelist(Player player)
        {
            this.RemoveWhitelist(player.Name);
        }

        public void RemoveWhitelist(string name)
        {
            if (!this.IsWhitelist(name))
            {
                return;
            }
            this.WhitelistConfig.Remove(name);
            this.WhitelistConfig.Save();
        }

        public bool IsBan(Player player)
        {
            return this.IsBan(player.Name);
        }

        public bool IsBan(string name)
        {
            return this.BanConfig.ContainsKey(name);
        }

        public void AddBan(Player player)
        {
            this.AddBan(player.Name);
        }

        public void AddBan(string name)
        {
            this.BanConfig.Set(name, true);
            this.BanConfig.Save();
        }

        public void RemoveBan(Player player)
        {
            this.RemoveBan(player.Name);
        }

        public void RemoveBan(string name)
        {
            if (!this.IsBan(name))
            {
                return;
            }
            this.BanConfig.Remove(name);
            this.BanConfig.Save();
        }

        public bool IsBanIp(Player player)
        {
            return this.IsBanIp(player.EndPoint);
        }

        public bool IsBanIp(IPEndPoint endPoint)
        {
            return this.IsBanIp(endPoint.Address.ToString());
        }

        public bool IsBanIp(string name)
        {
            return this.BanIpConfig.ContainsKey(name);
        }

        public void AddBanIp(Player player)
        {
            this.AddBanIp(player.EndPoint);
        }

        public void AddBanIp(IPEndPoint endPoint)
        {
            this.AddBanIp(endPoint.Address);
        }

        public void AddBanIp(IPAddress address)
        {
            this.AddBanIp(address.ToString());
        }

        public void AddBanIp(string ip)
        {
            this.BanIpConfig.Set(ip, true);
            this.BanIpConfig.Save();
        }

        public void RemoveBanIp(Player player)
        {
            this.RemoveBanIp(player.EndPoint);
        }

        public void RemoveBanIp(IPEndPoint endPoint)
        {
            this.RemoveBanIp(endPoint.Address);
        }

        public void RemoveBanIp(IPAddress address)
        {
            this.RemoveBanIp(address.ToString());
        }

        public void RemoveBanIp(string ip)
        {
            if (!this.IsBanIp(ip))
            {
                return;
            }
            this.BanIpConfig.Remove(ip);
            this.BanIpConfig.Save();
        }

        public bool IsOp(Player player)
        {
            return this.IsOp(player.Name);
        }

        public bool IsOp(string name)
        {
            return this.OpsConfig.ContainsKey(name);
        }

        public void AddOp(Player player)
        {
            this.AddOp(player.Name);
        }

        public void AddOp(string name)
        {
            this.OpsConfig.Set(name, true);
            this.OpsConfig.Save();
        }

        public void RemoveOp(Player player)
        {
            this.RemoveOp(player.Name);
        }

        public void RemoveOp(string name)
        {
            if (!this.IsOp(name))
            {
                return;
            }
            this.OpsConfig.Remove(name);
            this.OpsConfig.Save();
        }
    }
}
