using MineNET.Commands;
using MineNET.Entities.Players;
using MineNET.Events;
using MineNET.Events.ServerEvents;
using MineNET.Init;
using MineNET.IO;
using MineNET.Manager;
using MineNET.Network;
using MineNET.Network.MinecraftPackets;
using MineNET.Plugins;
using MineNET.Reports;
using MineNET.Worlds;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace MineNET
{
    public sealed class Server : IDisposable
    {
        #region Static Method
        public static string ExecutePath { get; } = Environment.CurrentDirectory;
        #endregion

        #region Singleton Instance
        public static Server Instance { get; private set; }
        #endregion

        #region Property & Field
        public ServerStatus Status { get; private set; } = ServerStatus.Stop;

        public ConstantClockManager Clock { get; private set; }

        public EventManager Event { get; private set; }
        public PluginManager Plugin { get; private set; }

        public MineNETConfig Config { get; private set; }
        public ServerConfig ServerProperty { get; private set; }

        public ILogger Logger { get; private set; }
        public CommandManager Command { get; private set; }

        public IServerListInfo ServerList { get; set; }

        public INetworkSocket NetworkSocket { get; private set; }
        public NetworkManager Network { get; private set; }
        public IPEndPoint EndPoint { get; private set; }

        public Dictionary<string, World> Worlds { get; } = new Dictionary<string, World>();
        #endregion

        #region Ctor
        public Server()
        {
            Server.Instance = this;
        }
        #endregion

        #region Start & Stop Method
        public bool Start()
        {
            if (this.Status == ServerStatus.Stop)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    Thread.CurrentThread.Name = "ServerThread";
                    this.Init(sw);
                    sw.Stop();

                    OutLog.Info("%server.start.done");
                    OutLog.Info("%server.start.done2", sw.Elapsed.ToString(@"mm\:ss\.fff"));
                    this.Status = ServerStatus.Running;

                    //TODO: ServerStartedEvent...
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    this.Status = ServerStatus.Error;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Restart()
        {

        }

        public bool Stop(string reason = "")
        {
            if (this.Status == ServerStatus.Running)
            {
                try
                {
                    this.Event.Server.OnServerStop(this, new ServerStopEventArgs());
                    OutLog.Info("%server.stoping");

                    this.Status = ServerStatus.Stoping;

                    foreach (Player player in this.GetPlayers())
                    {
                        if (reason == "")
                        {
                            reason = "disconnect.closed";
                        }

                        player.Close(reason);
                    }

                    this.Dispose();
                    this.Status = ServerStatus.Stop;

                    //TODO: ServerStopedEvent...
                    return true;
                }
                catch
                {
                    this.Status = ServerStatus.Error;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void ErrorStop(Exception e)
        {
            //TODO: ServerErrorStopEvent...
            OutLog.Fatal("%server.error.stop");
            if (e != null)
            {
                OutLog.Error(e.ToString());
            }
            OutLog.Info("%server.stoping");
            CrashReport.ExportReport(e.GetType().Name, e);
            this.Dispose();
            BlockInit.In?.Dispose();
            ItemInit.In?.Dispose();

            this.Status = ServerStatus.Stop;

            //TODO: ServerErrorStopedEvent...
        }

        public void StartUpdate()
        {
            long tick = 0;
            while (this.Status == ServerStatus.Running)
            {
                this.Clock.Start("server.update");
                this.OnUpdate(tick);
                tick++;
                this.Clock.Stop("server.update");
            }
        }
        #endregion

        #region Init Method
        public void Init(Stopwatch sw)
        {
            this.OnServerStart();

            this.LoadConfigs();
            this.LoadWorlds(sw);

            this.StartNetwork(sw);
        }

        private void InitRegistries()
        {
            MineNET_Registries.Init();
            new BlockInit();
            new ItemInit();
            new EffectInit();
        }

        private void OnServerStart()
        {
            this.Clock = new ConstantClockManager();

            this.InitRegistries();

            this.Event = new EventManager();
            this.Logger = new Logger();
            OutLog.Info("%server.start");
            this.Plugin = new PluginManager();
            this.Command = new CommandManager();

            this.Event.Server.OnServerStart(this, new ServerStartEventArgs());
        }

        private void StartNetwork(Stopwatch sw)
        {
            this.ServerList = new ServerListInfo();

            if (this.NetworkSocket == null)
            {
                int port = this.ServerProperty.ServerPort;
                OutLog.Info("%server.network.start", port);

                this.EndPoint = new IPEndPoint(IPAddress.Any, port);
                this.SetNetworkSocket(new UDPSocket(this.EndPoint));
            }
            this.Network = new NetworkManager();
            OutLog.Info("%server.network.start.done", sw.Elapsed.ToString(@"mm\:ss\.fff"));
        }

        private void LoadWorlds(Stopwatch sw)
        {
            string worldFolder = Server.ExecutePath + "\\worlds";
            if (Directory.Exists(worldFolder))
            {
                Directory.CreateDirectory(worldFolder);
            }

            string mainWorld = this.ServerProperty.MainWorldName;
            if (!World.Exists(mainWorld))
            {
                World.CreateWorld(mainWorld);
                OutLog.Notice("%server.world.create", mainWorld);
            }
            else
            {
                World.LoadWorld(mainWorld);
                OutLog.Info("%server.world.load", mainWorld);
            }

            string[] subWorlds = this.ServerProperty.LoadWorldNames;
            for (int i = 0; i < subWorlds.Length; ++i)
            {
                if (!World.Exists(subWorlds[i]))
                {
                    World.CreateWorld(subWorlds[i]);
                    OutLog.Notice("%server.world.create", subWorlds[i]);
                }
                else
                {
                    World.LoadWorld(subWorlds[i]);
                    OutLog.Info("%server.world.load", subWorlds[i]);
                }
            }
        }

        private void LoadConfigs()
        {
            this.Config = MineNETConfig.Load<MineNETConfig>($"{ExecutePath}\\MineNET.yml");
            this.ServerProperty = ServerConfig.Load<ServerConfig>($"{ExecutePath}\\ServerProperties.yml");
        }
        #endregion

        #region Update Method
        public void OnUpdate(long tick)
        {
            if (this.Status != ServerStatus.Running)
            {
                return;
            }

            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].UpdateTick(tick);
            }

            World[] worlds = this.GetWorlds();
            for (int i = 0; i < worlds.Length; ++i)
            {
                worlds[i].UpdateTick(tick);
            }

            this.Logger.Input.GetQueueCommand();
        }
        #endregion

        #region Interface Set Method
        public bool SetNetworkSocket(INetworkSocket socket)
        {
            if (this.Status == ServerStatus.Stop)
            {
                this.NetworkSocket = socket;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetLoggger(ILogger logger)
        {
            if (this.Status == ServerStatus.Stop)
            {
                this.Logger = logger;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Player Method
        public Player[] GetPlayers()
        {
            List<Player> list = new List<Player>();
            foreach (Player player in this.Network.Players.Values)
            {
                list.Add(player);
            }

            return list.ToArray();
        }
        #endregion

        #region World Method
        public World[] GetWorlds()
        {
            return this.Worlds.Values.ToArray();
        }
        #endregion

        public void BroadcastSendPacket(MinecraftPacket packet, Player[] players = null)
        {
            if (players == null)
            {
                players = Server.Instance.GetPlayers();
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendPacket(packet);
            }
        }

        #region Dispose Method
        public void Dispose()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            this.Worlds.Clear();
            this.Plugin?.Dispose();
            this.Command?.Dispose();
            this.Network?.Dispose();
            OutLog.Info("%server.network.stop", sw.Elapsed.ToString(@"mm\:ss\.fff"));
            this.NetworkSocket?.Dispose();
            sw.Stop();
            OutLog.Info("%server.stoped", sw.Elapsed.ToString(@"mm\:ss\.fff"));
            this.Logger?.Dispose();
            this.Clock?.Dispose();

            Server.Instance = null;
        }
        #endregion
    }
}
