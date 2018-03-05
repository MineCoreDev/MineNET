using System.Net;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Events.PlayerEvents;
using MineNET.Inventories;
using MineNET.Network.Packets;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Data;

namespace MineNET.Entities
{
    public class Player : EntityHuman, CommandSender, InventoryHolder
    {
        public const float GRAVITY = 9.8f;

        private PlayerInventory inventory;

        public Player()
        {
            this.IsPlayer = true;
            this.inventory = new PlayerInventory(this);
        }

        public override string Name { get; protected set; }

        public IPEndPoint EndPoint { get; internal set; }

        public LoginData LoginData { get; set; }
        public ClientData ClientData { get; set; }

        public bool IsPreLogined { get; private set; }
        public bool IsLogined { get; private set; }
        public bool HaveAllPacks { get; private set; }
        public bool PackSyncCompleted { get; private set; }
        public bool HasSpawned { get; private set; }

        internal void PacketHandle(DataPacket pk)
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
                this.MovePlayerPacketHandle((MovePlayerPacket) pk);
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

            PlayerPreLoginEventArgs playerPreLoginEvent = new PlayerPreLoginEventArgs(this, "");
            PlayerEvents.OnPlayerPreLogin(playerPreLoginEvent);
            if (playerPreLoginEvent.IsCancel)
            {
                this.Close(playerPreLoginEvent.KickMessage);
                return;
            }

            IsPreLogined = true;

            this.LoginData = pk.LoginData;
            this.Name = pk.LoginData.DisplayName;

            this.ClientData = pk.ClientData;

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
                //TODO: ResourcePackDataInfoPacket
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_HAVE_ALL_PACKS)
            {
                ResourcePackStackPacket resourcePackStackPacket = new ResourcePackStackPacket();
                this.SendPacket(resourcePackStackPacket);

                HaveAllPacks = true;
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_COMPLETED && HaveAllPacks)
            {
                PackSyncCompleted = true;

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

            for (int i = ((int) this.X >> 4) - chunkSize; i < ((int) this.X >> 4) + chunkSize; ++i)
            {
                for (int j = ((int) this.Z >> 4) - chunkSize; j < ((int) this.Z >> 4) + chunkSize; ++j)
                {
                    new Chunk(i, j).TestChunkSend(this);
                }
            }

            this.SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);

            this.HasSpawned = true;

            GameRules rules = new GameRules();
            rules.Add(new GameRule<bool>("ShowCoordinates", true));

            GameRulesChangedPacket gameRulesChangedPacket = new GameRulesChangedPacket();
            gameRulesChangedPacket.GameRules = rules;
            this.SendPacket(gameRulesChangedPacket);
        }

        public void MovePlayerPacketHandle(MovePlayerPacket pk)
        {
            //TODO: MoveCheck...
            Vector3 pos = pk.Pos;
            Vector3 direction = pk.Direction;
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = pos.Z;
            this.Pitch = direction.X;
            this.Yaw = direction.Y;
            //this.SendPosition(pos, direction, MovePlayerPacket.MODE_RESET);
        }

        private void ProcessLogin()
        {
            //TODO: PlayerDataLoad
            this.X = 128;
            this.Y = 6;
            this.Z = 128;

            StartGamePacket startGamePacket = new StartGamePacket();
            startGamePacket.EntityUniqueId = this.EntityID;
            startGamePacket.EntityRuntimeId = this.EntityID;
            startGamePacket.PlayerGamemode = 1;
            startGamePacket.PlayerPosition = new Vector3(this.X, this.Y, this.Z);
            startGamePacket.Direction = new Vector2(this.Yaw, this.Pitch);
            startGamePacket.WorldGamemode = 0;
            startGamePacket.Difficulty = 1;
            startGamePacket.SpawnX = 128;
            startGamePacket.SpawnY = 6;
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
            adventureSettingsPacket.EntityUniqueId = this.EntityID;
            this.SendPacket(adventureSettingsPacket);

            //SetEntityData
            //InventoryContent
            //MobArmorEquipment
            //inventoryContent
            //MobEquipment
            //InventorySlot
            //PlayerList
        }

        private int FixRadius(int radius)
        {
            int maxRequest = Server.ServerConfig.ViewDistance;
            if (radius > maxRequest) radius = maxRequest;
            return radius;
        }

        public void SendPlayStatus(int status)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            this.SendPacket(pk);
        }

        public void SendPlayerAttribute()
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
            updateAttributesPacket.EntityRuntimeId = this.EntityID;
            updateAttributesPacket.Attributes = atts;
            this.SendPacket(updateAttributesPacket);
        }

        public void SendPosition(Vector3 pos, Vector2 yawPitch, byte mode)
        {
            MovePlayerPacket pk = new MovePlayerPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.Pos = pos;
            pk.Direction = new Vector3(yawPitch.X, yawPitch.Y, yawPitch.X);
            pk.Mode = mode;

            SendPacket(pk);
        }

        public void SendPacket(DataPacket pk, bool needACK = false, bool immediate = false)
        {
            Server.Instance.NetworkManager.SendPacket(this, pk);
        }

        public void Close(string reason)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();//TODO NotQueue Send...
                pk.Message = reason;

                this.SendPacket(pk);
            }
            Server.Instance.NetworkManager.PlayerClose(this.EndPoint, reason);
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

        public override void SetMotion(Vector3 motion)
        {
            SetEntityMotionPacket pk = new SetEntityMotionPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.Motion = motion;

            SendPacket(pk);

            base.SetMotion(motion);
        }

        internal override void OnUpdate()
        {
            this.SetMotion(new Vector3(0, -0.05f, 0));
        }
    }
}
