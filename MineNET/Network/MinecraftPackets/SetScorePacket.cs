namespace MineNET.Network.MinecraftPackets
{
    public class SetScorePacket : MinecraftPacket
    {
        public const byte TYPE_MODIFY_SCORE = 0;
        public const byte TYPE_RESET_SCORE = 1;

        public override byte PacketID { get; } = MinecraftProtocol.SET_SCORE_PACKET;

        public byte Type { get; set; }
        //public ScorePacketEntry Entry { get; set; } 
    }
}
