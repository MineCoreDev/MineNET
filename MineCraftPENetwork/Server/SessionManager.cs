using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

using MineCraftPENetwork.Protocol;

namespace MineCraftPENetwork.Server
{
    public class SessionManager
    {
        protected Dictionary<int, Packet> packetPool = new Dictionary<int, Packet>();

        protected RakNetServer server;

        protected UdpClient socket;

        protected int receiveBytes = 0;
        protected int sendBytes = 0;

        protected Dictionary<string, Session> sessions = new Dictionary<string, Session>();

        protected OfflineMessageHandler offlineMessageHandler;

        protected string name;

        protected int packetLimit = 1000;

        protected bool shutdown = false;

        protected long ticks;
        protected Timer tickClock;

        protected long lastMeasure;

        protected Dictionary<string, int> block = new Dictionary<string, int>();
        protected Dictionary<string, int> ipSec = new Dictionary<string, int>();

        public bool portChecking = false;

        public SessionManager(RakNetServer server, UdpClient socket)
        {
            this.server = server;
            this.socket = socket;

            offlineMessageHandler = new OfflineMessageHandler(this);

            RegisterPackets();

            Run();
        }

        public int GetPort()
        {
            return server.GetPort();
        }

        /*public void getLogger()
        {
            return server.GetLogger();
        }*/

        public void Run()
        {
            tickClock = new Timer(Tick, null, 100, 50);
            socket.BeginReceive(OnReceive, socket);
        }

        public void Tick(object state)
        {
            if (shutdown)
            {
                tickClock.Dispose();
            }
            ReceiveStream();

            for (var i = 0; i < sessions.Values.Count; i++)
            {
                var session = sessions.Values.ToArray()[i];
                session.Update(DateTime.Now.Ticks);
            }
        }

        public void OnReceive(IAsyncResult result)
        {
            IPEndPoint point = null;
            UdpClient client = (UdpClient)result.AsyncState;
            byte[] buffer = null;

            socket.BeginReceive(OnReceive, socket);

            try
            {
                buffer = client.EndReceive(result, ref point);
            }
            catch (SocketException e)
            {
                Console.WriteLine("[SocketError]Pass Receive!");
            }

            receiveBytes += buffer.Length;
            if (block.ContainsKey(point.Address.ToString()))
            {
                return;
            }

            if (ipSec.ContainsKey(point.Address.ToString()))
            {
                ipSec[point.Address.ToString()]++;
            }
            else
            {
                ipSec.Add(point.Address.ToString(), 1);
            }

            if (buffer.Length > 0)
            {
                try
                {
                    byte pid = buffer[0];
                    var pk = GetPacketFromPool(pid, buffer);
                    if (pk != null)
                    {
                        var session = GetSession(point.Address.ToString(), point.Port);
                        if (session != null)
                        {
                            if (pk is OfflineMessage)
                            {
                                Console.WriteLine("[Session]OffLineMessage");
                            }
                            else
                            {
                                session.HandlePacket(pk);
                            }
                        }
                        else if (pk is OfflineMessage)
                        {
                            offlineMessageHandler.Handle((OfflineMessage)pk, point.Address, point.Port);
                        }
                        else
                        {
                            Console.WriteLine("[Error]SessionNotCreate!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[NotHandle]NotHandlePacket!");
                        StreamRaw(point.Address, point.Port, buffer);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("[Error]BlockUser . " + point.Address.ToString());
                    Console.WriteLine("[Error]" + e.ToString());
                    BlockAddress(point.Address.ToString());
                }
            }
        }

        public void OnSend(IAsyncResult result)
        {
            UdpClient client = (UdpClient)result.AsyncState;
            var len = 0;
            try
            {
                len = client.EndSend(result);
            }
            catch (Exception e)
            {
                return;
            }
        }

        public void SendPacket(Packet packet, IPAddress ip, int port)
        {
            IPEndPoint point = new IPEndPoint(ip, port);
            packet.Encode();
            sendBytes += packet.Buffer.Length;
            socket.BeginSend(packet.Buffer, packet.Buffer.Length, point, OnSend, socket);
        }

        public void StreamEncapsulated(Session session, EncapsulatedPacket packet, int flags = RakNet.PRIORITY_NORMAL)
        {
            var id = session.GetAddress() + ":" + session.GetPort();
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_ENCAPSULATED);
            br.Write(id);
            br.Write((byte)flags);
            br.Write(packet.ToBinary(true));
            server.PushThreadToMainPacket(ms.ToArray());
        }

        public void StreamRaw(IPAddress address, int port, byte[] payload)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_RAW);
            br.Write(address.ToString());
            br.Write((short)port);
            br.Write(payload);
            server.PushThreadToMainPacket(ms.ToArray());
        }

