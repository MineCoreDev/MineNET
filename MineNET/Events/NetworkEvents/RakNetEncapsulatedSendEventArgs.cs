using MineNET.Network;
using MineNET.Network.RakNetPackets;

namespace MineNET.Events.NetworkEvents
{
    public class RakNetEncapsulatedSendEventArgs : NetworkEventArgs, ICancelable
    {
        public NetworkSession Session { get; }
        public EncapsulatedPacket Packet { get; set; }
        public bool IsCancel { get; set; }

        public RakNetEncapsulatedSendEventArgs(NetworkSession session, EncapsulatedPacket packet)
        {
            this.Session = session;
            this.Packet = packet;
        }
    }
}
