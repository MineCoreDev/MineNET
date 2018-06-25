using MineNET.Network;
using System.Net;

namespace MineNET.Events.NetworkEvents
{
    public class CreateSessionEventArgs : NetworkEventArgs, ICancelable
    {
        public IPEndPoint EndPoint { get; }
        public NetworkSession Session { get; set; }
        public bool IsCancel { get; set; }

        public CreateSessionEventArgs(IPEndPoint endPoint, NetworkSession session)
        {
            this.Network = Server.Instance.Network;

            this.EndPoint = endPoint;
            this.Session = session;
        }
    }
}
