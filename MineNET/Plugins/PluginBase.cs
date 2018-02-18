namespace MineNET.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public string Directory { get; private set; }
        public string FileName { get; private set; }
        public bool Loaded { get; private set; }
        public string Name { get; private set; }

        public void Init(PluginAttribute plugin)
        {
            this.Directory = plugin.pluginPath;
            this.FileName = plugin.pluginName;
            this.Name = plugin.pluginName;
        }

        public abstract void OnDisable();
        public abstract void OnEnable();
        public abstract void OnLoad();
        public abstract void OnUnLoad();
    }
}