        protected void StreamClose(string identifier, string reason)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_CLOSE_SESSION);
            br.Write(identifier);
            br.Write(reason);
            server.PushThreadToMainPacket(ms.ToArray());
        }

        protected void StreamInvalid(string identifier)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_INVALID_SESSION);
            br.Write(identifier);
            server.PushThreadToMainPacket(ms.ToArray());
        }

        protected void StreamOpen(Session session)
        {
            var identifier = session.GetAddress() + ":" + session.GetPort();
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_OPEN_SESSION);
            br.Write(identifier);
            br.Write(session.GetAddress());
            br.Write((short)session.GetPort());
            br.Write(session.GetID());
            server.PushThreadToMainPacket(ms.ToArray());
        }

        protected void StreamACK(string identifier, int identifierACK)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_ACK_NOTIFICATION);
            br.Write(identifier);
            br.Write(identifierACK);
            server.PushThreadToMainPacket(ms.ToArray());
        }

        protected void StreamOption(string name, byte[] value)
        {
            var ms = new MemoryStream();
            var br = new BinaryWriter(ms);
            br.Write(RakNet.PACKET_SET_OPTION);
            br.Write(name);
            br.Write(value);
            server.PushThreadToMainPacket(ms.ToArray());
        }

        public void ReceiveStream()
        {
            var packet = server.ReadMainToThreadPacket();
            if (packet.Length > 0)
            {
                var id = packet[0];
			    var offset = 1;

                if (id == RakNet.PACKET_ENCAPSULATED)
                {
                    var len = packet[offset++];
                    var identifier = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset, len));
                    offset += len;
                    if (sessions.ContainsKey(identifier))
                    {
                        var flags = packet[offset++];
                        var buffer = RakNet.GetBuffer(packet, offset);
                        var t = 0;
                        sessions[identifier].AddEncapsulatedToQueue(EncapsulatedPacket.FromBinary(buffer, ref t, true), flags);
                    }
                    else
                    {
                        StreamInvalid(identifier);
                    }
                }
                else if (id == RakNet.PACKET_RAW)
                {
                    var len = packet[offset++];
                    var address = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset, len));
                    offset += len;
                    var port = BitConverter.ToInt16(RakNet.GetBuffer(packet, offset, 2), 0);
                    offset += 2;
                    var payload = RakNet.GetBuffer(packet, offset);
                    var point = new IPEndPoint(IPAddress.Parse(address), port);
                    socket.BeginSend(payload, payload.Length, point, OnSend, socket);
                }
                else if (id == RakNet.PACKET_CLOSE_SESSION)
                {
                    var len = packet[offset++];
                    var identifier = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset, len));
                    if (sessions.ContainsKey(identifier))
                    {
                        RemoveSession(sessions[identifier]);
                    }
                    else
                    {
                        StreamInvalid(identifier);
                    }
                }
                else if (id == RakNet.PACKET_INVALID_SESSION)
                {
                    var len = packet[offset++];
                    var identifier = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset, len));
                    if (sessions.ContainsKey(identifier))
                    {
                        RemoveSession(sessions[identifier]);
                    }
                }
                else if (id == RakNet.PACKET_SET_OPTION)
                {
                    var len = packet[offset++];
                    var name = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset, len));
                    offset += len;
                    var value = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset));

                    switch (name)
                    {
                        case "name":
                            this.name = value;
                            break;
                        case "portChecking":
                            portChecking = bool.Parse(value);
                            break;
                        case "packetLimit":
                            packetLimit = int.Parse(value);
                            break;
                    }
                }
                else if (id == RakNet.PACKET_BLOCK_ADDRESS)
                {
                    var len = packet[offset++];
                    var address = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset, len));
                    offset += len;
                    var timeout = BitConverter.ToInt32(RakNet.GetBuffer(packet, offset, 4), 0);
                    BlockAddress(address, timeout);
                }
                else if (id == RakNet.PACKET_UNBLOCK_ADDRESS)
                {
                    var len = packet[offset++];
                    var address = Encoding.UTF8.GetString(RakNet.GetBuffer(packet, offset, len));
                    UnblockAddress(address);
                }
                else if (id == RakNet.PACKET_SHUTDOWN)
                {
                    foreach (var session in sessions.Values)
                    {
                        RemoveSession(session);
                    }

                    socket.Close();
                    shutdown = true;
                }
                else if (id == RakNet.PACKET_EMERGENCY_SHUTDOWN)
                {
                    shutdown = true;
                }
                else
                {
                }
            }
        }

        public void BlockAddress(string address, int timeout = 30000000)
        {
            var final = DateTime.Now.Ticks + timeout;
            if (!block.ContainsKey(address) || timeout == -1)
            {
                if (timeout == -1)
                {
                    final = long.MaxValue;
                }
                else
                {
                    //Block!
                }
                block.Add(address, (int)final);
            }
            else if (block[address] < final)
            {
                block[address] = (int)final;
            }
        }

        public void UnblockAddress(string address)
        {
            block.Remove(address);
        }

        public Session GetSession(string ip, int port)
        {
            var id = ip + ":" + port;
            if (sessions.ContainsKey(id))
            {
                return sessions[id];
            }
            return null;
        }

        public Session CreateSession(string ip, int port, long clientId, int mtuSize)
        {
            CheckSessions();

            var session = new Session(this, ip, port, clientId, mtuSize);
            sessions[ip + ":" + port] = session;

            return session;
        }

        public void RemoveSession(Session session, string reason = "unknown")
        {
            var id = session.GetAddress() + ":" + session.GetPort();
            if (sessions.ContainsKey(id))
            {
                sessions[id].Close();
                sessions.Remove(id);
                StreamClose(id, reason);
            }
        }

        public void OpenSession(Session session)
        {
            StreamOpen(session);
        }

        private void CheckSessions()
        {
            if (sessions.Count > 4096)
            {
                foreach (var key in sessions.Keys)
                {
                    if (sessions[key].IsTemporal())
                    {
                        sessions.Remove(key);
                        if (sessions.Count <= 4096)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public void NotifyACK(Session session, int identifierACK)
        {
            StreamACK(session.GetAddress() + ":" + session.GetPort(), identifierACK);
        }

        public string GetName()
        {
            return name;
        }

        public long GetID()
        {
            return server.GetServerId();
        }

        private void RegisterPacket(int id, Type type)
        {
            packetPool.Add(id, (Packet)Activator.CreateInstance(type));
        }

        public Packet GetPacketFromPool(int id, byte[] buffer)
        {
            if (packetPool.ContainsKey(id))
            {
                var pk = packetPool[id];
                if (pk != null)
                {
                    var cpk = (Packet)Activator.CreateInstance(pk.GetType());
                    cpk.Buffer = buffer;
                    return cpk;
                }
            }

            return null;
        }

        private void RegisterPackets()
        {
            RegisterPacket(UNCONNECTED_PING.ID, typeof(UNCONNECTED_PING));
            RegisterPacket(UNCONNECTED_PING_OPEN_CONNECTIONS.ID, typeof(UNCONNECTED_PING_OPEN_CONNECTIONS));

            RegisterPacket(OPEN_CONNECTION_REQUEST_1.ID, typeof(OPEN_CONNECTION_REQUEST_1));
            RegisterPacket(OPEN_CONNECTION_REPLY_1.ID, typeof(OPEN_CONNECTION_REPLY_1));
            RegisterPacket(OPEN_CONNECTION_REQUEST_2.ID, typeof(OPEN_CONNECTION_REQUEST_2));
            RegisterPacket(OPEN_CONNECTION_REPLY_2.ID, typeof(OPEN_CONNECTION_REPLY_2));

            RegisterPacket(UNCONNECTED_PONG.ID, typeof(UNCONNECTED_PONG));

            RegisterPacket(DATA_PACKET_0.ID, typeof(DATA_PACKET_0));
            RegisterPacket(DATA_PACKET_1.ID, typeof(DATA_PACKET_1));
            RegisterPacket(DATA_PACKET_2.ID, typeof(DATA_PACKET_2));
            RegisterPacket(DATA_PACKET_3.ID, typeof(DATA_PACKET_3));
            RegisterPacket(DATA_PACKET_4.ID, typeof(DATA_PACKET_4));
            //RegisterPacket(DATA_PACKET_5.ID, typeof(DATA_PACKET_5));
            //RegisterPacket(DATA_PACKET_6.ID, typeof(DATA_PACKET_6));
            //RegisterPacket(DATA_PACKET_7.ID, typeof(DATA_PACKET_7));
            //RegisterPacket(DATA_PACKET_8.ID, typeof(DATA_PACKET_8));
            //RegisterPacket(DATA_PACKET_9.ID, typeof(DATA_PACKET_9));
            //RegisterPacket(DATA_PACKET_A.ID, typeof(DATA_PACKET_A));
            //RegisterPacket(DATA_PACKET_B.ID, typeof(DATA_PACKET_B));
            RegisterPacket(DATA_PACKET_C.ID, typeof(DATA_PACKET_C));
            //RegisterPacket(DATA_PACKET_D.ID, typeof(DATA_PACKET_D));
            //RegisterPacket(DATA_PACKET_E.ID, typeof(DATA_PACKET_E));
            //RegisterPacket(DATA_PACKET_F.ID, typeof(DATA_PACKET_F));

            RegisterPacket(NACK.ID, typeof(NACK));
            RegisterPacket(ACK.ID, typeof(ACK));
        }

    }
}
