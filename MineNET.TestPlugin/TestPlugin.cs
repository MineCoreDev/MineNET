using MineNET.Events.ServerEvents;
using MineNET.Plugins;
using MineNET.Utils;

namespace MineNET.TestPlugin
{
    [Plugin("TestPlugin", "PluginTest", APIVersion.Version_1_0_0_0)]
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
            Logger.Info("Plugin UnLoad");
        }

        public override void OnDisable()
        {
            Logger.Info("Plugin Disable");
        }

        public override void OnEnable()
        {
            Logger.Info("Plugin Enable");
        }
    }
}
