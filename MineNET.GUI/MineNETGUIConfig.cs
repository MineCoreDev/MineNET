using MineNET.Utils.Config;

namespace MineNET.GUI
{
    public class MineNETGUIConfig : YamlStaticConfig
    {
        [YamlDescription("app.config.checkVersion", typeof(MineNETGUIConfig), "MineNET.GUI.Resources.Lang.")]
        public bool CheckVersion { get; set; } = true;
        [YamlDescription("app.config.showNews", typeof(MineNETGUIConfig), "MineNET.GUI.Resources.Lang.")]
        public bool ShowNews { get; set; } = false;

        public bool ServerLoadStart { get; set; }
    }
}
