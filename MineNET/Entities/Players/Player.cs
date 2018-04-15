using System.Collections.Generic;
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
using MineNET.Utils;
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

        public int RequestChunkRadius { get; private set; } = 5;

        public override void EntityInit()
        {
            base.EntityInit();

            this.Attributes.AddAttribute(EntityAttribute.HUNGER);
            this.Attributes.AddAttribute(EntityAttribute.SATURATION);
            this.Attributes.AddAttribute(EntityAttribute.EXHAUSTION);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE_LEVEL);
        }

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

        public override float Health
        {
            get
            {
                return base.Health;
            }

            set
            {
                base.Health = value;
                this.Attributes.Update(this);
            }
        }

        public override float Absorption
        {
            get
            {
                return base.Absorption;
            }

            set
            {
                base.Absorption = value;
                this.Attributes.Update(this);
            }
        }

        public float Hunger
        {
            get
            {
                return this.Attributes.GetAttribute("minecraft:player.hunger").Value;
            }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:player.hunger");
                attribute.Value = value;
                this.Attributes.AddAttribute(attribute);
                this.Attributes.Update(this);
                if (attribute.Value <= 6 && this.Sprinting)
                {
                    this.Sprinting = false;
                }
            }
        }

        public void AddHunger(float value)
        {
            float hunger = this.Hunger + value;
            this.Hunger = hunger;
        }

        public void TakeHunger(float value)
        {
            float hunger = this.Hunger - value;
            this.Hunger = hunger;
        }

        public float Saturation
        {
            get
            {
                return this.Attributes.GetAttribute("minecraft:player.saturation").Value;
            }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:player.saturation");
                attribute.Value = value;
                this.Attributes.AddAttribute(attribute);
                this.Attributes.Update(this);
            }
        }

        public void AddSaturation(float value)
        {
            float saturation = this.Saturation + value;
            this.Saturation = saturation;
        }

        public void TakeSaturation(float value)
        {
            float saturation = this.Saturation - value;
            this.Saturation = saturation;
        }

        public float Exhaustion
        {
            get
            {
                return this.Attributes.GetAttribute("minecraft:player.exhaustion").Value;
            }

            set
            {
                EntityAttribute attribute = this.Attributes.GetAttribute("minecraft:player.exhaustion");
                attribute.Value = value;
                this.Attributes.AddAttribute(attribute);
                this.Attributes.Update(this);
            }
        }

        public void AddExhaustion(float amount, int cause)
        {
            if (this.IsCreative || this.IsSpectator)
            {
                return;
            }
            PlayerExhaustEventArgs playerExhaustEvent = new PlayerExhaustEventArgs(this, amount, cause);
            PlayerEvents.OnPlayerExhaust(playerExhaustEvent);
            if (playerExhaustEvent.IsCancel)
            {
                return;
            }
            float exhaustion = this.Exhaustion + playerExhaustEvent.Amount;
            while (exhaustion >= 4f)
            {
                exhaustion -= 4f;
                if (this.Saturation != 0)
                {
                    this.TakeSaturation(1f);
                }
                else
                {
                    this.TakeHunger(1f);
                }
            }
            this.Exhaustion = exhaustion;
        }

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

        public void SendPlayStatus(int status)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            this.SendPacket(pk);
        }

        public void SendPlayerAttribute()
        {
            this.Attributes.Update(this);
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
            PlayerQuitEventArgs playerQuitEvent = new PlayerQuitEventArgs(this, $"§e{this.Name} が世界を去りました", reason);
            PlayerEvents.OnPlayerQuit(playerQuitEvent);
            reason = playerQuitEvent.Reason;
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                this.SendPacket(pk, true);
            }
            if (!string.IsNullOrEmpty(playerQuitEvent.QuitMessage))
            {
                Server.Instance.BroadcastMessage(playerQuitEvent.QuitMessage);
            }
            Logger.Info($"§e{this.Name} left the game");

            this.Save();

            if (this.World != null)
            {
                this.World.UnLoadChunks(this);
            }

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
                this.SaveNBT();

                string path = $"{Server.ExecutePath}\\players\\{this.Name}.dat";
                NBTIO.WriteGZIPFile(path, this.NamedTag, NBTEndian.BIG_ENDIAN);
            }
        }

        public override void SaveNBT()
        {
            base.SaveNBT();

            this.Inventory.SaveNBT();

            this.NamedTag.PutInt("PlayerGameMode", this.GameMode.GameModeToInt());
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

        public void SendMessage(string message)
        {
            TextPacket pk = new TextPacket();
            pk.Type = TextPacket.TYPE_RAW;
            pk.Message = message;
            this.SendPacket(pk);
        }

        public void SendMessage(string message, params object[] args)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < args.Length; ++i)
            {
                list.Add(args[i].ToString());
            }

            TextPacket pk = new TextPacket();
            pk.Type = TextPacket.TYPE_TRANSLATION;
            pk.Message = message;
            pk.Parameters = list.ToArray();
            this.SendPacket(pk);
        }

        public void SendMessage(TranslationMessage message)
        {
            if (message.TranslationFills == null)
            {
                this.SendMessage($"{message.Header.ToString()}%{message.TranslationKey}", new string[0]);
            }
            else
            {
                this.SendMessage($"{message.Header.ToString()}%{message.TranslationKey}", message.TranslationFills);
            }
        }

        public void SendChat(string message, string xboxUserId = "")
        {
            TextPacket pk = new TextPacket();
            pk.Type = TextPacket.TYPE_CHAT;
            pk.Message = message;
            pk.XboxUserId = xboxUserId;
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

        public bool Whitelist
        {
            get
            {
                return Server.Instance.WhitelistConfig.ContainsKey(this.Name);
            }

            set
            {
                if (value)
                {
                    Server.Instance.WhitelistConfig.Set(this.Name, true);
                }
                else
                {
                    if (this.Whitelist)
                    {
                        Server.Instance.WhitelistConfig.Remove(this.Name);
                    }
                }
            }
        }

        public bool Ban
        {
            get
            {
                return Server.Instance.BanConfig.ContainsKey(this.Name);
            }

            set
            {
                if (value)
                {
                    Server.Instance.BanConfig.Set(this.Name, true);
                }
                else
                {
                    if (this.Whitelist)
                    {
                        Server.Instance.BanConfig.Remove(this.Name);
                    }
                }
            }
        }
    }
}
