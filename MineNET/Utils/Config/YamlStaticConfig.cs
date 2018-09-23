using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using MineNET.Utils.Config.Yaml;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace MineNET.Utils.Config
{
    public abstract class YamlStaticConfig
    {
        public string filePath;

        public static T Load<T>(string filePath) where T : YamlStaticConfig
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader r = new StreamReader(filePath, false))
                    {
                        Deserializer s = (Deserializer) new DeserializerBuilder()
                            .Build();
                        YamlStaticConfig conv = s.Deserialize<T>(r);
                        conv.filePath = filePath;
                        return (T) Convert.ChangeType(conv, typeof(T));
                    }
                }
                else
                {
                    object ins = Activator.CreateInstance(typeof(T));
                    YamlStaticConfig conv = (YamlStaticConfig) ins;
                    conv.filePath = filePath;
                    conv.Save<T>();
                    return (T) conv;
                }
            }
            catch (SerializationException)
            {
                File.Move(filePath, filePath + ".old");
                return YamlStaticConfig.Load<T>(filePath);
            }
            catch (YamlException)
            {
                File.Move(filePath, filePath + ".old");
                return YamlStaticConfig.Load<T>(filePath);
            }
            catch (Exception e3)
            {
                throw e3;
            }
        }

        public void Save<T>()
        {
            try
            {
                if (File.Exists(this.filePath))
                {
                    StreamWriter w = new StreamWriter(this.filePath, false, Encoding.UTF8);
                    SerializerBuilder sb = new SerializerBuilder()
                        .EmitDefaults()
                        .WithTypeInspector(inner => new CommentTypeInspector(inner))
                        .WithEmissionPhaseObjectGraphVisitor(args => new CommentObjectGraphVisitor(args.InnerVisitor));
                    Serializer s = (Serializer) sb.Build();
                    s.Serialize(w, this, typeof(T));
                    w.Close();
                }
                else
                {
                    FileStream file = File.Create(this.filePath);
                    file.Close();
                    StreamWriter w = new StreamWriter(this.filePath, false, Encoding.UTF8);
                    SerializerBuilder sb = new SerializerBuilder()
                        .EmitDefaults()
                        .WithTypeInspector(inner => new CommentTypeInspector(inner))
                        .WithEmissionPhaseObjectGraphVisitor(args => new CommentObjectGraphVisitor(args.InnerVisitor));
                    Serializer s = (Serializer) sb.Build();
                    s.Serialize(w, this, typeof(T));
                    w.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
