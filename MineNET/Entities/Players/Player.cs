using System.Net;
using MineNET.Commands;
using MineNET.Entities.Attributes;
using MineNET.Entities.Data;
using MineNET.Events.PlayerEvents;
using MineNET.Inventories;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.Network.Packets;
using MineNET.Network.Packets.Data;
using MineNET.Values;

namespace MineNET.Entities.Players
{
    public partial class Player : EntityHuman, CommandSender, InventoryHolder
    {
        public override string Name { get; protected set; }
        public override bool IsPlayer { get { return true; } }

        public IPEndPoint EndPoint { get; internal set; }

        public LoginData LoginData { get; internal set; }
        public ClientData ClientData { get; internal set; }

        public bool IsPreLogined { get; private set; }
        public bool IsLogined { get; private set; }
        public bool HaveAllPacks { get; private set; }
        public bool PackSyncCompleted { get; private set; }
        public bool HasSpawned { get; private set; }

        private GameMode gameMode = GameMode.Survival;
        public GameMode GameMode
        {
            get
            {
                return this.gameMode;
            }

            set
            {
                this.gameMode = value;
                this.SendGameMode();
            }
        }

        public int RequestChunkRadius { get; private set; } = 5;

        public override float WIDTH
        {
            get
            {
                return 0.60f;
            }
        }

        public override float HEIGHT
        {
            get
            {
                return 1.80f;
            }
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

        public void SendPacket(DataPacket pk, bool immediate = false)
        {
            Server.Instance.NetworkManager.SendPacket(this, pk, immediate);
        }

        public void Close(string reason, bool clientDisconnect = false)
        {
            PlayerQuitEventArgs playerQuitEvent = new PlayerQuitEventArgs(this, "", reason);
            PlayerEvents.OnPlayerQuit(playerQuitEvent);
            reason = playerQuitEvent.Reason;
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                this.SendPacket(pk, true);
            }
            this.Save();

            this.World.UnLoadChunks(this);

            this.Closed = true;

            Server.Instance.RemovePlayer(this.EntityID);

            if (!clientDisconnect)
            {
                Server.Instance.NetworkManager.PlayerClose(this.EndPoint, reason);
            }
        }

        public void Save()
        {
            if (this.HasSpawned)
            {
                this.namedTag.PutInt("PlayerGameMode", this.GameMode.GameModeToInt());

                string path = $"{Server.ExecutePath}\\players\\{this.Name}.dat";
                NBTIO.WriteGZIPFile(path, this.namedTag, NBTEndian.BIG_ENDIAN);
            }
        }

        public new PlayerInventory Inventory
        {
            get
            {
                return (PlayerInventory) base.Inventory;
            }

            protected set
            {
                base.Inventory = value;
            }
        }

        public void OpenInventory(Inventory inventory)
        {
            inventory.Open(this);
            this.Inventory.OpenInventory(inventory);
        }

        public void CloseInventory(Inventory inventory)
        {
            inventory.Close(this);
            this.Inventory.CloseInventory();
        }

        public override void SetMotion(Vector3 motion)
        {
            SetEntityMotionPacket pk = new SetEntityMotionPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.Motion = motion;

            SendPacket(pk);

            base.SetMotion(motion);
        }

        public Skin GetSkin()
        {
            return this.ClientData.Skin;
        }

        public void SetSkin(Skin skin)
        {
            //TODO: 
        }

        public void SendMessage(string message)
        {
            TextPacket pk = new TextPacket();
            pk.Type = TextPacket.TYPE_RAW;
            pk.Message = message;
            this.SendPacket(pk);
        }

        public bool IsSurvival
        {
            get
            {
                return this.GameMode == GameMode.Survival;
            }
        }

        public bool IsCreative
        {
            get
            {
                return this.GameMode == GameMode.Creative;
            }
        }

        public bool IsAdventure
        {
            get
            {
                return this.GameMode == GameMode.Adventure;
            }
        }

        public bool IsSpectator
        {
            get
            {
                return this.GameMode == GameMode.Spectator;
            }
        }

        public void SendAllInventories()
        {
            this.Inventory.SendContents(this);
            this.Inventory.ArmorInventory.SendContents(this);
            this.Inventory.PlayerCursorInventory.SendContents(this);
            this.Inventory.PlayerOffhandInventory.SendContents(this);
            if (this.Inventory.OpendInventory != null)
            {
                this.Inventory.OpendInventory.SendContents(this);
            }
        }

        public bool CanInteract(Vector3 pos, double maxDistance)
        {
            if (Vector3.DistanceSquared(this.Vector3, pos) > maxDistance * maxDistance)
            {
                return false;
            }
            Vector2 dv = this.DirectionPlane;
            float dot1 = Vector2.Dot(dv, new Vector2(this.X, this.Z));
            float dot2 = Vector2.Dot(dv, new Vector2(pos.X, this.Z));
            return (dot2 - dot1) >= -0.5;
        }
    }
}
