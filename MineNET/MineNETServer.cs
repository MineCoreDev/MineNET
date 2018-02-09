using System.Diagnostics;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Network;
using MineNET.RakNet;
using MineNET.Utils;

namespace MineNET
{
    public sealed class MineNETServer
    {
        static MineNETServer instance;
        public static MineNETServer Instance
        {
            get
            {
                return instance;
            }
        }

        RakNetServer server;

        ConsoleInput consoleInput;

        NetworkManager networkManager;
        public NetworkManager NetworkManager
        {
            get
            {
                return networkManager;
            }
        }
        CommandManager commandManager;
        public CommandManager CommandManager
        {
            get
            {
                return commandManager;
            }
        }

        bool isShutdown;
        public bool IsShutdown()
        {
            return isShutdown;
        }

        public void Start()
        {
            instance = this;

            Stopwatch s = new Stopwatch();
            s.Start();

            Init();

            s.Stop();
            Logger.Info(LangManager.GetString("server_started"));
            Logger.Info(LangManager.GetString("server_started2"), s.Elapsed.ToString());
        }

        void Init()
        {
            UpdateLogger();
            Update();//StartUpdate

            networkManager = new NetworkManager();
            commandManager = new CommandManager();

            consoleInput = new ConsoleInput();

            Logger.Init();
            Logger.Info(LangManager.GetString("server_start"));
            server = new RakNetServer(1115);
        }

        async void Update()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1000 / 20);
                CommandHandle();
            }
        }

        async void UpdateLogger()
        {
            while (!IsShutdown())
            {
                await Task.Delay(1000 / 20);
                Logger.Update();
            }
        }

        public void Stop()
        {
            Logger.Info(LangManager.GetString("server_stop"));
            Kill();
        }

        public void Kill()
        {
            Logger.Info(LangManager.GetString("server_stoped"));
            Killed();
        }

        async void Killed()
        {
            await Task.Delay(1000);
            isShutdown = true;
        }

        void CommandHandle()
        {
            string cmd = consoleInput.GetCommand();
            if (!string.IsNullOrEmpty(cmd))
            {
                commandManager.HandleConsoleCommand(cmd);
            }
        }
    }
}
