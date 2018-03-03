using System;

namespace MineNET.Plugins
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PluginAttribute : Attribute
    {
        internal string pluginPath;
        internal string pluginName;
    }
}
