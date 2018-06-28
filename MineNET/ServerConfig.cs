using MineNET.Utils.Config;

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

        public int MaxChunkRadius { get; set; } = 8;
    }
}
