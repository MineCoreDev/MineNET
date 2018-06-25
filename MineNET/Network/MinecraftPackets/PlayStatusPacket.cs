namespace MineNET.Network.MinecraftPackets
{
    public class PlayStatusPacket : MinecraftPacket
    {
        public const int LOGIN_SUCCESS = 0;
        public const int LOGIN_FAILED_CLIENT = 1;
        public const int LOGIN_FAILED_SERVER = 2;
        public const int PLAYER_SPAWN = 3;
        public const int LOGIN_FAILED_INVALID_TENANT = 4;
        public const int LOGIN_FAILED_VANILLA_EDU = 5;
        public const int LOGIN_FAILED_EDU_VANILLA = 6;
        public const int LOGIN_FAILED_SERVER_FULL = 7;

        public override byte PacketID { get; } = MinecraftProtocol.PLAY_STATUS_PACKET;

        public int Status { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteInt(this.Status);
        }
    }
}
