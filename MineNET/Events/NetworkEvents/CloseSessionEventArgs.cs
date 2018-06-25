using MineNET.Network;
using System.Net;

namespace MineNET.Events.NetworkEvents
{
    public class CloseSessionEventArgs : NetworkEventArgs
    {
        public IPEndPoint EndPoint { get; }
        public NetworkSession Session { get; set; }

        public CloseSessionEventArgs(IPEndPoint endPoint, NetworkSession session)
        {
            this.EndPoint = endPoint;
            this.Session = session;
        }
    }
}
