using MineNET.GUI.Data;
using MineNET.Utils.Config;

namespace MineNET.GUI.Config
{
    public class MineNETGUIConfig : YamlStaticConfig
    {
        [YamlDescription("%config_outputOption", typeof(MineNETGUIConfig), "MineNET.GUI.Resources.Lang.")]
        public OutputOption OutputOption { get; set; } = new OutputOption();
        public int InputModeIndex { get; set; } = 0;
    }
}
