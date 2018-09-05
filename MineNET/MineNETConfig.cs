using MineNET.Utils.Config;

namespace MineNET
{
    public class MineNETConfig : YamlStaticConfig
    {
        public string Language { get; set; } = "ja_JP";
        public bool PacketDebug { get; set; } = false;
        public bool ClockDelayDebug { get; set; } = false;
        public bool TraceLogDisable { get; set; } = true;
    }
}
