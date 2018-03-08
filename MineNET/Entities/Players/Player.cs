using System.IO;
using System.Net;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Entities.Data;
using MineNET.Events.PlayerEvents;
using MineNET.Inventories;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Data;

namespace MineNET.Entities.Players
{
    public partial class Player : EntityHuman, CommandSender, InventoryHolder
    {
        public override string Name { get; protected set; }

        public IPEndPoint EndPoint { get; internal set; }

        public LoginData LoginData { get; internal set; }
        public ClientData ClientData { get; internal set; }

        public override bool IsPlayer { get { return true; } }

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

        private async void SendFastChunk()
        {
            await Task.Run(() =>
            {
                for (int i = ((int) this.X >> 4) - this.RequestChunkRadius; i < ((int) this.X >> 4) + this.RequestChunkRadius; ++i)
                {
                    for (int j = ((int) this.Z >> 4) - this.RequestChunkRadius; j < ((int) this.Z >> 4) + this.RequestChunkRadius; ++j)
                    {
                        new Chunk(i, j).TestChunkSend(this);
                    }
                }

                this.SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);
            });

            this.HasSpawned = true;

            GameRules rules = new GameRules();
            rules.Add(new GameRule<bool>("ShowCoordinates", true));

            GameRulesChangedPacket gameRulesChangedPacket = new GameRulesChangedPacket();
            gameRulesChangedPacket.GameRules = rules;
            this.SendPacket(gameRulesChangedPacket);
            this.SendDataProperties();
            PlayerListEntry entry = new PlayerListEntry(this.LoginData.ClientUUID, this.EntityID, this.Name, this.ClientData.DeviceOS, new Skin("", new byte[0], new byte[0], "", ""), this.LoginData.XUID);
            Server.Instance.AddPlayer(this, entry);
        }

        private void LoadData()
        {
            string path = $"{Server.ExecutePath}\\players\\{this.Name}.dat";
            if (!File.Exists(path))
            {
                NBTIO.WriteGZIPFile(path, new CompoundTag(), NBTEndian.BIG_ENDIAN);
            }
            this.namedTag = NBTIO.ReadGZIPFile(path, NBTEndian.BIG_ENDIAN);
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

        internal override void OnUpdate()
        {

        }
    }
}
