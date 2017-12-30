using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using MineCraftPENetwork.Protocol;

namespace MineCraftPENetwork.Server
{
    public class OfflineMessageHandler
    {
        private SessionManager sessionManager;

        public OfflineMessageHandler(SessionManager mgr)
        {
            sessionManager = mgr;
        }

        public bool Handle(OfflineMessage packet, IPAddress ip, int port)
        {
            switch (packet.PacketID)
            {
                case UNCONNECTED_PING.ID:

                    var cast_0 = (UNCONNECTED_PING)packet;
                    cast_0.Decode();

                    var pk = new UNCONNECTED_PONG();
                    pk.serverID = sessionManager.GetID();
                    pk.pingID = cast_0.pingID;
                    pk.serverName = sessionManager.GetName();
                    sessionManager.SendPacket(pk, ip, port);

                    return true;

                case OPEN_CONNECTION_REQUEST_1.ID:

                    //packet.protocol; //TODO: check protocol number and refuse connections
                    var cast_1 = (OPEN_CONNECTION_REQUEST_1)packet;
                    cast_1.Decode();
                    
                    var pk1 = new OPEN_CONNECTION_REPLY_1();
                    pk1.mtuSize = cast_1.mtuSize;
                    pk1.serverID = sessionManager.GetID();
                    sessionManager.SendPacket(pk1, ip, port);

                    return true;

                case OPEN_CONNECTION_REQUEST_2.ID:

                    var cast_2 = (OPEN_CONNECTION_REQUEST_2)packet;
                    cast_2.Decode();
                    
                    if (cast_2.serverPort == sessionManager.GetPort() || !sessionManager.portChecking)
                    {
                        var mtuSize = Math.Min((short)cast_2.mtuSize, (short)1464); //Max size, do not allow creating large buffers to fill server memory
                        var pk2 = new OPEN_CONNECTION_REPLY_2();
                        pk2.mtuSize = mtuSize;
                        pk2.serverID = sessionManager.GetID();
                        pk2.clientAddress = ip.ToString();
                        pk2.clientPort = port;
                        sessionManager.SendPacket(pk2, ip, port);
                        sessionManager.CreateSession(ip.ToString(), port, cast_2.clientID, mtuSize);
                    }
                    else
                    {

                    }

                    return true;
            }
            return false;
        }
    }
}
