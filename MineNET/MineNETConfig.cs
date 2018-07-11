using MineNET.IO;
using MineNET.Utils.Config;

namespace MineNET
{
    public class MineNETConfig : YamlStaticConfig
    {
        public string Language { get; set; } = "ja_JP";

        public LoggerLevel ShowLogLevel { get; set; } = LoggerLevel.Info;
    }
}
