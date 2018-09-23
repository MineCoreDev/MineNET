using MineNET.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MineNET.Plugins
{
    public class PluginManager : IDisposable
    {
        public const string Plugin = ".mnp";
        public const string Package = ".mnpkg";
        public const string Package_Sub = ".zip";
        public const string Library = ".mnlib";
        public const ApiVersion NowVersion = ApiVersion.Version1000;

        public List<IPlugin> Plugins { get; private set; } = new List<IPlugin>();
        public List<IPlugin> Libraries { get; private set; } = new List<IPlugin>();
        public List<IPlugin> Packages { get; private set; } = new List<IPlugin>();

        public PluginManager()
        {
            string path = Server.ExecutePath;
            string folder = path + "\\plugins";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            DirectoryInfo dir = new DirectoryInfo(folder);
            foreach (FileInfo file in dir.GetFiles())//Check Lib
            {
                if (file.Extension == PluginManager.Library)
                {
                    this.LoadLibrary(file.FullName);
                }
            }

            foreach (FileInfo file in dir.GetFiles())//Check Lib
            {
                if (file.Extension == PluginManager.Package || file.Extension == PluginManager.Package_Sub)
                {
                    this.LoadPackage(file.FullName);
                }
            }

            foreach (FileInfo file in dir.GetFiles())//Check Lib
            {
                if (file.Extension == PluginManager.Plugin)
                {
                    this.LoadPlugin(file.FullName);
                }
            }

            this.EnableAll();
        }

        #region Load Method
        public void LoadLibrary(string path)
        {
            Assembly asm = Assembly.LoadFile(path);
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsClass && t.IsPublic && !t.IsAbstract && t.GetInterface(typeof(IPlugin).FullName) != null)
                {
                    IPlugin plugin = (IPlugin) Activator.CreateInstance(t);

                    bool versionCheck = false;
                    if (plugin.Flag.HasFlag(PluginFlags.ApiVersionCheck))
                    {
                        if (plugin.ApiVersion == PluginManager.NowVersion)
                        {
                            versionCheck = true;
                        }
                    }
                    else
                    {
                        versionCheck = true;
                    }

                    if (plugin.Flag.HasFlag(PluginFlags.Library) && versionCheck)
                    {
                        Logger.Info("%server.plugin.load_library", plugin.Name);
                        plugin.OnLoad();
                        this.Libraries.Add(plugin);
                    }
                }
            }
        }

        private void LoadPlugin(string path)
        {
            Assembly asm = Assembly.LoadFile(path);

            foreach (Type t in asm.GetTypes())
            {
                if (t.IsClass && t.IsPublic && !t.IsAbstract && t.GetInterface(typeof(IPlugin).FullName) != null)
                {
                    IPlugin plugin = (IPlugin) Activator.CreateInstance(t);

                    bool versionCheck = false;
                    if (plugin.Flag.HasFlag(PluginFlags.ApiVersionCheck))
                    {
                        if (plugin.ApiVersion == PluginManager.NowVersion)
                        {
                            versionCheck = true;
                        }
                    }
                    else
                    {
                        versionCheck = true;
                    }

                    if (plugin.Flag.HasFlag(PluginFlags.Plugin) && versionCheck)
                    {
                        Logger.Info("%server.plugin.load_plugin", plugin.Name);
                        plugin.OnLoad();
                        this.Plugins.Add(plugin);
                    }
                }
            }
        }

        private void LoadPackage(string path)
        {
            Assembly asm = Assembly.LoadFile(path);
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsClass && t.IsPublic && !t.IsAbstract && t.GetInterface(typeof(IPlugin).FullName) != null)
                {
                    IPlugin plugin = (IPlugin) Activator.CreateInstance(t);

                    bool versionCheck = false;
                    if (plugin.Flag.HasFlag(PluginFlags.ApiVersionCheck))
                    {
                        if (plugin.ApiVersion == PluginManager.NowVersion)
                        {
                            versionCheck = true;
                        }
                    }
                    else
                    {
                        versionCheck = true;
                    }

                    if (plugin.Flag.HasFlag(PluginFlags.Package) && versionCheck)
                    {
                        Logger.Info("%server.plugin.load_package", plugin.Name);
                        plugin.OnLoad();
                        this.Packages.Add(plugin);
                    }
                }
            }
        }
        #endregion

        public void EnableAll()
        {
            foreach (IPlugin plugin in this.Libraries)
            {
                Logger.Info("%server.plugin.enable_library", plugin.Name);
                plugin.OnEnable();
            }

            foreach (IPlugin plugin in this.Plugins)
            {
                Logger.Info("%server.plugin.enable_plugin", plugin.Name);
                plugin.OnEnable();
            }

            foreach (IPlugin plugin in this.Packages)
            {
                Logger.Info("%server.plugin.enable_package", plugin.Name);
                plugin.OnEnable();
            }
        }

        public void DisableAll()
        {
            foreach (IPlugin plugin in this.Libraries)
            {
                Logger.Info("%server.plugin.disable_library", plugin.Name);
                plugin.OnDisable();
            }

            foreach (IPlugin plugin in this.Plugins)
            {
                Logger.Info("%server.plugin.disable_plugin", plugin.Name);
                plugin.OnDisable();
            }

            foreach (IPlugin plugin in this.Packages)
            {
                Logger.Info("%server.plugin.disable_package", plugin.Name);
                plugin.OnDisable();
            }
        }
        
        public void Dispose()
        {
            this.DisableAll();

            this.Plugins.Clear();
            this.Libraries.Clear();
            this.Packages.Clear();
            this.Plugins = null;
            this.Libraries = null;
            this.Packages = null;
        }
    }
}
