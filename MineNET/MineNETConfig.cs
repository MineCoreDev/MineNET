using MineNET.Utils.Config;

namespace MineNET
{
    public class MineNETConfig : YamlStaticConfig
    {
        public string Language { get; set; } = "ja_JP";

        public bool EnableConsoleInput { get; set; } = true;
        public bool EnableConsoleOutput { get; set; } = true;

        public bool EnableDebugLog { get; set; } = true;

        public string ServerStopText { get; set; } = "disconnect.closed";
    }
}
