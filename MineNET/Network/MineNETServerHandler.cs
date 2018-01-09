using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCraftPENetwork;
using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

using MineNET.Entities;
using MineNET.Network.Packets;

namespace MineNET.Network
{
    public class MineNETServerHandler : ServerInstance
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        private Dictionary<int, string> identifiers = new Dictionary<int, string>();

        private Dictionary<string, int> identifiersACK = new Dictionary<string, int>();

        public void CloseSession(string identifier, string reason)
        {
            if (players.ContainsKey(identifier))
            {
                identifiers.Remove(players[identifier].GetHashCode());
                identifiersACK.Remove(identifier);
                players.Remove(identifier);
                //TODO Player Close
            }
        }

        public void HandleEncapsulated(string identifier, EncapsulatedPacket packet, int flags)
        {
            if (players.ContainsKey(identifier))
            {
                try
                {
                    this.GetPacket(packet.buffer, identifier);
                }
                catch (Exception e)
                {
                    Server.GetLogger().Error(e);
                }
            }
        }

        public void HandleOption(string option, byte[] value)
        {
            //throw new NotImplementedException();
        }

        public void HandleRaw(string address, int port, byte[] payload)
        {
            //throw new NotImplementedException();
        }

        public void NotifyACK(string identifier, int identifierACK)
        {
            //throw new NotImplementedException();
        }

        public void OpenSession(string identifier, string address, int port, long clientID)
        {
            if (!players.ContainsKey(identifier))
            {
                var player = new Player();

                //TODO Event...
                identifiers.Add(player.GetHashCode(), identifier);
                identifiersACK.Add(identifier, 0);
                players.Add(identifier, player);
            }
        }

        public void PutPacket(Player player, Packets.Packet packet, bool needACK = false, bool immediate = true)
        {
            EncapsulatedPacket ep = null;
            int hash = player.GetHashCode();
            if (this.identifiers.ContainsKey(hash))
            {
                string identifier = this.identifiers[hash];
                if (packet is BatchPacket)
                {
                    if (needACK)
                    {
                        ep = new EncapsulatedPacket();
                        ep.identifierACK = this.identifiersACK[identifier]++;
                        ep.buffer = ((BatchPacket)packet).GetBuffer();
                        ep.reliability = immediate ? PacketReliability.RELIABLE : PacketReliability.RELIABLE_ORDERED;
					    ep.orderChannel = 0;
                    }
                    else
                    {
                        ep = new EncapsulatedPacket();
                        ep.identifierACK = -1;
                        ep.buffer = ((BatchPacket)packet).GetBuffer();
                        ep.reliability = immediate ? PacketReliability.RELIABLE : PacketReliability.RELIABLE_ORDERED;
                        ep.orderChannel = 0;
                    }
                    Server.GetInstance().networkManager.serverHandler.SendEncapsulated(identifier, ep, (needACK == true ? RakNet.FLAG_NEED_ACK : 0) | (immediate == true ? RakNet.PRIORITY_IMMEDIATE : RakNet.PRIORITY_NORMAL));
                }
                else
                {
                    var batch = new BatchPacket();
                    
                    batch.PutPacket(packet);
                    batch.Encode();

                    this.PutPacket(player, batch, needACK, immediate);
                }
            }
        }

        public async void GetPacket(byte[] buffer, string id)
        {
            var pid = buffer[0];
            if (pid != 0xfe)
            {
                return;
            }

            Console.WriteLine("[Debug]HandleBatchPacket");

            var pk = new BatchPacket();
            pk.Write(buffer, 0, buffer.Length);

            pk.Decode();

            var packets = await pk.GetPackets();

            foreach(var p in packets)
            {
                players[id].HandlePacket(p);
            }
        }
    }
}
