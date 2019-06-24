using System.Net;

namespace MineNET.Network.RakNet.Interfaces
{
    public interface IRakNetServer
    {
        bool Start(IPEndPoint endPoint);
        bool Stop();

        void AddSession(IRakNetSession session);
        void RemoveSession(IRakNetSession session);
        IRakNetSession GetSession(IPEndPoint endPoint);
        IRakNetSession[] GetSessions();
    }
}