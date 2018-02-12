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
