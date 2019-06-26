using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MineNET.IO;

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
            string folder = path + "/plugins";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            DirectoryInfo dir = new DirectoryInfo(folder);
            foreach (FileInfo file in dir.GetFiles()) //Check Lib
            {
                if (file.Extension == Library)
                {
                    try
                    {
                        this.LoadLibrary(file.FullName);
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("%server.plugin.load_error", file.Name);
                        Logger.Error(e);
                    }
                }
            }

            foreach (FileInfo file in dir.GetFiles()) //Check Lib
            {
                if (file.Extension == Package || file.Extension == Package_Sub)
                {
                    try
                    {
                        this.LoadPackage(file.FullName);
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("%server.plugin.load_error", file.Name);
                        Logger.Error(e);
                    }
                }
            }

            foreach (FileInfo file in dir.GetFiles()) //Check Lib
            {
                if (file.Extension == Plugin)
                {
                    try
                    {
                        this.LoadPlugin(file.FullName);
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("%server.plugin.load_error", file.Name);
                        Logger.Error(e);
                    }
                }
            }
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
                        if (plugin.ApiVersion == NowVersion)
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
                        if (plugin.ApiVersion == NowVersion)
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
                        if (plugin.ApiVersion == NowVersion)
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
                try
                {
                    Logger.Info("%server.plugin.enable_library", plugin.Name);
                    plugin.OnEnable();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            foreach (IPlugin plugin in this.Plugins)
            {
                try
                {
                    Logger.Info("%server.plugin.enable_plugin", plugin.Name);
                    plugin.OnEnable();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            foreach (IPlugin plugin in this.Packages)
            {
                try
                {
                    Logger.Info("%server.plugin.enable_package", plugin.Name);
                    plugin.OnEnable();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        public void DisableAll()
        {
            foreach (IPlugin plugin in this.Libraries)
            {
                try
                {
                    Logger.Info("%server.plugin.disable_library", plugin.Name);
                    plugin.OnDisable();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            foreach (IPlugin plugin in this.Plugins)
            {
                try
                {
                    Logger.Info("%server.plugin.disable_plugin", plugin.Name);
                    plugin.OnDisable();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            foreach (IPlugin plugin in this.Packages)
            {
                try
                {
                    Logger.Info("%server.plugin.disable_package", plugin.Name);
                    plugin.OnDisable();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
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