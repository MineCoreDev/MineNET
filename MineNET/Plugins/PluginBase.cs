using MineNET.Utils.Config;
using System;
using System.IO;
using System.Reflection;
using MineNET.Events;

namespace MineNET.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public abstract string Name { get; }
        public virtual string Description { get; } = "";
        public virtual string Version { get; } = "1.0.0.0";
        public virtual ApiVersion ApiVersion { get; } = ApiVersion.Version1100;
        public virtual PluginFlags Flag { get; } = PluginFlags.Package;
        public virtual string[] PremisePlugins { get; set; } = null;

        public YamlConfig Config { get; private set; } //TODO:

        public PluginBase()
        {
            string saveFolder = this.GetPluginPath() + "/" + this.Name;
            string saveFile = saveFolder + "/config.yml";
            if (this.Flag.HasFlag(PluginFlags.GenerateConfig))
            {
                if (!Directory.Exists(saveFolder))
                {
                    Directory.CreateDirectory(saveFolder);
                }

                this.Config = YamlConfig.Load(saveFile);
            }

            // Danger!! 黒魔術コード (クソ遅い)
            MethodInfo[] methods = GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                // Attributeを探す
                Attribute att = method.GetCustomAttribute(typeof(EventHandlerAttribute));
                if (att != null)
                {
                    // パラメータから整合性をチェック
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length > 1)
                    {
                        // パラメータを取得
                        Type args = parameters[1].ParameterType;

                        // イベントマネージャーから探す
                        EventManager eventManager = Server.Instance.Event;
                        PropertyInfo[] properties = eventManager.GetType().GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            // 全てのイベントを取得
                            EventInfo[] events = property.PropertyType.GetEvents();
                            foreach (EventInfo ev in events)
                            {
                                // 型情報がパラメータと一致するものを探す
                                if (ev.EventHandlerType.GenericTypeArguments.Length > 0 &&
                                    ev.EventHandlerType.GenericTypeArguments[0] == args)
                                    // イベントを動的追加
                                    ev.AddEventHandler(property.GetMethod.Invoke(eventManager, new object[0]),
                                        Delegate.CreateDelegate(ev.EventHandlerType, this, method));
                            }
                        }
                    }
                }
            }
        }

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