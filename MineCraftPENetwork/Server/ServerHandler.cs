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
            br.Write(packet.ToBinary(true));
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

        public void HandlePacket()
        {
            var packet = server.ReadThreadToMainPacket();
            if (packet.Length > 0)
            {
                var ms = new MemoryStream(packet);
                var br = new BinaryReader(ms);
                var id = br.ReadByte();
                var offset = 1;
                if (id == RakNet.PACKET_ENCAPSULATED)
                {
                    var identifier = br.ReadString();
                    var flags = br.ReadByte();
                    var buffer = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
                    instance.HandleEncapsulated(identifier, EncapsulatedPacket.FromBinary(buffer, ref offset, true), flags);
                }
                else if (id == RakNet.PACKET_RAW)
                {
                    var address = br.ReadString();
                    var port = br.ReadUInt16();
                    offset += 2;
                    var payload = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
                    instance.HandleRaw(address, port, payload);
                }
                else if (id == RakNet.PACKET_SET_OPTION) {
                    var name = br.ReadString();
                    var value = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
                    instance.HandleOption(name, value);
                }
                else if (id == RakNet.PACKET_OPEN_SESSION)
                {
                    var identifier = br.ReadString();
                    var address = br.ReadString();
                    var port = br.ReadUInt16();
                    var clientID = br.ReadInt64();
                    instance.OpenSession(identifier, address, port, clientID);
                }
                else if (id == RakNet.PACKET_CLOSE_SESSION)
                {
                    var identifier = br.ReadString();
                    var reason = br.ReadString();
                    instance.CloseSession(identifier, reason);
                }
                else if (id == RakNet.PACKET_INVALID_SESSION)
                {
                    var identifier = br.ReadString();
                    instance.CloseSession(identifier, "Invalid session");
                }
                else if (id == RakNet.PACKET_ACK_NOTIFICATION)
                {
                    var identifier = br.ReadString();
                    var identifierACK = br.ReadInt32();
                    instance.NotifyACK(identifier, identifierACK);
                }
            }
        }
    }
}
