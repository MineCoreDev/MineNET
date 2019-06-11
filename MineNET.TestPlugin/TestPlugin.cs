using MineNET.Events;
using MineNET.IO;
using MineNET.Plugins;

namespace MineNET.TestPlugin
{
    public class TestPlugin : PluginBase
    {
        public override string Name
        {
            get { return "MineNET_TestPlugin"; }
        }

        public override string Description
        {
            get { return "MineNET Test Plugin."; }
        }

        public override PluginFlags Flag
        {
            get { return base.Flag | PluginFlags.GenerateConfig; }
        }

        public override void OnLoad()
        {
            Logger.Info("Good morning MineNET plugin!");
            this.Config.Set("key1", "ウンチーコング");
        }

        public override void OnDisable()
        {
            Logger.Info("Goodbye MineNET plugin!");
            this.Config.Save();
        }

        public override void OnEnable()
        {
            Logger.Info("Hello MineNET plugin!");

            Server.Instance.Event.IO.InputAction += IO_InputAction;

            Server.Instance.Event.Network.CreateSession += Network_CreateSession;
            Server.Instance.Event.Network.RakNetDataPacketReceive += Network_RakNetDataPacketReceive;
            Server.Instance.Event.Network.RakNetDataPacketSend += Network_RakNetDataPacketSend;
            Server.Instance.Event.Network.RakNetPacketReceive += Network_RakNetPacketReceive;
            Server.Instance.Event.Network.RakNetPacketSend += Network_RakNetPacketSend;

            Server.Instance.Event.Player.PlayerCommand += Player_PlayerCommand;
            Server.Instance.Event.Player.PlayerCreate += Player_PlayerCreate;

            //Server.Instance.Event.Server.ServerCommand += Server_ServerCommand;
            Server.Instance.Event.Server.ServerStart += Server_ServerStart;
            Server.Instance.Event.Server.ServerStop += Server_ServerStop;
        }

        private void Server_ServerStop(object sender, Events.ServerEvents.ServerStopEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void Server_ServerStart(object sender, Events.ServerEvents.ServerStartEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        [EventHandler]
        public void Server_ServerCommand(object sender, Events.ServerEvents.ServerCommandEventArgs e)
        {
            Logger.Info(e.GetType().Name);
        }

        private void Player_PlayerCreate(object sender, Events.PlayerEvents.PlayerCreateEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void Player_PlayerCommand(object sender, Events.PlayerEvents.PlayerCommandEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void Network_RakNetPacketSend(object sender, Events.NetworkEvents.RakNet.RakNetPacketSendEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void Network_RakNetPacketReceive(object sender,
            Events.NetworkEvents.RakNet.RakNetPacketReceiveEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void Network_RakNetDataPacketSend(object sender,
            Events.NetworkEvents.RakNet.RakNetDataPacketSendEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void Network_RakNetDataPacketReceive(object sender,
            Events.NetworkEvents.RakNet.RakNetDataPacketReceiveEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void Network_CreateSession(object sender, Events.NetworkEvents.CreateSessionEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }

        private void IO_InputAction(object sender, Events.IOEvents.InputActionEventArgs e)
        {
            Logger.Trace(e.GetType().Name);
        }
    }
}