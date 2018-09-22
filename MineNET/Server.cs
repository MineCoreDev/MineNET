using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
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
using MineNET.Text;
using MineNET.Utils.Config;
using MineNET.Worlds;

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

        public IServerLogger Logger { get; private set; } = new Logger();
        public CommandManager Command { get; private set; }

        public IServerListInfo ServerList { get; set; }

        public INetworkSocket NetworkSocket { get; private set; }
        public NetworkManager Network { get; private set; }
        public IPEndPoint EndPoint { get; private set; }

        public Dictionary<string, World> Worlds { get; } = new Dictionary<string, World>();

        public ConcurrentQueue<Action> InvokeMainThreadActions { get; } = new ConcurrentQueue<Action>();

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

                    if (Thread.CurrentThread.Name != "ServerThread")
                    {
                        Thread.CurrentThread.Name = "ServerThread";
                    }
                    this.Init(sw);
                    sw.Stop();

                    IO.Logger.Info("%server.start.done");
                    IO.Logger.Info("%server.start.done2", sw.Elapsed.ToString(@"mm\:ss\.fff"));
                    this.Status = ServerStatus.Running;

                    //TODO: ServerStartedEvent...
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    this.Status = ServerStatus.Error;
                    this.ErrorStop(e);
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
                    IO.Logger.Info("%server.stopping");

                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    this.Status = ServerStatus.Stoping;

                    foreach (Player player in this.GetPlayers())
                    {
                        if (reason == "")
                        {
                            reason = "disconnect.closed";
                        }

                        player.Close(reason);
                    }

                    foreach (World world in this.GetWorlds())
                    {
                        world.Save();
                    }

                    this.Dispose(sw);
                    this.Status = ServerStatus.Stop;

                    //TODO: ServerStopedEvent...
                    return true;
                }
                catch (Exception e)
                {
                    IO.Logger.Error(e);
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
            IO.Logger.Fatal("%server.error.stop");
            if (e != null)
            {
                IO.Logger.Error(e.ToString());
            }
            IO.Logger.Info("%server.stopping");
            CrashReport.ExportReport(e.GetType()?.Name, e);

            this.Dispose();
            BlockInit.In?.Dispose();
            ItemInit.In?.Dispose();

            this.Status = ServerStatus.Stop;

            //TODO: ServerErrorStopedEvent...

            Environment.Exit(-1);
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
            this.LoadConfigs();
            this.OnServerStart();

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

            GlobalBlockPalette.Init();

            this.InitRegistries();

            this.Event = new EventManager();
            IO.Logger.Info("%server.start");
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
                IO.Logger.Info("%server.network.start", port);

                this.EndPoint = new IPEndPoint(IPAddress.Any, port);
                INetworkSocket socket = new UDPSocket();
                socket.Init(this.EndPoint);
                this.SetNetworkSocket(socket);
            }
            this.Network = new NetworkManager();
            IO.Logger.Info("%server.network.start.done", sw.Elapsed.ToString(@"mm\:ss\.fff"));
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
                IO.Logger.Info("%server.world.create", mainWorld);
            }
            else
            {
                World.LoadWorld(mainWorld);
                IO.Logger.Info("%server.world.load", mainWorld);
            }

            string[] subWorlds = this.ServerProperty.LoadWorldNames;
            for (int i = 0; i < subWorlds.Length; ++i)
            {
                if (!World.Exists(subWorlds[i]))
                {
                    World.CreateWorld(subWorlds[i]);
                    IO.Logger.Info("%server.world.create", subWorlds[i]);
                }
                else
                {
                    World.LoadWorld(subWorlds[i]);
                    IO.Logger.Info("%server.world.load", subWorlds[i]);
                }
            }
        }

        private void LoadConfigs()
        {
            this.Config = YamlStaticConfig.Load<MineNETConfig>($"{ExecutePath}\\MineNET.yml");
            this.ServerProperty = YamlStaticConfig.Load<ServerConfig>($"{ExecutePath}\\ServerProperties.yml");
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

            if (this.InvokeMainThreadActions.Count > 0)
            {
                Action action = null;
                if (this.InvokeMainThreadActions.TryDequeue(out action))
                {
                    action();
                }
            }

            this.Logger.InputLogger.GetInputQueue();
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

        public bool SetLoggger(IServerLogger logger)
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

        public Player[] GetOnlinePlayers()
        {
            Player[] players = this.Network.Players.Values.ToArray();
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
        #endregion

        #region World Method
        public World[] GetWorlds()
        {
            return this.Worlds.Values.ToArray();
        }
        #endregion

        #region Update Thread Invoke Method

        public void Invoke(Action action)
        {
            this.InvokeMainThreadActions.Enqueue(action);
        }

        #endregion

        #region Broadcast Method

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

        public void BroadcastMessage(TranslationContainer message, Player[] players = null)
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

        public void BroadcastMessageAndLoggerSend(TranslationContainer message, Player[] players = null)
        {
            if (players == null)
            {
                players = this.GetPlayers();
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendMessage(message);
            }

            if (message.Args == null)
            {
                IO.Logger.Info(new CultureTextContainer(message.Key));
            }
            else
            {
                IO.Logger.Info(new CultureTextContainer(message.Key, message.Args));
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

            this.Logger.OutputLogger.Info(message);
        }

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
        #endregion

        #region Dispose Method
        public void Dispose()
        {
            GlobalBlockPalette.Clear();

            this.Logger?.Dispose();
            this.Worlds.Clear();
            this.Plugin?.Dispose();
            this.Command?.Dispose();
            this.Network?.Dispose();
            this.NetworkSocket?.Dispose();
            this.Clock?.Dispose();

            Server.Instance = null;
        }

        public void Dispose(Stopwatch sw)
        {
            GlobalBlockPalette.Clear();

            this.Worlds.Clear();
            this.Plugin?.Dispose();
            this.Command?.Dispose();
            this.Network?.Dispose();
            IO.Logger.Info("%server.network.stop", sw.Elapsed.ToString(@"mm\:ss\.fff"));
            this.NetworkSocket?.Dispose();
            sw.Stop();
            IO.Logger.Info("%server.stoped", sw.Elapsed.ToString(@"mm\:ss\.fff"));
            this.Clock?.Dispose();

            Server.Instance = null;
        }
        #endregion
    }
}
