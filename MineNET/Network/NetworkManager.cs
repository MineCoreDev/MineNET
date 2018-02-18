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
                return this.server;
            }
        }

        public Dictionary<string, Player> players = new Dictionary<string, Player>();
        public Dictionary<string, int> identifierACKs = new Dictionary<string, int>();

        public Dictionary<int, DataPacket> packetPool = new Dictionary<int, DataPacket>();

        public NetworkManager()
        {
            this.Init();
        }

        void Init()
        {
            this.server = new RakNetServer(MineNET.Server.ServerConfig.ServerPort);
            this.RegisterPackets();
        }

        public void PlayerClose(IPEndPoint point, string reason)
        {
            RakNetSession session = server.GetSession(point);
            session.Close(reason);
        }

        public void CreatePlayer(IPEndPoint point, string id)
        {
            if (!this.players.ContainsKey(id))
            {
                Player player = new Player();
                player.EndPoint = point;

                this.players.Add(id, player);
                this.identifierACKs.Add(id, 0);
            }
            else
            {
                throw new Exception("Created Player!");
            }
        }

        public void RemovePlayer(string id)
        {
            if (this.players.ContainsKey(id))
            {
                this.identifierACKs.Remove(id);
                this.players.Remove(id);
            }
        }

        public void HandleBatchPacket(RakNetSession session, byte[] buffer)
        {
            string id = RakNetServer.IPEndPointToID(session.EndPoint);
            if (this.players.ContainsKey(id))
            {
                Player player = players[id];
                int pkid = buffer[0];

                if (pkid == BatchPacket.ID)
                {
                    using (BatchPacket batch = new BatchPacket())
                    {
                        batch.SetBuffer(buffer);
                        batch.Decode();

                        this.GetPackets(batch, player);
                    }
                }
            }
        }

        public void SendPacket(IPEndPoint point, DataPacket pk, bool needACK = false, bool immediate = false)
        {
            RakNetSession session = server.GetSession(point);

            pk.Encode();

            byte[] buffer = pk.GetResult();
            Logger.Log("%server_packet_send", buffer[0].ToString("X"));

            BinaryStream st = new BinaryStream();
            st.WriteVarInt((int) pk.Length);
            st.WriteBytes(buffer);

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
            using (BinaryStream stream = new BinaryStream(pk.Payload))
            {
                while (!stream.EndOfStream())
                {
                    int len = stream.ReadVarInt();
                    byte[] buffer = stream.ReadBytes(len);
                    using (DataPacket packet = GetPacket(buffer[0]))
                    {
                        if (packet != null)
                        {
                            Logger.Log("%server_packet_handle", buffer[0].ToString("X"));
                            packet.SetBuffer(buffer);
                            packet.Decode();
                            player.PacketHandle(packet);
                        }
                        else
                        {
                            Logger.Log("%server_packet_notHandle", buffer[0].ToString("X"));
                        }
                    }
                }
            }
        }

        DataPacket GetPacket(int id)
        {
            if (packetPool.ContainsKey(id))
            {
                return (DataPacket) packetPool[id].Clone();
            }
            return null;
        }

        public void RegisterPacket(DataPacket packet)
        {
            if (this.packetPool.ContainsKey(packet.PacketID))
            {
                this.packetPool[packet.PacketID] = packet;
            }
            else
            {
                this.packetPool.Add(packet.PacketID, packet);
            }
        }

        void RegisterPackets()
        {
            this.RegisterPacket(new LoginPacket());
            this.RegisterPacket(new PlayStatusPacket());
            this.RegisterPacket(new ResourcePackClientResponsePacket());
            this.RegisterPacket(new RequestChunkRadiusPacket());
        }
    }
}
