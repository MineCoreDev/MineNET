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

        public int RequestChunkRadius = 5;

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

        public void Close(string reason)
        {
            PlayerQuitEventArgs playerQuitEvent = new PlayerQuitEventArgs(this, "", reason);
            PlayerEvents.OnPlayerQuit(playerQuitEvent);
            reason = playerQuitEvent.Reason;
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();//TODO NotQueue Send...
                pk.Message = reason;

                this.SendPacket(pk, true);
            }
            this.Save();
            Server.Instance.RemovePlayer(this.EntityID);
            Server.Instance.NetworkManager.PlayerClose(this.EndPoint, reason);
        }

        public void Save()
        {
            string path = $"{Server.ExecutePath}\\players\\{this.Name}.dat";
            NBTIO.WriteGZIPFile(path, this.namedTag, NBTEndian.BIG_ENDIAN);
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
    }
}
