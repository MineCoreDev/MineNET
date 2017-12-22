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
using MineCraftPENetwork.Packets.MCPE;
using MineCraftPENetwork.Packets.MCPE.PacketCapsule;

namespace MineCraftPENetwork
{
    public class Network
    {

        public UdpClient udpClient;
        public long serverID = new Random().Next(0, int.MaxValue);
        public string serverName;

        public ServerListFormat serverListFormat;

        public Network()
        {
            IPEndPoint ipPort = new IPEndPoint(IPAddress.Any, 19132);
            Console.WriteLine("[Network]UDPポートで開始しています。");
            udpClient = new UdpClient(ipPort);
            udpClient.BeginReceive(OnReceive, udpClient);
            Console.WriteLine("[Network]UDPポートで開始しました。");
        }

        public Network(int port)
        {
            IPEndPoint ipPort = new IPEndPoint(IPAddress.Any, port);
            Console.WriteLine("[Network]UDPポートで開始しています。");
            udpClient = new UdpClient(ipPort);
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
            Console.WriteLine("[Send]<id: " + rakNetPacketID +">");
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
            switch (rakNetPacketID)
            {
                case PingOpenPacket.ID:

                    var pingPK = new PingOpenPacket();
                    pingPK.Decode(reader);

                    var pingRPK = new PingOpenConnectionPacket();
                    pingRPK.pingID = pingPK.pingID;
                    pingRPK.serverID = serverID;
                    pingRPK.magic = pingPK.magic;
                    pingRPK.serverName = serverListFormat.ToString();

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

                    Console.WriteLine("[Debug]<serverID: " + serverID + "><clientID: " + request2.clientID + "><mtu: " + request2.mtuSize + ">");

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

                    var capsulePacket = MCPEPacket.ConvertBinaryReader(dataPK4.payload);
                    byte capsuleID = capsulePacket.ReadByte();

                    MCPEHandleInternal(capsuleID, capsulePacket);
                    break;
            }

            Console.WriteLine("[Handle]<rakID: " + rakNetPacketID + ">");
        }
        
        private void MCPEHandleInternal(byte capsuleID, BinaryReader reader)
        {
            byte packetTypeID = 0;
            switch (capsuleID)
            {
                case PacketCapsule_0.CapsuleID:
                    var cap0 = new PacketCapsule_0();
                    cap0.Decode(reader);

                    var packet0 = MCPEPacket.ConvertBinaryReader(cap0.packet);
                    packetTypeID = packet0.ReadByte();

                    MCPEHandle(packetTypeID, packet0);
                    break;

                case PacketCapsule_1.CapsuleID:
                    var cap1 = new PacketCapsule_1();
                    cap1.Decode(reader);

                    var packet1 = MCPEPacket.ConvertBinaryReader(cap1.packet);
                    packetTypeID = packet1.ReadByte();

                    MCPEHandle(packetTypeID, packet1);
                    break;
            }

            Console.WriteLine("[Handle]<capID: " + capsuleID + ">");
        }

        private void MCPEHandle(byte packetTypeID, BinaryReader reader)
        {
            Console.WriteLine("[Handle]<packetID: " + packetTypeID + ">");
        }
    }
}
