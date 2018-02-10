namespace MineNET.Network.Packets
{
    public class PlayStatusPacket : DataPacket
    {
        public const byte ID = ProtocolInfo.PLAY_STATUS_PACKET;

        public const int LOGIN_SUCCESS = 0;
        public const int LOGIN_FAILED_CLIENT = 1;
        public const int LOGIN_FAILED_SERVER = 2;
        public const int PLAYER_SPAWN = 3;
        public const int LOGIN_FAILED_INVALID_TENANT = 4;
        public const int LOGIN_FAILED_VANILLA_EDU = 5;
        public const int LOGIN_FAILED_EDU_VANILLA = 6;

        private int status;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteInt(status);
        }
    }
}
