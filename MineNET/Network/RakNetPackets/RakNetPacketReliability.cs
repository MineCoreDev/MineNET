namespace MineNET.Network.RakNetPackets
{
    /// <summary>
    /// from https://github.com/OculusVR/RakNet/blob/master/Source/PacketPriority.h
    /// </summary>
    /// 
    public static class RakNetPacketReliability
    {
        public const int UNRELIABLE = 0;
        public const int UNRELIABLE_SEQUENCED = 1;
        public const int RELIABLE = 2;
        public const int RELIABLE_ORDERED = 3;
        public const int RELIABLE_SEQUENCED = 4;
        public const int UNRELIABLE_WITH_ACK_RECEIPT = 5;
        public const int RELIABLE_WITH_ACK_RECEIPT = 6;
        public const int RELIABLE_ORDERED_WITH_ACK_RECEIPT = 7;

        public static bool IsReliable(int reliability)
        {
            return reliability == RELIABLE ||
                   reliability == RELIABLE_ORDERED ||
                   reliability == RELIABLE_SEQUENCED ||
                   reliability == RELIABLE_ORDERED_WITH_ACK_RECEIPT ||
                   reliability == RELIABLE_WITH_ACK_RECEIPT;
        }

        public static bool IsSequenced(int reliability)
        {
            return reliability == UNRELIABLE_SEQUENCED ||
                   reliability == RELIABLE_SEQUENCED;
        }

        public static bool IsOrdered(int reliability)
        {
            return reliability == RELIABLE_ORDERED ||
                   reliability == RELIABLE_ORDERED_WITH_ACK_RECEIPT;
        }

        public static bool IsSequencedOrOrdered(int reliability)
        {
            return reliability == UNRELIABLE_SEQUENCED ||
                   reliability == RELIABLE_ORDERED ||
                   reliability == RELIABLE_SEQUENCED ||
                   reliability == RELIABLE_ORDERED_WITH_ACK_RECEIPT;
        }
    }
}
