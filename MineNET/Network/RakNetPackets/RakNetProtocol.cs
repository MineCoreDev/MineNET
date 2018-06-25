namespace MineNET.Network.RakNetPackets
{
    public static class RakNetProtocol
    {
        public static readonly byte[] Magic = new byte[]
        {

            0x00,

            0xff,

            0xff,

            0x00,

            0xfe,

            0xfe,

            0xfe,

            0xfe,

            0xfd,

            0xfd,

            0xfd,

            0xfd,

            0x12,

            0x34,

            0x56,

            0x78

        };

        public const int FlagNormal = 0;
        public const int FlagImmediate = 1;

        public const byte OnlinePing = 0x00;
        public const byte OfflinePing = 0x01;

        public const byte OnlinePong = 0x03;

        public const byte OpenConnectingRequest1 = 0x05;
        public const byte OpenConnectingReply1 = 0x06;
        public const byte OpenConnectingRequest2 = 0x07;
        public const byte OpenConnectingReply2 = 0x08;
        public const byte ClientConnectDataPacket = 0x09;
        public const byte ServerHandShakeDataPacket = 0x10;

        public const byte ClientHandShakeDataPacket = 0x13;

        public const byte ClientDisconnectDataPacket = 0x15;

        public const byte OfflinePong = 0x1c;

        public const byte DataPacket0 = 0x80;
        public const byte DataPacket1 = 0x81;
        public const byte DataPacket2 = 0x82;
        public const byte DataPacket3 = 0x83;
        public const byte DataPacket4 = 0x84;
        public const byte DataPacket5 = 0x85;
        public const byte DataPacket6 = 0x86;
        public const byte DataPacket7 = 0x87;
        public const byte DataPacket8 = 0x88;
        public const byte DataPacket9 = 0x89;
        public const byte DataPacketA = 0x8a;
        public const byte DataPacketB = 0x8b;
        public const byte DataPacketC = 0x8c;
        public const byte DataPacketD = 0x8d;
        public const byte DataPacketE = 0x8e;
        public const byte DataPacketF = 0x8f;

        public const byte NackPacket = 0xa0;
        public const byte AckPacket = 0xc0;

        public const byte BatchPacket = 0xfe;
    }
}
