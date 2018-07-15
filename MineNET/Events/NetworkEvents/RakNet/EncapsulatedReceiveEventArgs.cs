using MineNET.Network;
using MineNET.Network.RakNetPackets;

namespace MineNET.Events.NetworkEvents.RakNet
{
    public class RakNetEncapsulatedReceiveEventArgs : NetworkEventArgs, ICancelable
    {
        public NetworkSession Session { get; }
        public EncapsulatedPacket Packet { get; set; }
        public bool IsCancel { get; set; }

        public RakNetEncapsulatedReceiveEventArgs(NetworkSession session, EncapsulatedPacket packet)
        {
            this.Session = session;
            this.Packet = packet;
        }
    }
}
