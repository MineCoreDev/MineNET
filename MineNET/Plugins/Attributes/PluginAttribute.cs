using System;

namespace MineNET.Plugins
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PluginAttribute : Attribute
    {
        public string PluginName { get; }
        public string PluginDescription { get; }
        public APIVersion PluginAPIVersion { get; }

        public PluginAttribute(string pluginName, string pluginDescription, APIVersion pluginAPIVersion)
        {
            this.PluginName = pluginName;
            this.PluginDescription = pluginDescription;
            this.PluginAPIVersion = pluginAPIVersion;
        }
    }
}
