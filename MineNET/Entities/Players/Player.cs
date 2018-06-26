using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Network;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MineNET.Entities.Players
{
    public class Player : EntityLiving, CommandSender
    {
        #region Property & Field
        public override bool IsPlayer
        {
            get
            {
                return true;
            }
        }

        public override string Name { get; protected set; }
        public new string DisplayName { get; private set; }

        public IPEndPoint EndPoint { get; internal set; }

        public bool IsPreLogined { get; private set; }
        public bool IsLogined { get; private set; }
        public LoginData LoginData { get; private set; }
        public ClientData ClientData { get; private set; }
        public Skin Skin { get; private set; }
        public UUID Uuid { get; private set; }

        public PlayerListEntry PlayerListEntry { get; private set; }
        public AdventureSettingsEntry AdventureSettingsEntry { get; private set; }

        public GameMode GameMode { get; private set; } = GameMode.Survival;

        public bool PackSyncCompleted { get; private set; }
        public bool HaveAllPacks { get; private set; }
        #endregion

        #region Ctor
        public Player() : base(null, null)
        {

        }
        #endregion

        #region Init Method
        protected override void EntityInit()
        {
            base.EntityInit();

            this.Attributes.AddAttribute(EntityAttribute.HUNGER);
            this.Attributes.AddAttribute(EntityAttribute.SATURATION);
            this.Attributes.AddAttribute(EntityAttribute.EXHAUSTION);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE_LEVEL);

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_BREATHING);
            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_CAN_CLIMB);
        }
        #endregion

        #region Send Message Method
        public void SendMessage(TranslationMessage message)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message, params object[] args)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Send Status Packet
        public void SendPlayStatus(int status, int flag = RakNetProtocol.FlagNormal)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            this.SendPacket(pk, flag: flag);
        }
        #endregion

        #region Send Packet Method
        public void SendPacket(MinecraftPacket packet, int reliability = RakNetPacketReliability.RELIABLE, int flag = RakNetProtocol.FlagNormal)
        {
            NetworkSession session = Server.Instance.Network.GetSession(this.EndPoint);
            if (session == null)
            {
                return;
            }

            session.AddPacketBatchQueue(packet, reliability, flag);
        }
        #endregion

        #region Packet Handle Method
        public void OnPacketHandle(MinecraftPacket packet)
        {
            if (packet is LoginPacket)
            {
                this.HandleLoginPacket((LoginPacket) packet);
            }
            else if (packet is ResourcePackClientResponsePacket)
            {
                this.HandleResourcePackClientResponsePacket((ResourcePackClientResponsePacket) packet);
            }
        }

        public void HandleLoginPacket(LoginPacket pk)
        {
            if (this.IsPreLogined)
            {
                return;
            }

            if (pk.Protocol < MinecraftProtocol.ClientProtocol)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_CLIENT, RakNetProtocol.FlagImmediate);
                this.Close("disconnectionScreen.outdatedClient");
                return;
            }
            else if (pk.Protocol > MinecraftProtocol.ClientProtocol)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_SERVER, RakNetProtocol.FlagImmediate);
                this.Close("disconnectionScreen.outdatedServer");
                return;
            }

            Player[] players = Server.Instance.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].GetHashCode() != this.GetHashCode())
                {
                    if (players[i].Name == this.Name)
                    {
                        this.Close("disconnectionScreen.loggedinOtherLocation");
                        return;
                    }
                }
            }

            //TODO: Auth MS Server

            this.LoginData = pk.LoginData;
            this.Name = pk.LoginData.DisplayName;
            this.DisplayName = this.Name;
            this.Uuid = this.LoginData.ClientUUID;

            this.ClientData = pk.ClientData;
            this.Skin = this.ClientData.Skin;

            //TODO: Event

            this.IsPreLogined = true;

            this.SendPlayStatus(PlayStatusPacket.LOGIN_SUCCESS);

            ResourcePacksInfoPacket info = new ResourcePacksInfoPacket();
            this.SendPacket(info);
        }

        public void HandleResourcePackClientResponsePacket(ResourcePackClientResponsePacket pk)
        {
            if (this.PackSyncCompleted)
            {
                return;
            }

            if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_REFUSED)
            {
                this.Close("disconnectionScreen.resourcePack");
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_SEND_PACKS)
            {
                //TODO: ResourcePackDataInfoPacket
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_HAVE_ALL_PACKS)
            {
                ResourcePackStackPacket resourcePackStackPacket = new ResourcePackStackPacket();
                this.SendPacket(resourcePackStackPacket);

                this.HaveAllPacks = true;
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_COMPLETED && this.HaveAllPacks)
            {
                if (this.IsLogined)
                {
                    return;
                }

                //TODO: Event

                this.IsLogined = true;

                //TODO: Load NBT

                this.World = Server.Instance.MainWorld;

                StartGamePacket startGamePacket = new StartGamePacket();
                startGamePacket.EntityUniqueId = this.EntityID;
                startGamePacket.EntityRuntimeId = this.EntityID;
                startGamePacket.PlayerGamemode = this.GameMode;
                startGamePacket.PlayerPosition = new Vector3(128, 5, 128);//new Vector3(this.X, this.Y, this.Z);
                startGamePacket.Direction = new Vector2(this.Yaw, this.Pitch);

                startGamePacket.WorldGamemode = this.World.Gamemode;
                startGamePacket.Difficulty = this.World.Difficulty;
                startGamePacket.SpawnX = this.World.SpawnX;
                startGamePacket.SpawnY = 5;//TODO: Safe Spawn
                startGamePacket.SpawnZ = this.World.SpawnZ;
                startGamePacket.WorldName = this.World.Name;

                startGamePacket.GameRules = new GameRules();
                startGamePacket.GameRules.Add(new GameRule<bool>("ShowCoordinates", true));
                this.SendPacket(startGamePacket);

                Dictionary<Tuple<int, int>, double> newOrders = new Dictionary<Tuple<int, int>, double>();
                int radius = 16;
                double radiusSquared = Math.Pow(radius, 2);
                Vector2 center = new Vector2(128 >> 4, 128 >> 4);

                for (int x = -radius; x <= radius; ++x)
                {
                    for (int z = -radius; z <= radius; ++z)
                    {
                        int distance = (x * x) + (z * z);
                        if (distance > radiusSquared)
                        {
                            continue;
                        }
                        int chunkX = (int) (x + center.X);
                        int chunkZ = (int) (z + center.Y);
                        Tuple<int, int> index = new Tuple<int, int>(chunkX, chunkZ);
                        newOrders[index] = distance;
                    }
                }

                this.PlayerListEntry = new PlayerListEntry(this.LoginData.ClientUUID)
                {
                    EntityUniqueId = this.EntityID,
                    Name = this.DisplayName,
                    PlatForm = this.ClientData.DeviceOS,
                    Skin = this.Skin,
                    UUID = this.Uuid,
                    XboxUserId = this.LoginData.XUID
                };
                this.PlayerListEntry.UpdateAll();

                AdventureSettingsEntry adventureSettingsEntry = new AdventureSettingsEntry();
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_PVP, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.AUTO_JUMP, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, true);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_CLIP, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.FLYING, false);
                adventureSettingsEntry.CommandPermission = PlayerPermissions.MEMBER;//this.Op ? PlayerPermissions.OPERATOR : PlayerPermissions.MEMBER;
                adventureSettingsEntry.PlayerPermission = PlayerPermissions.MEMBER;//this.Op ? PlayerPermissions.OPERATOR : PlayerPermissions.MEMBER;
                adventureSettingsEntry.EntityUniqueId = this.EntityID;
                this.AdventureSettingsEntry = adventureSettingsEntry;
                this.AdventureSettingsEntry.Update(this);

                this.SendDataProperties();
                this.Attributes.Update(this);

                this.SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);

                foreach (var pair in newOrders.OrderBy(pair => pair.Value))
                {
                    Chunk c = new Chunk(pair.Key.Item1, pair.Key.Item2);
                    for (int i = 0; i < 16; ++i)
                    {
                        for (int k = 0; k < 16; ++k)
                        {
                            c.SetBlock(i, 0, k, 2);
                        }
                    }
                    c.SendChunk(this);
                }
            }
        }
        #endregion

        #region Close Player Method
        public void Close(string reason)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                this.SendPacket(pk, flag: RakNetProtocol.FlagImmediate);
            }

            Server.Instance.Network.GetSession(this.EndPoint)?.Disconnect(reason);
        }
        #endregion
    }
}
