using System;
using System.Collections.Generic;
using System.Net;
using MineNET.Entities;
using MineNET.Network.Packets;
using MineNET.RakNet;

namespace MineNET.Network
{
    public sealed class NetworkManager
    {
        public Dictionary<string, Player> players = new Dictionary<string, Player>();

        public NetworkManager()
        {
            Init();
        }

        void Init()
        {

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
                    //TODO: BatchPacket GetDataPacket
                }
            }
        }
    }
}
