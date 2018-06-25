using MineNET.Network.RakNetPackets;
using System.Net;

namespace MineNET.Events.NetworkEvents
{
    public class RakNetPacketSendEventArgs : NetworkEventArgs
    {
        public IPEndPoint EndPoint { get; }
        public RakNetPacket Packet { get; }
        public bool IsCancel { get; set; }

        public RakNetPacketSendEventArgs(IPEndPoint endPoint, RakNetPacket packet)
        {
            this.EndPoint = endPoint;
            this.Packet = packet;
        }
    }
}
