using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Events;
using MineNET.Events.EventArgs;
using MineNET.Network.Packets;

namespace MineNET.Entities
{
    public class Player : Human
    {
        private string ip;
        public string IP
        {
            get
            {
                return ip;
            }
        }

        private int port;
        public int Port
        {
            get
            {
                return port;
            }
        }

        public Player()
        {

        }

        public void HandlePacket(Packet packet)
        {
            if (packet is LoginPacket)
            {
                this.HandleLoginPacket((LoginPacket)packet);
            }
        }

        public void HandleLoginPacket(LoginPacket packet)
        {
            packet.Decode();

            if (packet.Protocol != ProtocolInfo.CLIENT_PROTOCOL)
            {
                SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_CLIENT, true);
                return;
            }

            SendPlayStatus(PlayStatusPacket.LOGIN_SUCCESS);
        }

        public void SendPlayStatus(int status, bool immediate = false)
        {
            var pk = new PlayStatusPacket();
            pk.Status = status;
            pk.Encode();
            SendPacket(pk, false, immediate);
        }

        public void SendPacket(Packet packet, bool needACK = false, bool immediate = false)
        {
            //TODO ServerEvents.OnPacketSend(new PacketSendEventArgs());
            Server.GetInstance().networkManager.mineNetServerHandler.PutPacket(this, packet, needACK, immediate);
        }

        public void Close()
        {
            //Server.GetInstance().networkManager.mineNetServerHandler.CloseSession();
        }
    }
}
