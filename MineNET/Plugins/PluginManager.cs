using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MineNET.Utils;

namespace MineNET.Plugins
{
    public class PluginManager
    {
        public List<IPlugin> plugins;

        public PluginManager()
        {
            this.Init();
            LoadPlugins();
        }

        public void Init()
        {
            string path = Server.ExecutePath + @"\\plugins";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void LoadPlugins()
        {
            string path = Server.ExecutePath + @"\\plugins";
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] pluginFolder = dir.GetDirectories();
            for (int i = 0; i < pluginFolder.Length; ++i)
            {
                LoadPlugin(pluginFolder[i].Name, pluginFolder[i].FullName);
            }
        }

        public void LoadPlugin(string pluginFileName, string pluginPath)
        {
            string dllPath = pluginPath + @"\\" + pluginFileName + ".dll";
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
                bool isPlugin = false;
                for (int i = 0; i < types.Length; ++i)
                {
                    Type t = types[i];
                    PluginAttribute att = t.GetCustomAttribute<PluginAttribute>();
                    if (att != null && t is IPlugin)
                    {
                        IPlugin plugin = (IPlugin) Activator.CreateInstance(t);
                        att.FileName = fileName;
                        att.PluginPath = dllPath;
                        plugin.Init(att);
                        plugin.OnLoad();
                        plugins.Add(plugin);
                        isPlugin = true;
                    }
                }

                if (!isPlugin)
                {
                    Logger.Error("%server_plugin_notPlugin", fileName);
                }
                else
                {
                    Logger.Info("%server_plugin_load", fileName);
                }
            }
            catch
            {
                Logger.Info("%server_plugin_loadError", fileName);
            }
        }
    }
}
