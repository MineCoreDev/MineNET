using System.Net;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Network.Packets;
using MineNET.Utils;

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

            internal set
            {
                this.endPoint = value;
            }
        }

        LoginData loginData;
        public LoginData LoginData
        {
            get
            {
                return this.loginData;
            }

            set
            {
                this.loginData = value;
            }
        }

        ClientData clientData;
        public ClientData ClientData
        {
            get
            {
                return this.clientData;
            }

            set
            {
                this.clientData = value;
            }
        }

        bool packDownloaded;
        public bool PackDownloaded
        {
            get
            {
                return this.packDownloaded;
            }
        }

        bool packStatusCompleted;
        public bool PackStatusCompleted
        {
            get
            {
                return this.packStatusCompleted;
            }
        }

        public void PacketHandle(DataPacket pk)
        {
            Logger.Info(pk.ToString());
            if (pk is LoginPacket)
            {
                this.LoginPacketHandle((LoginPacket) pk);
            }
            else if (pk is ResourcePackClientResponsePacket)
            {
                this.ResourcePackClientResponsePacketHandle((ResourcePackClientResponsePacket) pk);
            }
            else if (pk is RequestChunkRadiusPacket)
            {
                this.RequestChunkRadiusPacketHandle((RequestChunkRadiusPacket) pk);
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
            if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_REFUSED)
            {
                this.Close("disconnectionScreen.resourcePackn");
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_SEND_PACKS)
            {
                //TODO:
                //ResourcePackDataInfoPacket
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_HAVE_ALL_PACKS)
            {
                packDownloaded = true;
                ResourcePackStackPacket resourcePackStackPacket = new ResourcePackStackPacket();
                this.SendPacket(resourcePackStackPacket);
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_COMPLETED)
            {
                packStatusCompleted = true;
                ProcessLogin();
            }
        }

        public void RequestChunkRadiusPacketHandle(RequestChunkRadiusPacket pk)
        {
            ChunkRadiusUpdatedPacket chunkRadiusUpdatedPacket = new ChunkRadiusUpdatedPacket();
            chunkRadiusUpdatedPacket.Radius = FixRadius(pk.Radius);
            Logger.Info(LangManager.GetString("server_chunkRadius"), pk.Radius, chunkRadiusUpdatedPacket.Radius);
            SendPacket(chunkRadiusUpdatedPacket);

            SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);
        }

        void ProcessLogin()
        {
            StartGamePacket startGamePacket = new StartGamePacket();
            startGamePacket.EntityUniqueId = 1;
            startGamePacket.EntityRuntimeId = 1;
            startGamePacket.PlayerGamemode = 0;
            startGamePacket.PlayerPosition = new Vector3(128, 4, 128);
            startGamePacket.Direction = new Vector2(0, 0);
            startGamePacket.WorldGamemode = 0;
            startGamePacket.Difficulty = 1;
            startGamePacket.SpawnX = 128;
            startGamePacket.SpawnY = 4;
            startGamePacket.SpawnZ = 128;
            startGamePacket.WorldName = "world";
            this.SendPacket(startGamePacket);

            this.SendPlayerAttribute();

            //AvailableCommands
            AvailableCommandsPacket availableCommandsPacket = new AvailableCommandsPacket();
            this.SendPacket(availableCommandsPacket);

            //AdventureSettings
            AdventureSettingsPacket adventureSettingsPacket = new AdventureSettingsPacket();
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.NO_PVP, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.AUTO_JUMP, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.NO_CLIP, false);
            adventureSettingsPacket.SetFlag(AdventureSettingsPacket.FLYING, false);
            adventureSettingsPacket.EntityUniqueId = 1;
            this.SendPacket(adventureSettingsPacket);

            //SetEntityData
            //InventoryContent
            //MobArmorEquipment
            //inventoryContent
            //MobEquipment
            //InventorySlot
            //PlayerList
        }

        int FixRadius(int radius)
        {
            return radius;
        }

        public void SendPlayStatus(int status)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            this.SendPacket(pk);
        }

        void SendPlayerAttribute()
        {
            EntityAttribute[] atts = new EntityAttribute[]
            {
                EntityAttribute.GetAttribute(EntityAttribute.HEALTH),
                EntityAttribute.GetAttribute(EntityAttribute.HUNGER),
                EntityAttribute.GetAttribute(EntityAttribute.MOVEMENT_SPEED),
                EntityAttribute.GetAttribute(EntityAttribute.EXPERIENCE_LEVEL),
                EntityAttribute.GetAttribute(EntityAttribute.EXPERIENCE)
            };

            UpdateAttributesPacket updateAttributesPacket = new UpdateAttributesPacket();
            updateAttributesPacket.EntityRuntimeId = 1;
            updateAttributesPacket.Attributes = atts;
            this.SendPacket(updateAttributesPacket);
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
