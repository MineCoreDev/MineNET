using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using MineNET.Utils;

namespace MineNET.Plugins
{
    public class PluginManager
    {
        public const string ZIPED_PLUGIN = ".zip";
        public const string ZIPED_MINENET_PLUGIN = ".minenet";

        public const string PLUGIN_STRUCT = ".mnplugin";
        public const string PLUGIN_STRUCT_SHORT = ".mnp";

        public Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();

        public PluginManager()
        {
            this.Init();
            this.LoadPlugins();
        }

        public void Init()
        {
            string path = $"{Server.ExecutePath}\\plugins";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void LoadPlugins()
        {
            string path = $"{Server.ExecutePath}\\plugins";
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo file in dir.EnumerateFiles())
            {
                Logger.Info(file.Extension.ToLower());
                if (this.IsZipedPlugin(file.Extension))
                {
                    string temp = $"{path}\\temp";
                    FastZip zip = new FastZip();
                    zip.CreateEmptyDirectories = true;
                    zip.RestoreAttributesOnExtract = true;
                    zip.RestoreDateTimeOnExtract = true;

                    zip.ExtractZip(file.FullName, temp, "");

                    foreach (FileInfo i in new DirectoryInfo(temp).EnumerateFiles())
                    {
                        if (this.IsPlugin(i.Extension))
                        {
                            this.LoadPlugin(i.FullName);
                        }
                    }
                }
                else if (this.IsPlugin(file.Extension))
                {
                    this.LoadPlugin(file.FullName);
                }
            }
        }

        public void LoadPlugin(string path)
        {
            string fileName = Path.GetFileName(path);
            try
            {
                Assembly asm = Assembly.LoadFile(path);
                Type[] types = asm.GetTypes();
                int count = 0;
                for (int i = 0; i < types.Length; ++i)
                {
                    Type t = types[i];
                    PluginAttribute att = t.GetCustomAttribute<PluginAttribute>();
                    if (att != null && t.GetInterface("IPlugin", false) != null)
                    {
                        IPlugin plugin = (IPlugin) Activator.CreateInstance(t);
                        plugin.Init(att);
                        Logger.Info("%server_plugin_load", fileName);
                        plugin.OnLoad();
                        plugin.Loaded = true;
                        this.plugins.Add(att.PluginName, plugin);
                        count++;
                        break;
                    }
                }

                if (count != 1)
                {
                    Logger.Error("%server_plugin_notPlugin", fileName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("%server_plugin_loadError", fileName);
                Logger.Error(ex);
            }
        }

        public void EnablePlugins()
        {
            IPlugin[] plugins = this.plugins.Values.ToArray();
            for (int i = 0; i < plugins.Length; ++i)
            {
                if (plugins[i].Loaded)
                {
                    plugins[i].OnEnable();
                    Logger.Info(LangManager.GetString("server_plugin_enable"), plugins[i].Name);
                }
            }
        }

        public bool EnablePlugin(string pluginName)
        {
            if (this.plugins.ContainsKey(pluginName))
            {
                this.plugins[pluginName].OnEnable();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisablePlugins()
        {
            IPlugin[] plugins = this.plugins.Values.ToArray();
            for (int i = 0; i < plugins.Length; ++i)
            {
                if (plugins[i].Loaded)
                {
                    plugins[i].OnDisable();
                    Logger.Info(LangManager.GetString("server_plugin_disable"), plugins[i].Name);
                }
            }
            this.UnLoadPlugins();
        }

        public bool DisablePlugin(string pluginName)
        {
            if (this.plugins.ContainsKey(pluginName))
            {
                this.plugins[pluginName].OnDisable();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UnLoadPlugins()
        {
            IPlugin[] plugins = this.plugins.Values.ToArray();
            for (int i = 0; i < plugins.Length; ++i)
            {
                if (plugins[i].Loaded)
                {
                    plugins[i].OnUnLoad();
                }
            }
            this.plugins.Clear();
        }

        public bool UnLoadPlugin(string pluginName)
        {
            if (this.plugins.ContainsKey(pluginName))
            {
                this.plugins[pluginName].OnUnLoad();
                this.plugins.Remove(pluginName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsZipedPlugin(string fileExt)
        {
            return fileExt.ToLower() == ZIPED_PLUGIN || fileExt.ToLower() == ZIPED_MINENET_PLUGIN;
        }

        public bool IsPlugin(string fileExt)
        {
            return fileExt.ToLower() == PLUGIN_STRUCT || fileExt.ToLower() == PLUGIN_STRUCT_SHORT;
        }
    }
}
