using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineNET.Utils.Config.Yaml;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace MineNET.Utils.Config
{
    public class YamlConfig
    {
        public static YamlConfig Load(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader r = new StreamReader(filePath, Encoding.UTF8, false))
                    {
                        Deserializer s = new DeserializerBuilder()
                            .Build();
                        YamlConfig conv = new YamlConfig();
                        conv.Root = s.Deserialize<Dictionary<string, object>>(r);
                        conv.FilePath = filePath;
                        return (YamlConfig) Convert.ChangeType(conv, typeof(YamlConfig));
                    }
                }
                else
                {
                    YamlConfig conv = new YamlConfig();
                    conv.FilePath = filePath;
                    conv.Save();
                    return conv;
                }
            }
            catch (YamlException e)
            {
                throw e;
            }
        }

        [YamlIgnore]
        public string FilePath { get; private set; }
        public Dictionary<string, object> Root { get; set; } = new Dictionary<string, object>();

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
                s.Serialize(w, this.Root, typeof(Dictionary<string, object>));
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
                s.Serialize(w, this.Root, typeof(Dictionary<string, object>));
                w.Close();
            }
        }

        public bool ContainsKey(string name)
        {
            return this.Root.ContainsKey(name);
        }

        public bool ContainsValue(object value)
        {
            return this.Root.ContainsValue(value);
        }

        public void Set(string name, object value)
        {
            this.Root[name] = value;
        }

        public object Get(string name)
        {
            return this.Root[name];
        }

        public byte GetByte(string name)
        {
            return (byte) this.Get(name);
        }

        public short GetShort(string name)
        {
            return (short) this.Get(name);
        }

        public int GetInt(string name)
        {
            return (int) this.Get(name);
        }

        public long GetLong(string name)
        {
            return (long) this.Get(name);
        }

        public float GetFloat(string name)
        {
            return (float) this.Get(name);
        }

        public double GetDouble(string name)
        {
            return (double) this.Get(name);
        }

        public string GetString(string name)
        {
            return (string) this.Get(name);
        }

        public bool GetBool(string name)
        {
            return (bool) this.Get(name);
        }

        public Dictionary<string, object> GetAll()
        {
            return this.Root;
        }

        public void Remove(string name)
        {
            this.Root.Remove(name);
        }

        public void RemoveAll()
        {
            this.Root.Clear();
        }
    }
}
