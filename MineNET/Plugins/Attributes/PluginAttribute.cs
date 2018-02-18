using System;

namespace MineNET.Plugins
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PluginAttribute : Attribute
    {
        public PluginAttribute(string pluginName)
        {
            if (string.IsNullOrEmpty(pluginName))
            {
                this.PluginName = "Test";
            }
            else
            {
                this.PluginName = pluginName;
            }
        }

        public string PluginName
        {
            get;
        }

        public string PluginPath
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }
    }
}
