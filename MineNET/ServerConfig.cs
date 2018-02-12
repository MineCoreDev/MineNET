using MineNET.Utils.Config;

namespace MineNET
{
    public sealed class ServerConfig : YamlStaticConfig
    {
        [YamlDescription("%server_config_serverMotd")]
        public string ServerMotd
        {
            get;
            set;
        } = "MineNETServer";

        [YamlDescription("%server_config_port")]
        public ushort ServerPort
        {
            get;
            set;
        } = 19132;

        [YamlDescription("%server_config_maxPlayer")]
        public int MaxPlayer
        {
            get;
            set;
        } = 25;

        [YamlDescription("%server_config_worldGameMode")]
        public string WorldGameMode
        {
            get;
            set;
        } = "Survival";

        [YamlDescription("%server_config_mainWorldName")]
        public string MainWorldName
        {
            get;
            set;
        } = "World";

        [YamlDescription("%server_config_loadWorldNames")]
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

        [YamlDescription("%server_config_mainWorldGenerator")]
        public string MainWorldGenerator
        {
            get;
            set;
        } = "Default";

        [YamlDescription("%server_config_generateNether")]
        public bool GenerateNether
        {
            get;
            set;
        } = true;

        [YamlDescription("%server_config_generateEnd")]
        public bool GenerateEnd
        {
            get;
            set;
        } = true;

        [YamlDescription("%server_config_onlineMode")]
        public bool OnlineMode
        {
            get;
            set;
        } = false;
    }
}
