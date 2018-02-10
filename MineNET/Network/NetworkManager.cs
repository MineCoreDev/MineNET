using System;
using System.Collections.Generic;
using System.Net;
using MineNET.Entities;
using MineNET.Network.Packets;
using MineNET.RakNet;
using MineNET.Utils;

namespace MineNET.Network
{
    public sealed class NetworkManager
    {

        RakNetServer server;
        public RakNetServer Server
        {
            get
            {
                return server;
            }
        }

        public Dictionary<string, Player> players = new Dictionary<string, Player>();

        public Dictionary<int, DataPacket> packetPool = new Dictionary<int, DataPacket>();

        public NetworkManager()
        {
            Init();
        }

        void Init()
        {
            server = new RakNetServer(1115);
            RegisterPackets();
        }

        public void PlayerClose(IPEndPoint point, string reason)
        {
            RakNetSession session = server.GetSession(point);
            session.Close(reason);
        }

        public void CreatePlayer(IPEndPoint point, string id)
        {
            if (!players.ContainsKey(id))
            {
                Player player = new Player();
                player.EndPoint = point;

                players.Add(id, player);
            }
            else
            {
                throw new Exception("Created Player!");
            }
        }

        public void RemovePlayer(string id)
        {
            if (players.ContainsKey(id))
            {
                players.Remove(id);
            }
        }

        public void HandleBatchPacket(RakNetSession session, byte[] buffer)
        {
            string id = RakNetServer.IPEndPointToID(session.EndPoint);
            if (players.ContainsKey(id))
            {
                Player player = players[id];
                int pkid = buffer[0];

                if (pkid == BatchPacket.ID)
                {
                    BatchPacket batch = new BatchPacket();
                    batch.SetBuffer(buffer);
                    batch.Decode();

                    GetPackets(batch, player);
                }
            }
        }

        public void SendPacket(IPEndPoint point, DataPacket pk)
        {
            RakNetSession session = server.GetSession(point);

            pk.Encode();

            BinaryStream st = new BinaryStream();
            st.WriteVarInt((int)pk.Length);
            st.WriteBytes(pk.GetResult());

            BatchPacket bp = new BatchPacket();
            bp.Payload = st.GetResult();
            bp.Encode();

            RakNet.Packets.EncapsulatedPacket enc = new RakNet.Packets.EncapsulatedPacket();
            enc.buffer = bp.GetResult();
            enc.reliability = RakNet.Packets.PacketReliability.UNRELIABLE;

            session.SendPacket(enc);
        }

        void GetPackets(BatchPacket pk, Player player)
        {
            BinaryStream stream = new BinaryStream(pk.Payload);
            while (!stream.EndOfStream())
            {
                int len = stream.ReadVarInt();
                byte[] buffer = stream.ReadBytes(len);
                DataPacket packet = GetPacket(buffer[0]);
                if (packet != null)
                {
                    packet.SetBuffer(buffer);
                    packet.Decode();
                    player.PacketHandle(packet);
                }
                else
                {
                    //Logger.Log("NotHandlePacket {0}", buffer[0]);
                }
            }
        }

        DataPacket GetPacket(int id)
        {
            if (packetPool.ContainsKey(id))
            {
                return (DataPacket)packetPool[id].Clone();
            }
            return null;
        }

        public void RegisterPacket(DataPacket packet)
        {
            if (packetPool.ContainsKey(packet.PacketID))
            {
                packetPool[packet.PacketID] = packet;
            }
            else
            {
                packetPool.Add(packet.PacketID, packet);
            }
        }

        void RegisterPackets()
        {
            RegisterPacket(new LoginPacket());
        }
    }
}
