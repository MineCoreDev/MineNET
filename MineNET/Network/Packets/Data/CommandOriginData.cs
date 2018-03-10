using System;

namespace MineNET.Network.Packets.Data
{
    public class CommandOriginData
    {
        public const int ORIGIN_PLAYER = 0;
        public const int ORIGIN_BLOCK = 1;
        public const int ORIGIN_MINECART_BLOCK = 2;
        public const int ORIGIN_DEV_CONSOLE = 3;
        public const int ORIGIN_TEST = 4;
        public const int ORIGIN_AUTOMATION_PLAYER = 5;
        public const int ORIGIN_CLIENT_AUTOMATION = 6;
        public const int ORIGIN_DEDICATED_SERVER = 7;
        public const int ORIGIN_ENTITY = 8;
        public const int ORIGIN_VIRTUAL = 9;
        public const int ORIGIN_GAME_ARGUMENT = 10;
        public const int ORIGIN_ENTITY_SERVER = 11;

        public long Type { get; set; }
        public Guid Guid { get; set; }
        public string RequestId { get; set; }
        public long Unknown { get; set; }
    }
}
