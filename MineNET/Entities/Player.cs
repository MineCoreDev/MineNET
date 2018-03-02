using System.Net;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Inventories;
using MineNET.Network.Packets;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Data;

namespace MineNET.Entities
{
    public class Player : EntityHuman, CommandSender
    {
        private PlayerInventory inventory;

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
            else if (pk is MovePlayerPacket)
            {
                //this.MovePlayerPacketHandle((MovePlayerPacket) pk);
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
                this.packDownloaded = true;
                ResourcePackStackPacket resourcePackStackPacket = new ResourcePackStackPacket();
                this.SendPacket(resourcePackStackPacket);
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_COMPLETED)
            {
                this.packStatusCompleted = true;
                this.ProcessLogin();
            }
        }

        public void RequestChunkRadiusPacketHandle(RequestChunkRadiusPacket pk)
        {
            int chunkSize = FixRadius(pk.Radius);
            ChunkRadiusUpdatedPacket chunkRadiusUpdatedPacket = new ChunkRadiusUpdatedPacket();
            chunkRadiusUpdatedPacket.Radius = chunkSize;
            Logger.Info("%server_chunkRadius", pk.Radius, chunkRadiusUpdatedPacket.Radius);
            SendPacket(chunkRadiusUpdatedPacket);

            for (int i = (128 >> 4) - chunkSize; i < (128 >> 4) + chunkSize; ++i)
            {
                for (int j = (128 >> 4) - chunkSize; j < (128 >> 4) + chunkSize; ++j)
                {
                    new Chunk(i, j).TestChunkSend(this);
                }
            }

            this.SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);

            GameRules rules = new GameRules();
            rules.Add(new GameRule<bool>("ShowCoordinates", true));

            GameRulesChangedPacket gameRulesChangedPacket = new GameRulesChangedPacket();
            gameRulesChangedPacket.GameRules = rules;
            this.SendPacket(gameRulesChangedPacket);
        }

        void ProcessLogin()
        {
            StartGamePacket startGamePacket = new StartGamePacket();
            startGamePacket.EntityUniqueId = this.id;
            startGamePacket.EntityRuntimeId = this.id;
            startGamePacket.PlayerGamemode = 1;
            startGamePacket.PlayerPosition = new Vector3(128, 6, 128);
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
            adventureSettingsPacket.EntityUniqueId = this.id;
            this.SendPacket(adventureSettingsPacket);

            //SetEntityData
            //InventoryContent
            //MobArmorEquipment
            //inventoryContent
            //MobEquipment
            //InventorySlot
            //PlayerList
        }

        public void MovePlayerPacketHandle(MovePlayerPacket pk)
        {
            this.SendPosition(new Vector3(), new Vector3(), MovePlayerPacket.MODE_RESET);
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
            updateAttributesPacket.EntityRuntimeId = this.id;
            updateAttributesPacket.Attributes = atts;
            this.SendPacket(updateAttributesPacket);
        }

        public void SendPosition(Vector3 pos, Vector2 yawPitch, byte mode)
        {
            MovePlayerPacket pk = new MovePlayerPacket();
            pk.EntityRuntimeId = this.id;
            pk.Pos = pos;
            pk.YawPitchHead = new Vector3(yawPitch.X, yawPitch.Y, yawPitch.X);
            pk.Mode = mode;

            SendPacket(pk);
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

        public new PlayerInventory GetInventory()
        {
            return this.inventory;
        }

        public void OpenInventory(Inventory inventory)
        {
            inventory.Open(this);
            this.inventory.OpenInventory(inventory);
        }

        public void CloseInventory(Inventory inventory)
        {
            inventory.Close(this);
            this.inventory.CloseInventory();
        }
    }
}
