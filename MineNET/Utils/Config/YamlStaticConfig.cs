using System;
using System.Text;
using System.IO;

using YamlDotNet.Serialization;

namespace MineNET.Utils.Config
{
    public abstract class YamlStaticConfig : IConfig
    {
        public string filePath;

        public static T Load<T>(string filePath) where T : YamlStaticConfig
        {
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath, false))
                {
                    Deserializer s = new Deserializer();
                    var conv = (YamlStaticConfig)s.Deserialize<T>(r);
                    conv.filePath = filePath;
                    return (T)Convert.ChangeType(conv, typeof(T));
                }
            }
            else
            {
                var ins = Activator.CreateInstance(typeof(T));
                var conv = (YamlStaticConfig)ins;
                conv.filePath = filePath;
                conv.Save<T>();
                return (T)conv;
            }
        }

        public void Save<T>()
        {
            if (File.Exists(this.filePath))
            {
                StreamWriter w = new StreamWriter(this.filePath, false, Encoding.UTF8);
                Serializer s = new Serializer();
                s.Serialize(w, this, typeof(T));
                w.Close();
            }
            else
            {
                var file = File.Create(this.filePath);
                file.Close();
                StreamWriter w = new StreamWriter(this.filePath, false, Encoding.UTF8);
                Serializer s = new Serializer();
                s.Serialize(w, this, typeof(T));
                w.Close();
            }
        }
    }
}
