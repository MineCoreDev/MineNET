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
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network;
using MineNET.Network.MinecraftPackets;
using MineNET.Plugins;
using MineNET.Reports;
using MineNET.Text;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Dimensions;

namespace MineNET
{
    public partial class Server : IDisposable
    {
        public static string ExecutePath { get; } = Environment.CurrentDirectory;
        public static string PlayerDataPath { get; } = $"{ExecutePath}/players";

        public static Server Instance { get; private set; }

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

        public Server()
        {
            Instance = this;
        }

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

                    this.Event.Server.OnServerStopped(this, new ServerStoppedEventArgs());
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

            this.Event.Server.OnServerStopped(this, new ServerStoppedEventArgs());
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

        public void OnUpdate(long tick)
        {
            if (this.Status != ServerStatus.Running)
            {
                return;
            }

            Player[] players = this.GetPlayers();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].UpdateTick(tick);
            }

            World[] worlds = this.GetWorlds();
            for (int i = 0; i < worlds.Length; i++)
            {
                worlds[i].UpdateTick(tick);
            }

            if (this.InvokeMainThreadActions.Count > 0)
            {
                for (int i = 0; i < this.InvokeMainThreadActions.Count; i++)
                {
                    Action action = null;
                    if (this.InvokeMainThreadActions.TryDequeue(out action))
                    {
                        try
                        {
                            action();
                        }
                        catch (Exception e)
                        {
                            Logger.OutputLogger.Error(e);
                        }
                    }
                }
            }

            this.Logger.InputLogger.GetInputQueue();
        }

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

        public World[] GetWorlds()
        {
            return this.Worlds.Values.ToArray();
        }

        public World GetWorld(String name)
        {
            World[] worlds = this.GetWorlds();
            for (int i = 0; i < worlds.Length; ++i)
            {
                if (worlds[i].Name == name)
                {
                    return worlds[i];
                }
            }

            return null;
        }

        public void Invoke(Action action)
        {
            this.InvokeMainThreadActions.Enqueue(action);
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
                players = Instance.GetPlayers();
            }

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendPacket(packet);
            }
        }

        public CompoundTag GetOfflinePlayerData(string xuid)
        {
            string path = $"{PlayerDataPath}/{xuid}.dat";
            CompoundTag nbt;
            if (!File.Exists(path))
            {
                World world = World.GetMainWorld();
                Position pos = world.GetWorldSpawn();
                nbt = new CompoundTag()
                    .PutLong("firstPlayed", DateTime.Now.ToBinary())
                    .PutLong("lastPlayed", DateTime.Now.ToBinary())
                    .PutList(new ListTag("Pos", NBTTagType.FLOAT)
                        .Add(new FloatTag("", pos.X))
                        .Add(new FloatTag("", pos.Y))
                        .Add(new FloatTag("", pos.Z)))
                    .PutList(new ListTag("Motion", NBTTagType.FLOAT)
                        .Add(new FloatTag("", 0))
                        .Add(new FloatTag("", 0))
                        .Add(new FloatTag("", 0)))
                    .PutList(new ListTag("Rotation", NBTTagType.FLOAT)
                        .Add(new FloatTag("", 0))
                        .Add(new FloatTag("", 0)))
                    .PutString("World", pos.World.Name)
                    .PutInt("Dimension", DimensionIDs.OverWorld)
                    .PutInt("PlayerGameType", Instance.ServerProperty.GameMode.GetIndex())
                    .PutInt("SpawnX", world.SpawnX)
                    .PutInt("SpawnY", world.SpawnY)
                    .PutInt("SpawnZ", world.SpawnZ)
                    .PutInt("Score", 0);

                this.SaveOfflinePlayerData(xuid, nbt);
            }
            else
            {
                nbt = NBTIO.ReadGZIPFile(path, NBTEndian.BIG_ENDIAN);
            }

            return nbt;
        }

        public void SaveOfflinePlayerData(string xuid, CompoundTag nbt)
        {
            string path = $"{PlayerDataPath}/{xuid}.dat";
            NBTIO.WriteGZIPFile(path, nbt, NBTEndian.BIG_ENDIAN);
        }

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

            Instance = null;
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

            Instance = null;
        }
    }
}