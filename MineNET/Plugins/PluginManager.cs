using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MineNET.Utils;

namespace MineNET.Plugins
{
    public class PluginManager
    {
        public List<IPlugin> plugins = new List<IPlugin>();

        public PluginManager()
        {
            this.Init();
            LoadPlugins();
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
            DirectoryInfo[] pluginFolder = dir.GetDirectories();
            for (int i = 0; i < pluginFolder.Length; ++i)
            {
                LoadPlugin(pluginFolder[i].Name, pluginFolder[i].FullName);
            }
            //TODO: zip & plugin & minenet LoadSystem...
        }

        public void LoadPlugin(string pluginFileName, string pluginPath)
        {
            string dllPath = $"{pluginPath}\\{pluginFileName}.dll";
            //string exePath = pluginPath + @"\\" + pluginFileName + ".exe";

            if (File.Exists(dllPath))
            {
                LoadDllPluginExecute(dllPath);
            }
            /*else if (File.Exists(exePath))
            {
                //
            }*/
        }

        void LoadDllPluginExecute(string dllPath)
        {
            string fileName = Path.GetFileName(dllPath);
            try
            {
                Assembly asm = Assembly.LoadFile(dllPath);
                Type[] types = asm.GetTypes();
                int count = 0;
                bool isPlugin = false;
                for (int i = 0; i < types.Length; ++i)
                {
                    Type t = types[i];
                    PluginAttribute att = t.GetCustomAttribute<PluginAttribute>();
                    if (att != null && t.GetInterface("IPlugin", false) != null)
                    {
                        IPlugin plugin = (IPlugin) Activator.CreateInstance(t);
                        att.pluginName = fileName;
                        att.pluginPath = dllPath;
                        plugin.Init(att);
                        Logger.Info("%server_plugin_load", fileName);
                        plugin.OnLoad();
                        this.plugins.Add(plugin);
                        isPlugin = true;
                        count++;
                    }
                }

                if (count != 1)
                {
                    throw new PluginException();
                }

                if (!isPlugin)
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
    }
}
