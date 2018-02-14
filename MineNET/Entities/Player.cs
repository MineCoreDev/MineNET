using MineNET.Commands;
using MineNET.Network.Packets;
using MineNET.Utils;
using System.Net;

namespace MineNET.Entities
{
    public class Player : Human, CommandSender
    {
        IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get
            {
                return this.endPoint;
            }

            set
            {
                this.endPoint = value;
            }
        }

        public void PacketHandle(DataPacket pk)
        {
            if (pk is LoginPacket)
            {
                this.LoginPacketHandle((LoginPacket) pk);
            }
            else if (pk is ResourcePackClientResponsePacket)
            {
                this.ResourcePackClientResponsePacketHandle((ResourcePackClientResponsePacket) pk);
            }
        }

        public void LoginPacketHandle(LoginPacket pk)
        {
            if (pk.Protocol < ProtocolInfo.CLIENT_PROTOCOL)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_CLIENT);
                this.Close("disconnectionScreen.outdatedClient");
                return;
            }
            else if (pk.Protocol > ProtocolInfo.CLIENT_PROTOCOL)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_SERVER);
                this.Close("disconnectionScreen.outdatedServer");
                return;
            }

            this.SendPlayStatus(PlayStatusPacket.LOGIN_SUCCESS);

            ResourcePacksInfoPacket resourcePacksInfoPacket = new ResourcePacksInfoPacket();
            this.SendPacket(resourcePacksInfoPacket);
        }

        public void ResourcePackClientResponsePacketHandle(ResourcePackClientResponsePacket pk)
        {
            Logger.Info($"{pk.ResponseStatus}");
            if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_REFUSED)
            {
                this.Close("disconnectionScreen.noReason");
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_SEND_PACKS)
            {
                //TODO
                //ResourcePackDataInfoPacket
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_HAVE_ALL_PACKS)
            {
                ResourcePackStackPacket resourcePackStackPacket = new ResourcePackStackPacket();
                this.SendPacket(resourcePackStackPacket);
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_COMPLETED)
            {
                //StartGamePacket
                ResourcePacksInfoPacket resourcePacksInfoPacket = new ResourcePacksInfoPacket();
                resourcePacksInfoPacket.MustAccepet = true;
                this.SendPacket(resourcePacksInfoPacket);
            }
        }

        public void SendPlayStatus(int status)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            this.SendPacket(pk);
        }

        public void SendPacket(DataPacket pk, bool needACK = false, bool immediate = false)
        {
            Server.Instance.NetworkManager.SendPacket(this.endPoint, pk);
        }

        public void Close(string reason)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                this.SendPacket(pk);
            }
            Server.Instance.NetworkManager.PlayerClose(this.endPoint, reason);
        }
    }
}
