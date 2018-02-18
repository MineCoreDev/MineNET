namespace MineNET.Plugins
{
    public interface IPlugin
    {
        string Name
        {
            get;
        }

        string FileName
        {
            get;
        }

        string Directory
        {
            get;
        }

        bool Loaded
        {
            get;
        }

        void Init(PluginAttribute plugin);
        void OnLoad();
        void OnEnable();
        void OnDisable();
        void OnUnLoad();
    }
}
