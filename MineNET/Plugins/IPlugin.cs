using System;

namespace MineNET.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }
        ApiVersion ApiVersion { get; }
        PluginFlags Flag { get; }
        string[] PremisePlugins { get; }

        void OnLoad();
        void OnEnable();
        void OnDisable();
        void OnError(Exception e);
    }
}
