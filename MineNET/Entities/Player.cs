using System.Net;
using MineNET.Commands;
using MineNET.Network.Packets;

namespace MineNET.Entities
{
    public class Player : Human, CommandSender
    {
        IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get
            {
                return endPoint;
            }

            set
            {
                endPoint = value;
            }
        }

        public void PacketHandle(DataPacket pk)
        {
            if (pk is LoginPacket)
            {
                LoginPacketHandle((LoginPacket)pk);
            }
        }

        public void LoginPacketHandle(LoginPacket pk)
        {
            if (pk.Protocol < ProtocolInfo.CLIENT_PROTOCOL)
            {
                SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_CLIENT);
                Close("disconnectionScreen.outdatedClient");
                return;
            }
            else if (pk.Protocol > ProtocolInfo.CLIENT_PROTOCOL)
            {
                SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_SERVER);
                Close("disconnectionScreen.outdatedServer");
                return;
            }

            SendPlayStatus(PlayStatusPacket.LOGIN_SUCCESS);
        }

        public void SendPlayStatus(int status)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            SendPacket(pk);
        }

        public void SendPacket(DataPacket pk, bool needACK = false, bool immediate = false)
        {
            Server.Instance.NetworkManager.SendPacket(endPoint, pk);
        }

        public void Close(string reason)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                SendPacket(pk);
            }
            Server.Instance.NetworkManager.PlayerClose(endPoint, reason);
        }
    }
}
