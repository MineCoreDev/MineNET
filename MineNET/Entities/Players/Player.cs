using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Events.PlayerEvents;
using MineNET.Inventories;
using MineNET.Items;
using MineNET.NBT.Tags;
using MineNET.Network;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Text;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities.Players
{
    public partial class Player : EntityHuman, CommandSender
    {
        public override bool IsPlayer
        {
            get { return true; }
        }

        public override int NetworkId { get; } = EntityIDs.PLAYER;

        public override string Name { get; protected set; }
        public override string SaveId { get; } = "Player";
        public new string DisplayName { get; private set; }

        public IPEndPoint EndPoint { get; internal set; }

        public bool IsPreLogined { get; private set; }
        public bool IsLoggedIn { get; private set; }
        public LoginData LoginData { get; private set; }
        public ClientData ClientData { get; private set; }

        public PlayerListEntry PlayerListEntry { get; private set; }
        public AdventureSettingsEntry AdventureSettingsEntry { get; private set; }

        public bool PackSyncCompleted { get; private set; }
        public bool HaveAllPacks { get; private set; }

        public bool HasSpawned { get; private set; }
        public bool AnySendChunk { get; private set; }
        public int RequestChunkRadius { get; private set; } = 8;

        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnZ { get; set; }

        public override float Width { get; } = 0.60f;
        public override float Height { get; } = 1.80f;

        public ConcurrentDictionary<Tuple<int, int>, double> LoadedChunks { get; private set; } =
            new ConcurrentDictionary<Tuple<int, int>, double>();

        private GameMode gameMode;

        /// <summary>
        /// Minecraft に存在するプレイヤーを提供するクラス。
        /// </summary>
        public Player() : base(World.GetMainWorld().GetChunk(new Tuple<int, int>(128 >> 4, 128 >> 4)), null)
        {
        }

        protected override void EntityInit(CompoundTag nbt)
        {
            base.EntityInit(nbt);

            this.Attributes.AddAttribute(EntityAttribute.HUNGER);
            this.Attributes.AddAttribute(EntityAttribute.SATURATION);
            this.Attributes.AddAttribute(EntityAttribute.EXHAUSTION);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE_LEVEL);

            this.SetFlag(DATA_FLAGS, DATA_FLAG_BREATHING);
            this.SetFlag(DATA_FLAGS, DATA_FLAG_CAN_CLIMB);

            this.Inventory = new PlayerInventory(this);
            this.Inventory.LoadNBT(nbt);

            this.World = World.GetWorld(nbt.GetString("World")) ?? World.GetMainWorld();

            this.SpawnX = nbt.GetInt("SpawnX");
            this.SpawnY = nbt.GetInt("SpawnY");
            this.SpawnZ = nbt.GetInt("SpawnZ");

            this.AdventureSettingsEntry = new AdventureSettingsEntry();

            this.GameMode = GameModeExtention.FromIndex(nbt.GetInt("PlayerGameType"));
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度の最大値を取得します
        /// </summary>
        /// <returns></returns>
        public virtual float GetMaxHunger()
        {
            return this.Attributes.GetAttribute(EntityAttribute.HUNGER.Name).MaxValue;
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度を取得します
        /// </summary>
        /// <returns></returns>
        public virtual float GetHunger()
        {
            return this.Attributes.GetAttribute(EntityAttribute.HUNGER.Name).Value;
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度を設定します
        /// </summary>
        /// <param name="amount"></param>
        public virtual void SetHunger(float amount)
        {
            EntityAttribute attribute = this.Attributes.GetAttribute(EntityAttribute.HUNGER.Name);
            attribute.Value = amount;
            this.Attributes.SetAttribute(attribute);
            this.Attributes.Update(this);
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度を増やします
        /// </summary>
        /// <param name="amount"></param>
        public virtual void AddHunger(float amount)
        {
            this.SetHunger(this.GetHunger() + amount);
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度を減らします
        /// </summary>
        /// <param name="amount"></param>
        public virtual void ReduceHunger(float amount)
        {
            this.SetHunger(this.GetHunger() - amount);
        }

        /// <summary>
        /// <see cref="Player"/> の隠し満腹度の最大値を取得します
        /// </summary>
        /// <returns></returns>
        public virtual float GetMaxSaturation()
        {
            return this.Attributes.GetAttribute(EntityAttribute.SATURATION.Name).MaxValue;
        }

        /// <summary>
        /// <see cref="Player"/> の隠し満腹度を取得します
        /// </summary>
        /// <returns></returns>
        public virtual float GetSaturation()
        {
            return this.Attributes.GetAttribute(EntityAttribute.SATURATION.Name).Value;
        }

        /// <summary>
        /// <see cref="Player"/> の隠し満腹度を設定します
        /// </summary>
        /// <param name="amount"></param>
        public virtual void SetSaturation(float amount)
        {
            EntityAttribute attribute = this.Attributes.GetAttribute(EntityAttribute.SATURATION.Name);
            attribute.Value = amount;
            this.Attributes.SetAttribute(attribute);
            this.Attributes.Update(this);
        }

        /// <summary>
        /// <see cref="Player"/> の隠し満腹度を増やします
        /// </summary>
        /// <param name="amount"></param>
        public virtual void AddSaturation(float amount)
        {
            this.SetSaturation(this.GetSaturation() + amount);
        }

        /// <summary>
        /// <see cref="Player"/> の隠し満腹度を減らします
        /// </summary>
        /// <param name="amount"></param>
        public virtual void ReduceSaturation(float amount)
        {
            this.SetSaturation(this.GetSaturation() - amount);
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度消耗値を取得します
        /// </summary>
        /// <returns></returns>
        public virtual float GetExhaustion()
        {
            return this.Attributes.GetAttribute(EntityAttribute.EXHAUSTION.Name).Value;
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度消耗値を設定します
        /// </summary>
        /// <param name="amount"></param>
        public virtual void SetExhaustion(float amount)
        {
            EntityAttribute attribute = this.Attributes.GetAttribute(EntityAttribute.EXHAUSTION.Name);
            attribute.Value = amount;
            this.Attributes.SetAttribute(attribute);
            this.Attributes.Update(this);
        }

        /// <summary>
        /// <see cref="Player"/> の満腹度消耗値を増加させます
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cause"></param>
        public virtual void Exhaust(float amount, int cause = PlayerExhaustEventArgs.CAUSE_CUSTOM)
        {
            PlayerExhaustEventArgs args = new PlayerExhaustEventArgs(this, amount, cause);
            Server.Instance.Event.Player.OnPlayerExhaust(this, args);
            if (args.IsCancel)
            {
                return;
            }
            amount = args.Amount;

            float exhaustion = this.GetExhaustion() + amount;
            while (exhaustion >= 4f)
            {
                exhaustion -= 4f;
                float saturation = this.GetSaturation();
                if (saturation > 0)
                {
                    saturation = Math.Max(0, saturation - 1f);
                    this.SetSaturation(saturation);
                }
                else
                {
                    float hunger = this.GetHunger();
                    if (hunger > 0)
                    {
                        this.SetHunger(hunger - 1);
                    }
                }
            }
            this.SetExhaustion(exhaustion);
        }

        /// <summary>
        /// <see cref="Player"/> の経験値レベルを取得します
        /// </summary>
        /// <returns></returns>
        public int GetXpLevel()
        {
            return (int) this.Attributes.GetAttribute(EntityAttribute.EXPERIENCE_LEVEL.Name).Value;
        }

        /// <summary>
        /// <see cref="Player"/> の経験値レベルを設定します
        /// </summary>
        /// <param name="amount"></param>
        public void SetXpLevel(int amount)
        {
            this.TotalXp = this.GetNeedXp(amount) + (int) (this.GetXpFromLast(amount) * this.GetXpProgress());
            this.SetXpAndProgress(amount, null);
        }

        /// <summary>
        /// <see cref="Player"/> の経験値ゲージの割合を0～1で取得します
        /// </summary>
        /// <returns></returns>
        public float GetXpProgress()
        {
            return this.Attributes.GetAttribute(EntityAttribute.EXPERIENCE.Name).Value;
        }

        /// <summary>
        /// <see cref="Player"/> の経験値を増加させます
        /// </summary>
        /// <param name="amount"></param>
        public void AddXp(int amount)
        {
            this.TotalXp += amount;
            int level = this.GetXpLevel();
            while (true)
            {
                if (this.GetNeedXp(level + 1) <= this.TotalXp)
                {
                    level++;
                }
                else
                {
                    break;
                }
            }
            float progress = (this.TotalXp - this.GetNeedXp(level)) / (float) this.GetXpFromLast(level);
            this.SetXpAndProgress(level, progress);
        }

        public int TotalXp { get; protected set; }

        /// <summary>
        /// 引数の数値のレベルまでに必要な経験値を返します
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetNeedXp(int level)
        {
            if (level < 17)
            {
                return level * level + 6 * level;
            }
            else if (level < 32)
            {
                return (int) (2.5 * level * level - 40.5f * level + 360);
            }
            else
            {
                return (int) (4.5f * level * level - 162.5 * level + 2220);
            }
        }

        /// <summary>
        /// 次のレベルまでに必要な経験値を返します
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetXpFromLast(int level)
        {
            if (level < 16)
            {
                return 2 * level + 7;
            }
            if (level < 31)
            {
                return 5 * level - 38;
            }
            return 9 * level - 158;
        }

        /// <summary>
        /// 経験値ゲージのゲージとレベルを設定します
        /// </summary>
        /// <param name="level"></param>
        /// <param name="progress"></param>
        protected void SetXpAndProgress(int? level, float? progress)
        {
            if (level != null)
            {
                EntityAttribute attribute = this.Attributes.GetAttribute(EntityAttribute.EXPERIENCE_LEVEL.Name);
                attribute.Value = (float) level;
                this.Attributes.SetAttribute(attribute);
            }
            if (progress != null)
            {
                EntityAttribute attribute = this.Attributes.GetAttribute(EntityAttribute.EXPERIENCE.Name);
                attribute.Value = (float) progress;
                this.Attributes.SetAttribute(attribute);
            }
            this.Attributes.Update(this);
        }


        public void SendMessage(TranslationContainer message)
        {
            if (message.Args == null)
            {
                this.SendMessage(message.GetText(), new object[0]);
            }
            else
            {
                this.SendMessage(message.GetText(), message.Args);
            }
        }

        public void SendMessage(string message)
        {
            TextPacket pk = new TextPacket
            {
                Type = TextPacket.TYPE_RAW,
                Message = message
            };
            this.SendPacket(pk);
        }

        public void SendMessage(string message, params object[] args)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < args.Length; ++i)
            {
                list.Add(args[i].ToString());
            }

            TextPacket pk = new TextPacket
            {
                Type = TextPacket.TYPE_TRANSLATION,
                NeedsTranslation = true,
                Message = message,
                Parameters = list.ToArray()
            };
            this.SendPacket(pk);
        }

        public void SendPlayStatus(int status, int flag = RakNetProtocol.FlagNormal)
        {
            PlayStatusPacket pk = new PlayStatusPacket
            {
                Status = status
            };

            this.SendPacket(pk, flag: flag);
        }

        public void SendChunkRadiusUpdated(int radius)
        {
            ChunkRadiusUpdatedPacket pk = new ChunkRadiusUpdatedPacket
            {
                Radius = radius
            };

            this.SendPacket(pk);

            this.RequestChunkRadius = radius;
        }

        public void SendChunk()
        {
            Task.Run(() =>
            {
                Thread.CurrentThread.Name = "ChunkSendThread";
                List<FullChunkDataPacket> Chunks = new List<FullChunkDataPacket>();
                foreach (Chunk c in this.World.LoadChunks(this, this.RequestChunkRadius))
                {
                    Chunks.Add(c.ChunkData());
                }

                foreach (FullChunkDataPacket chunk in Chunks)
                {
                    this.SendPacket(chunk, RakNetPacketReliability.RELIABLE_ORDERED, RakNetProtocol.FlagImmediate);
                }
            });
        }

        public void SendPacket(MinecraftPacket packet, int reliability = RakNetPacketReliability.RELIABLE, int flag = RakNetProtocol.FlagNormal)
        {
            NetworkSession session = this.GetSession();
            if (session == null)
            {
                return;
            }

            session.AddPacketBatchQueue(packet, reliability, flag);
        }

        public void SendAvailableCommands()
        {
            AvailableCommandsPacket availableCommandsPacket = new AvailableCommandsPacket
            {
                Commands = MineNET_Registries.Command.ToDictionary()
            };
            this.SendPacket(availableCommandsPacket);
        }

        public void SendAllInventories()
        {
            this.Inventory.SendContents(this);
            this.Inventory.ArmorInventory.SendContents(this);
            this.Inventory.EntityOffhandInventory.SendContents(this);
            this.Inventory.PlayerCursorInventory.SendContents(this);
            this.Inventory.OpendInventory?.SendContents(this);
        }

        internal override bool UpdateTick(long tick)
        {
            if (tick % 20 == 0 && this.AnySendChunk && !this.Closed)
            {
                this.SendChunk();
            }

            return true;
        }

        public bool IsOnline
        {
            get
            {
                return this.GetSession() != null && this.IsLoggedIn;
            }
        }

        public NetworkSession GetSession()
        {
            return Server.Instance.Network.GetSession(this.EndPoint);
        }

        #region Close Player Method

        public void Close(string reason)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                this.SendPacket(pk, flag: RakNetProtocol.FlagImmediate);
            }

            this.Close();

            Server.Instance.Network.GetSession(this.EndPoint)?.Disconnect(reason);
        }

        public override void Close()
        {
            if (this.HasSpawned)
            {
                this.Save();
            }

            this.World?.UnLoadChunks(this);
            this.World?.RemoveEntity(this);

            this.Closed = true;
        }

        #endregion

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

        #region Gamemode Property

        public GameMode GameMode
        {
            get { return this.gameMode; }

            set
            {
                this.gameMode = value;
                this.SendGameMode();
            }
        }

        public bool IsSurvival
        {
            get { return this.GameMode == GameMode.Survival; }
        }

        public bool IsCreative
        {
            get { return this.GameMode == GameMode.Creative; }
        }

        public bool IsAdventure
        {
            get { return this.GameMode == GameMode.Adventure; }
        }

        public bool IsSpectator
        {
            get { return this.GameMode == GameMode.Spectator; }
        }

        public void SendGameMode()
        {
            SetPlayerGameTypePacket pk = new SetPlayerGameTypePacket();
            pk.GameMode = this.GameMode;
            this.SendPacket(pk);

            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.BUILD_AND_MINE, !this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_BUILDER, !this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_CLIP, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_PVP, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.FLYING, this.IsCreative || this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, this.IsCreative || this.IsSpectator);
            this.AdventureSettingsEntry.Update(this);
        }

        #endregion

        #region Load & Save Method

        public void Load()
        {
            CompoundTag nbt = Server.Instance.GetOfflinePlayerData(this.LoginData.XUID);

            this.EntityInit(nbt);
        }

        public void Save()
        {
            CompoundTag nbt = this.SaveNBT();

            nbt.PutInt("PlayerGameType", this.GameMode.GetIndex());

            nbt.PutString("World", this.World.Name);

            nbt.PutInt("SpawnX", this.SpawnX);
            nbt.PutInt("SpawnY", this.SpawnY);
            nbt.PutInt("SpawnZ", this.SpawnZ);

            Dictionary<string, Tag> tags = this.Inventory.SaveNBT().Tags;
            foreach (string name in tags.Keys)
            {
                nbt.PutTag(name, tags[name]);
            }

            Server.Instance.SaveOfflinePlayerData(this.LoginData.XUID, nbt);
        }

        #endregion

        public bool CanInteract(Vector3 pos, double maxDistance)
        {
            if (Vector3.DistanceSquared(this.GetVector3(), pos) > maxDistance * maxDistance)
            {
                return false;
            }

            Vector2 dv = this.GetDirectionPlane();
            float dot1 = Vector2.Dot(dv, new Vector2(this.X, this.Z));
            float dot2 = Vector2.Dot(dv, new Vector2(pos.X, this.Z));
            return (dot2 - dot1) >= -0.5;
        }

        public bool DropItem(ItemStack item)
        {
            Vector3 pos = this.GetVector3().Add(0, 1.3f, 0);
            Vector3 motion = this.GetDirectionVector().Multiply(0.4f);

            this.World.DropItem(item, pos, motion);
            this.Action = true;
            this.SendDataProperties();
            return true;
        }
    }
}