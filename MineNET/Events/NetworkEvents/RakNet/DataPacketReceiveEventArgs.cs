using MineNET.Network;
using MineNET.Network.RakNetPackets;

namespace MineNET.Events.NetworkEvents.RakNet
{
    public class RakNetDataPacketReceiveEventArgs : NetworkEventArgs, ICancelable
    {
        public NetworkSession Session { get; }
        public DataPacket Packet { get; set; }
        public bool IsCancel { get; set; }

        public RakNetDataPacketReceiveEventArgs(NetworkSession session, DataPacket packet)
        {
            this.Session = session;
            this.Packet = packet;
        }
    }
}
