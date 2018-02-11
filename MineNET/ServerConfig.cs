using MineNET.Utils.Config;

namespace MineNET
{
    public class ServerConfig : YamlStaticConfig
    {
        public string ServerMotd
        {
            get;
            set;
        } = "MineNETServer";

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
    }
}
