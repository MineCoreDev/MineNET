namespace MineNET.RakNet.Packets
{
    /// <summary>
    /// from https://github.com/OculusVR/RakNet/blob/master/Source/PacketPriority.h
    /// </summary>
    /// 
    public class PacketReliability
    {
        public const int UNRELIABLE = 0;
        public const int UNRELIABLE_SEQUENCED = 1;
        public const int RELIABLE = 2;
        public const int RELIABLE_ORDERED = 3;
        public const int RELIABLE_SEQUENCED = 4;
        public const int UNRELIABLE_WITH_ACK_RECEIPT = 5;
        public const int RELIABLE_WITH_ACK_RECEIPT = 6;
        public const int RELIABLE_ORDERED_WITH_ACK_RECEIPT = 7;
    }
}
