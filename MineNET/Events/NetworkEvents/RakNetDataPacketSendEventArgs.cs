using MineNET.Network;
using MineNET.Network.RakNetPackets;

namespace MineNET.Events.NetworkEvents
{
    public class RakNetDataPacketSendEventArgs : NetworkEventArgs, ICancelable
    {
        public NetworkSession Session { get; }
        public DataPacket Packet { get; set; }
        public bool IsCancel { get; set; }

        public RakNetDataPacketSendEventArgs(NetworkSession session, DataPacket packet)
        {
            this.Session = session;
            this.Packet = packet;
        }
    }
}
