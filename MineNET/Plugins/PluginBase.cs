namespace MineNET.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public bool Loaded { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public APIVersion APIVersion { get; private set; }

        public void Init(PluginAttribute plugin)
        {
            this.Name = plugin.PluginName;
            this.Description = plugin.PluginDescription;
            this.APIVersion = plugin.PluginAPIVersion;
        }

        public abstract void OnDisable();
        public abstract void OnEnable();
        public abstract void OnLoad();
        public abstract void OnUnLoad();
    }
}
