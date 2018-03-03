using MineNET.Utils.Config;

namespace MineNET
{
    public sealed class ServerConfig : YamlStaticConfig
    {
        [YamlDescription("%server_config_serverMotd")]
        public string ServerMotd { get; set; } = "MineNETServer";

        [YamlDescription("%server_config_port")]
        public ushort ServerPort { get; set; } = 19132;

        [YamlDescription("%server_config_maxPlayer")]
        public int MaxPlayers { get; set; } = 20;

        [YamlDescription("%server_config_worldGameMode")]
        public string GameMode { get; set; } = "Survival";

        [YamlDescription("%server_config_mainWorldName")]
        public string MainWorldName { get; set; } = "World";

        [YamlDescription("%server_config_loadWorldNames")]
        public string[] LoadWorldNames { get; set; } = new string[0];
        /* = new string[]
        {
            "Nether",
            "End"
        };*/

        [YamlDescription("%server_config_mainWorldGenerator")]
        public string WorldGenerator { get; set; } = "Default";

        [YamlDescription("%server_config_generateNether")]
        public bool GenerateNether { get; set; } = false;

        [YamlDescription("%server_config_generateEnd")]
        public bool GenerateEnd { get; set; } = false;

        public int ViewDistance { get; set; } = 5;

        [YamlDescription("%server_config_onlineMode")]
        public bool OnlineMode { get; set; } = false;

        [YamlDescription("ホワイトリスト")]
        public bool WhiteList { get; set; } = false;

        [YamlDescription("ハードコア")]
        public bool Hardcore { get; set; } = false;

        [YamlDescription("pvp")]
        public bool Pvp { get; set; } = true;

        [YamlDescription("難易度")]
        public int Difficulty { get; set; } = 1;
    }
}
