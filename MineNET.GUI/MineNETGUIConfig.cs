using MineNET.Utils.Config;

namespace MineNET.GUI
{
    public class MineNETGUIConfig : YamlStaticConfig
    {
        public bool CheckVersion { get; set; } = true;
        public bool CheckNews { get; set; } = true;

        public bool ServerLoadStart { get; set; }

    }
}
