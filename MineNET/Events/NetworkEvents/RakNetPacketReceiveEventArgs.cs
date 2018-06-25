using MineNET.Network.RakNetPackets;
using System.Net;

namespace MineNET.Events.NetworkEvents
{
    public class RakNetPacketReceiveEventArgs : NetworkEventArgs, ICancelable
    {
        public IPEndPoint EndPoint { get; }
        public RakNetPacket Packet { get; }
        public bool IsCancel { get; set; }

        public RakNetPacketReceiveEventArgs(IPEndPoint endPoint, RakNetPacket packet)
        {
            this.EndPoint = endPoint;
            this.Packet = packet;
        }
    }
}
