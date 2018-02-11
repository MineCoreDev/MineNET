using MineNET.Utils.Config;

namespace MineNET
{
    public class MineNETConfig : YamlStaticConfig
    {
        public bool EnableConsoleInput
        {
            get;
            set;
        } = true;

        public bool EnableConsoleOutput
        {
            get;
            set;
        } = true;
    }
}
