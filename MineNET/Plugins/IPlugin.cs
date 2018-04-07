namespace MineNET.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        APIVersion APIVersion { get; }
        bool Loaded { get; set; }

        void Init(PluginAttribute plugin);
        void OnLoad();
        void OnEnable();
        void OnDisable();
        void OnUnLoad();
    }
}
