using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineNET.Utils.Config.Yaml;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace MineNET.Utils.Config
{
    public class YamlConfig : IConfig
    {
        public static YamlConfig Load(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader r = new StreamReader(filePath, false))
                    {
                        Deserializer s = new DeserializerBuilder()
                            .Build();
                        YamlConfig conv = s.Deserialize<YamlConfig>(r);
                        conv.FilePath = filePath;
                        return (YamlConfig) Convert.ChangeType(conv, typeof(YamlConfig));
                    }
                }
                else
                {
                    object ins = Activator.CreateInstance(typeof(YamlConfig));
                    YamlConfig conv = (YamlConfig) ins;
                    conv.FilePath = filePath;
                    conv.Save();
                    return (YamlConfig) conv;
                }
            }
            catch (YamlException e)
            {
                Logger.Error(e);
                Logger.Error("%config_error");
                Logger.Notice("%config_error2");
                throw new ServerException();
            }
        }

        [YamlIgnore]
        public string FilePath { get; private set; }
        public Dictionary<string, object> Datas { get; set; } = new Dictionary<string, object>();

        public void Save()
        {
            if (File.Exists(this.FilePath))
            {
                StreamWriter w = new StreamWriter(this.FilePath, false, Encoding.UTF8);
                SerializerBuilder sb = new SerializerBuilder()
                    .EmitDefaults()
                    .WithTypeInspector(inner => new CommentTypeInspector(inner))
                    .WithEmissionPhaseObjectGraphVisitor(args => new CommentObjectGraphVisitor(args.InnerVisitor));
                Serializer s = sb.Build();
                s.Serialize(w, this, typeof(YamlConfig));
                w.Close();
            }
            else
            {
                FileStream file = File.Create(this.FilePath);
                file.Close();
                StreamWriter w = new StreamWriter(this.FilePath, false, Encoding.UTF8);
                SerializerBuilder sb = new SerializerBuilder()
                    .EmitDefaults()
                    .WithTypeInspector(inner => new CommentTypeInspector(inner))
                    .WithEmissionPhaseObjectGraphVisitor(args => new CommentObjectGraphVisitor(args.InnerVisitor));
                Serializer s = sb.Build();
                s.Serialize(w, this, typeof(YamlConfig));
                w.Close();
            }
        }
    }
}
