﻿using MineNET.Utils.Config;
using System;
using System.IO;

namespace MineNET.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public abstract string Name { get; }
        public virtual string Description { get; } = "";
        public virtual string Version { get; } = "1.0.0.0";
        public virtual ApiVersion ApiVersion { get; } = ApiVersion.Version_1_0_0_0;
        public virtual PluginFlags Flag { get; } = PluginFlags.Package;
        public virtual string[] PremisePlugins { get; set; } = null;

        public YamlConfig Config { get; private set; }

        public virtual void OnDisable()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnError(Exception e)
        {
        }

        public virtual void OnLoad()
        {
        }

        public string GetPluginPath()
        {
            return Path.GetDirectoryName(this.GetPluginLocation());
        }

        public string GetPluginLocation()
        {
            return this.GetType().Assembly.Location;
        }

        public string GetPluginFileName()
        {
            return Path.GetFileName(this.GetPluginLocation());
        }
    }
}
