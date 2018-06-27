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
using MineNET.Worlds;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public long Tick { get; private set; }

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

        public World MainWorld { get; private set; }
        public List<World> SubWorlds { get; } = new List<World>();
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

        public bool ErrorStop(Exception e)
        {
            if (this.Status == ServerStatus.Running)
            {
                //TODO: ServerErrorStopEvent...
                OutLog.Fatal("%server.error.stop");
                OutLog.Error(e.ToString());
                OutLog.Info("%server.stoping");
                this.Dispose();
                BlockInit.In.Dispose();
                ItemInit.In.Dispose();

                this.Status = ServerStatus.Stop;

                //TODO: ServerErrorStopedEvent...
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Init Method
        public void Init(Stopwatch sw)
        {
            this.Clock = new ConstantClockManager();

            MineNET_Registries.Init();
            new BlockInit();
            new ItemInit();
            new EffectInit();

            this.Event = new EventManager();
            this.Logger = new Logger();
            OutLog.Info("%server.start");
            this.Plugin = new PluginManager();
            this.Command = new CommandManager();

            this.Event.Server.OnServerStart(this, new ServerStartEventArgs());

            this.MainWorld = new World();

            this.LoadConfig();

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

        public void LoadConfig()
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
                players[i].UpdateTick(this.Tick);
            }

            this.Logger.Input.GetQueueCommand();

            this.Tick++;
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

            this.Clock?.Dispose();
            this.Plugin?.Dispose();
            this.Command?.Dispose();
            this.Network?.Dispose();
            OutLog.Info("%server.network.stop", sw.Elapsed.ToString(@"mm\:ss\.fff"));
            this.NetworkSocket?.Dispose();
            sw.Stop();
            OutLog.Info("%server.stoped", sw.Elapsed.ToString(@"mm\:ss\.fff"));
            this.Logger?.Dispose();

            Server.Instance = null;
        }
        #endregion
    }
}
