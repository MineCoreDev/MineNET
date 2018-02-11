using MineNET.Utils.Config;

namespace MineNET
{
    public sealed class ServerConfig : YamlStaticConfig
    {
        [YamlDescription("ServerName - サーバー名")]
        public string ServerMotd
        {
            get;
            set;
        } = "MineNETServer";

        [YamlDescription("ServerPortNumber <1 ~ 65535> - サーバーポート番号 <1 ~ 65535>")]
        public ushort ServerPort
        {
            get;
            set;
        } = 19132;

        public int MaxPlayer
        {
            get;
            set;
        } = 25;

        public string WorldGameMode
        {
            get;
            set;
        } = "Survival";

        public string MainWorldName
        {
            get;
            set;
        } = "World";

        public string[] LoadWorldNames
        {
            get;
            set;
        } = new string[0];
        /* = new string[]
        {
            "Nether",
            "End"
        };*/

        public string MainWorldGenerator
        {
            get;
            set;
        } = "Default";

        public bool GenerateNether
        {
            get;
            set;
        } = true;

        public bool GenerateEnd
        {
            get;
            set;
        } = true;

        public bool OnlineMode
        {
            get;
            set;
        } = false;
    }
}
