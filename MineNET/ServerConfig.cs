using MineNET.Utils.Config;
using MineNET.Worlds;

namespace MineNET
{
    public class ServerConfig : YamlStaticConfig
    {
        [YamlDescription("%server.config.serverMotd")]
        public string ServerMotd { get; set; } = "MineNETServer";

        [YamlDescription("%server.config.port")]
        public ushort ServerPort { get; set; } = 19132;

        [YamlDescription("%server.config.maxPlayer")]
        public int MaxPlayers { get; set; } = 20;

        [YamlDescription("%server.config.gameMode")]
        public GameMode GameMode { get; set; } = GameMode.Survival;

        [YamlDescription("%server.config.difficulty")]
        public int Difficulty { get; set; } = 1;

        [YamlDescription("%server.config.mainWorldName")]
        public string MainWorldName { get; set; } = "World";

        [YamlDescription("%server.config.loadWorldNames")]
        public string[] LoadWorldNames { get; set; } = new string[0];
        /* = new string[]
        {
            "Nether",
            "End"
        };*/

        [YamlDescription("%server.config.spawnProtection")]
        public int SpawnProtection { get; set; } = 16;

        [YamlDescription("%server.config.mainWorldGenerator")]
        public string WorldGenerator { get; set; } = "Default";

        [YamlDescription("%server.config.flatGeneratorOptions")]
        public string FlatGeneratorOptions { get; set; } = "0;7,2*3,2";

        [YamlDescription("%server.config.generateNether")]
        public bool GenerateNether { get; set; } = false;

        [YamlDescription("%server.config.generateEnd")]
        public bool GenerateEnd { get; set; } = false;

        [YamlDescription("%server.config.maxViewDistance")]
        public int MaxViewDistance { get; set; } = 5;

        [YamlDescription("%server.config.onlineMode")]
        public bool OnlineMode { get; set; } = false;

        [YamlDescription("%server.config.whitelist")]
        public bool WhiteList { get; set; } = false;

        [YamlDescription("%server.config.hardcore")]
        public bool Hardcore { get; set; } = false;

        [YamlDescription("%server.config.pvp")]
        public bool Pvp { get; set; } = true;
    }
}
