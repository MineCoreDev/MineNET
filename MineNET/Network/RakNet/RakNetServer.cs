using System;
using System.Net;
using MineNET.Network.RakNet.Interfaces;

namespace MineNET.Network.RakNet
{
    public class RakNetServer : IRakNetServer
    {
        public bool Start(IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }

        public void AddSession(IRakNetSession session)
        {
            throw new NotImplementedException();
        }

        public void RemoveSession(IRakNetSession session)
        {
            throw new NotImplementedException();
        }

        public IRakNetSession GetSession(IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public IRakNetSession[] GetSessions()
        {
            throw new NotImplementedException();
        }
    }
}