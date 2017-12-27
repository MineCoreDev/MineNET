using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using MineCraftPENetwork.Protocol;

namespace MineCraftPENetwork.Server
{
    public class ServerHandler
    {
        protected RakNetServer server;
        protected ServerInstance instance;

        public ServerHandler(RakNetServer server, ServerInstance instance)
        {
            this.server = server;
            this.instance = instance;
        }

        public void SendEncapsulated(Session session, EncapsulatedPacket packet, int flags = RakNet.PRIORITY_NORMAL)
        {
            var id = session.GetAddress() + ":" + session.GetPort();
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_ENCAPSULATED);
            br.Write(id);
            br.Write((byte)flags);
            //TODO br.Write(packet.ToBinary(true));
            server.PushMainToThreadPacket(ms.ToArray());
        }

        public void SendRaw(IPAddress address, int port, byte[] payload)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_RAW);
            br.Write(address.ToString());
            br.Write((short)port);
            br.Write(payload);
            server.PushMainToThreadPacket(ms.ToArray());
        }

        protected void CloseSession(string identifier, string reason)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_CLOSE_SESSION);
            br.Write(identifier);
            br.Write(reason);
            server.PushMainToThreadPacket(ms.ToArray());
        }

        public void SendOption(string name, byte[] value)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_SET_OPTION);
            br.Write(name);
            br.Write(value);
            server.PushMainToThreadPacket(ms.ToArray());
        }

        public void BlockAddress(string address, int timeout)
        {
		//buffer = chr(RakLib::PACKET_BLOCK_ADDRESS).chr(strlen(address)). address.Binary::writeInt(timeout);
		//server.pushMainToThreadPacket(buffer);
        }

        public void UnblockAddress(string address)
        {
		//buffer = chr(RakLib::PACKET_UNBLOCK_ADDRESS).chr(strlen(address)). address;
		//server.pushMainToThreadPacket(buffer);
        }

        public void Shutdown()
        {
            server.PushMainToThreadPacket(new byte[1] { RakNet.PACKET_SHUTDOWN });
		    server.Shutdown();
        }

        public void EmergencyShutdown()
        {
		    server.Shutdown();
		    server.PushMainToThreadPacket(new byte[1] { RakNet.PACKET_EMERGENCY_SHUTDOWN });
        }

        protected void InvalidSession(string identifier)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_INVALID_SESSION);
            br.Write(identifier);
            server.PushThreadToMainPacket(ms.ToArray());
        }
    }
}
