using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Net;
using System.Net.Sockets;

using MineCraftPENetwork.Packets.Data;
using MineCraftPENetwork.Packets.RakNet;
using MineCraftPENetwork.Packets.RakNet.PacketCapsule;
using MineCraftPENetwork.Packets.RakNet.CapsuledPacket;

namespace MineCraftPENetwork
{
    public class Network
    {

        public UdpClient udpClient;
        public IPEndPoint ipData;
        public long serverID = new Random().Next(0, int.MaxValue);
        public string serverName;

        public ServerListFormat serverListFormat;

        public Network()
        {
            ipData = new IPEndPoint(IPAddress.Any, 19132);
            Console.WriteLine("[Network]UDPポートで開始しています。");
            udpClient = new UdpClient(ipData);
            udpClient.BeginReceive(OnReceive, udpClient);
            Console.WriteLine("[Network]UDPポートで開始しました。");
        }

        public Network(int port)
        {
            ipData = new IPEndPoint(IPAddress.Any, port);
            Console.WriteLine("[Network]UDPポートで開始しています。");
            udpClient = new UdpClient(ipData);
            udpClient.BeginReceive(OnReceive, udpClient);
            Console.WriteLine("[Network]UDPポートで開始しました。");
        }

        public void SendPacket(IPEndPoint ipPort, MemoryStream ms)
        {

        }

        public void SendPacket(IPEndPoint ipPort, RakNetPacket packet)
        {
            byte[] buffer = packet.Encode();
            udpClient.BeginSend(buffer, buffer.Length, ipPort, OnSend, udpClient);

            BinaryReader reader = new BinaryReader(new MemoryStream(buffer));
            byte rakNetPacketID = reader.ReadByte();
            Console.WriteLine("[Send]<id: " + rakNetPacketID +"><Length:" + reader.BaseStream.Length + ">");
        }

        public void OnReceive(IAsyncResult result)
        {
            IPEndPoint sender = null;
            UdpClient client = (UdpClient)result.AsyncState;
            byte[] buffer;

            try
            {
                buffer = client.EndReceive(result, ref sender);
            }
            catch(SocketException e)
            {
                Console.WriteLine("[SocketError]" + e.Message + "->" + e.ErrorCode);
                return;
            }
            catch(ObjectDisposedException e)
            {
                Console.WriteLine("[NetworkError]Closed Network!");
                Console.WriteLine("[NetworkError]" + e.Message);
                return;
            }

            BinaryReader reader = new BinaryReader(new MemoryStream(buffer));
            byte rakNetPacketID = reader.ReadByte();
            Handle(rakNetPacketID, reader, sender);

            client.BeginReceive(OnReceive, client);
        }

        private void OnSend(IAsyncResult result)
        {
            UdpClient client = (UdpClient)result.AsyncState;
            int byteLength;

            try
            {
                byteLength = client.EndSend(result);
            }
            catch (SocketException e)
            {
                Console.WriteLine("[SocketError]" + e.Message + "->" + e.ErrorCode);
                return;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("[NetworkError]Closed Network!");
                Console.WriteLine("[NetworkError]" + e.Message);
                return;
            }
        }

        private void Handle(byte rakNetPacketID, BinaryReader reader, IPEndPoint ipPort)
        {
            Console.WriteLine("[Handle]<rakID: " + rakNetPacketID + "><Length:" + reader.BaseStream.Length + ">");

            switch (rakNetPacketID)
            {
                case PingOpenPacket.ID:

                    var pingPK = new PingOpenPacket();
                    pingPK.Decode(reader);

                    var pingRPK = new PingOpenConnectionPacket();
                    pingRPK.pingID = pingPK.pingID;
                    pingRPK.serverID = serverID;
                    pingRPK.magic = pingPK.magic;
                    pingRPK.serverName = "";//serverListFormat.ToString();

                    SendPacket(ipPort, pingRPK);

                    break;

                case OpenConnectionRequestPacket_1.ID:

                    var request = new OpenConnectionRequestPacket_1();
                    request.Decode(reader);

                    var reply = new OpenConnectionReplyPacket_1();
                    reply.magic = request.magic;
                    reply.serverID = serverID;
                    reply.serverSecurity = 0;
                    reply.mtuSize = request.mtuSize;

                    SendPacket(ipPort, reply);

                    break;

                case OpenConnectionRequestPacket_2.ID:

                    var request2 = new OpenConnectionRequestPacket_2();
                    request2.Decode(reader);

                    var reply2 = new OpenConnectionReplyPacket_2();
                    reply2.magic = request2.magic;
                    reply2.serverID = serverID;
                    reply2.clientPort = request2.clientPort;
                    reply2.mtuSize = request2.mtuSize;
                    reply2.security = request2.security;

                    SendPacket(ipPort, reply2);

                    break;

                case DataPacket_4.ID:

                    var dataPK4 = new DataPacket_4();
                    dataPK4.Decode(reader);

                    var capsulePackets = dataPK4.packets;
                    foreach (var capsulePacket in capsulePackets)
                    {
                        var br = RakNetPacket.ConvertBinaryReader(capsulePacket.packet);
                        byte packetID = br.ReadByte();

                        CapsuledPacketHandle(packetID, br, ipPort);
                    }

                    break;

                case ACKPacket.ID:

                    var ack = new ACKPacket();
                    ack.Decode(reader);

                    SendPacket(ipPort, ack);

                    break;
            }
            //Console.WriteLine("[Handle]<packetID: " + rakNetPacketID + "><unkown: " + (reader.BaseStream.Length - reader.BaseStream.Position) + ">");
        }

        private void CapsuledPacketHandle(byte packetTypeID, BinaryReader reader, IPEndPoint ipPort)
        {
            Console.WriteLine("[Handle]<packetID: " + packetTypeID + "><Length:" + reader.BaseStream.Length + ">");

            switch (packetTypeID)
            {
                case ClientConnectPacket.ID:

                    var ccp = new ClientConnectPacket();
                    ccp.Decode(reader);

                    var serverHand = new ServerHandShakePacket();
                    serverHand.ip = ipPort.Address.ToString();
                    serverHand.port = (byte)ipPort.Port;
                    serverHand.session = ccp.session;
                    serverHand.pong = ccp.session;

                    var cap_0 = new PacketCapsule_0();
                    cap_0.SetLength((int)reader.BaseStream.Length);
                    cap_0.packet = serverHand.Encode();

                    var dp_0 = new DataPacket_0();
                    dp_0.packets = new PacketCapsuleBase[1]
                    {
                        cap_0
                    };

                    SendPacket(ipPort, dp_0);

                    break;

                case ClientHandShakePacket.ID:

                    var cliendHand = new ClientHandShakePacket();
                    cliendHand.Decode(reader);

                    Console.WriteLine("[Handle]<packetID: " + packetTypeID + "><unkown: " + (reader.BaseStream.Length - reader.BaseStream.Position) + ">");
                    //Console.WriteLine("[Debug]");

                    break;
            }
        }
    }
}
