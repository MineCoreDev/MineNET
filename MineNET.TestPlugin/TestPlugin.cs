using System;
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
