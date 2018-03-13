using System.IO;
using YamlDotNet.RepresentationModel;

namespace MineNET.Utils.Config
{
    public class YamlConfig : IConfig
    {
        public static YamlConfig Load(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                YamlConfig config = new YamlConfig();
                config.FilePath = filePath;
                config.stream.Load(reader);
                return config;
            }

        }

        private YamlStream stream;
        public string FilePath { get; private set; }

        public YamlConfig()
        {
            stream = new YamlStream();
        }

        public YamlDocument Read(int index)
        {
            return stream.Documents[index];
        }

        public void Write(YamlDocument doc)
        {
            stream.Documents.Add(doc);
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(this.FilePath))
            {
                stream.Save(writer);
            }
        }
    }
}
