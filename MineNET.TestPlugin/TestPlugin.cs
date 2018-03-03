using System;
using MineNET.Events.ServerEvents;
using MineNET.Plugins;
using MineNET.Utils;

namespace MineNET.TestPlugin
{
    [Plugin]
    public class TestPlugin : PluginBase
    {
        public override void OnLoad()
        {
            Logger.Log("Hallo World!");
            Logger.Info("Hallo World!");
            Logger.Warning("Hallo World!");
            Logger.Error("Hallo World!");
            Logger.Fatal("Hallo World!");

            ServerEvents.ServerStart += ServerEvents_ServerStart;
            ServerEvents.ServerStop += ServerEvents_ServerStop;
        }

        private void ServerEvents_ServerStop(ServerStopEventArgs args)
        {
            Logger.Warning("Server Stop");
        }

        private void ServerEvents_ServerStart(ServerStartEventArgs args)
        {
            Logger.Log("Server Start");
        }

        public override void OnUnLoad()
        {
            throw new NotImplementedException();
        }

        public override void OnDisable()
        {
            throw new NotImplementedException();
        }

        public override void OnEnable()
        {
            throw new NotImplementedException();
        }
    }
}
