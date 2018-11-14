using System;
using System.Collections.Generic;
using System.Linq;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
using MineNET.IO;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    /// <summary>
    /// Minecraft に存在するエンティティーを提供するクラス。
    /// </summary>
    public abstract partial class Entity : ILocation
    {
        private static long nextEntityId = 0;

        /// <summary>
        /// 定義されている　<see cref="Entity"/> を生成します。
        /// </summary>
        /// <param name="type"><see cref="Entity"/> のID</param>
        /// <param name="chunk"><see cref="Entity"/> を設置するチャンク</param>
        /// <param name="nbt"><see cref="Entity"/> のNBT</param>
        /// <returns>生成した <see cref="Entity"/></returns>
        public static Entity CreateEntity(string type, Chunk chunk, CompoundTag nbt)
        {
            if (MineNET_Registries.Entity.ContainsKey(type))
            {
                Type t = MineNET_Registries.Entity[type];
                return MineNET_Registries.Entity.GetExpression(type)(chunk, nbt);
            }
            else
            {
                throw new KeyNotFoundException(type);
            }
        }

        /// <summary>
        /// <see cref="Entity"/> を生成するために必要な <see cref="CompoundTag"/> を生成します
        /// </summary>
        /// <param name="pos"><see cref="Entity"/> の座標</param>
        /// <param name="motion"><see cref="Entity"/> の動き</param>
        /// <param name="rotation"><see cref="Entity"/> の向き</param>
        /// <returns>取得した <see cref="CompoundTag"/></returns>
        public static CompoundTag CreateEntityNBT(Vector3 pos, Vector3 motion = new Vector3(), Vector2 rotation = new Vector2())
        {
            CompoundTag nbt = new CompoundTag()
                .PutList(new ListTag("Pos", NBTTagType.FLOAT)
                    .Add(new FloatTag("", pos.X))
                    .Add(new FloatTag("", pos.Y))
                    .Add(new FloatTag("", pos.Z)))
                .PutList(new ListTag("Motion", NBTTagType.FLOAT)
                    .Add(new FloatTag("", motion.X))
                    .Add(new FloatTag("", motion.Y))
                    .Add(new FloatTag("", motion.Z)))
                .PutList(new ListTag("Rotation", NBTTagType.FLOAT)
                    .Add(new FloatTag("", motion.X))
                    .Add(new FloatTag("", motion.Y)));
            return nbt;
        }

        /// <summary>
        /// <see cref="Entity"/>　のネットワークID
        /// </summary>
        public abstract int NetworkId { get; }

        /// <summary>
        /// <see cref="Entity"/> の名前
        /// </summary>
        public abstract string Name { get; protected set; }
        /// <summary>
        /// <see cref="Entity"/> を<see cref="Worlds.World"/> に保存する際の保存ID
        /// </summary>
        public abstract string SaveId { get; }

        /// <summary>
        /// <see cref="Entity"/> の幅
        /// </summary>
        public virtual float Width { get; }
        /// <summary>
        /// <see cref="Entity"/> の高さ
        /// </summary>
        public virtual float Height { get; }
        /// <summary>
        /// <see cref="Entity"/> の重力
        /// </summary>
        public virtual float Gravity { get; }
        /// <summary>
        /// <see cref="Entity"/> のモーションの摩擦
        /// </summary>
        public virtual float Drag { get; }

        /// <summary>
        /// <see cref="Entity"/> のX座標
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// <see cref="Entity"/> のY座標
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// <see cref="Entity"/> のZ座標
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// <see cref="Entity"/> の存在する <see cref="Worlds.World"/>
        /// </summary>
        public World World { get; protected set; }

        /// <summary>
        /// <see cref="Entity"/> の存在する <see cref="Worlds.Chunk"/>
        /// </summary>
        public Chunk Chunk { get; private set; }

        /// <summary>
        /// <see cref="Entity"/> の横方向の角度
        /// </summary>
        public float Yaw { get; set; }
        /// <summary>
        /// <see cref="Entity"/> の縦方向の角度
        /// </summary>
        public float Pitch { get; set; }

        /// <summary>
        /// <see cref="Entity"/> の顔の横方向の角度
        /// </summary>
        public float HeadYaw { get; set; }

        /// <summary>
        /// <see cref="Entity"/> の当たり判定の大きさ
        /// </summary>
        public AxisAlignedBB BoundingBox { get; protected set; } = AxisAlignedBB.None;

        /// <summary>
        /// <see cref="Entity"/> のX座標へのモーション
        /// </summary>
        public float MotionX { get; set; }
        /// <summary>
        /// <see cref="Entity"/> のY座標へのモーション
        /// </summary>
        public float MotionY { get; set; }
        /// <summary>
        /// <see cref="Entity"/> のZ座標へのモーション
        /// </summary>
        public float MotionZ { get; set; }

        public Vector3 LastMotion { get; protected set; }
        public Location LastLocation { get; protected set; }

        /// <summary>
        /// <see cref="Entity"/> のランタイムなID
        /// </summary>
        public long EntityID { get; }

        public virtual float StepHeight { get; }

        public float YSize { get; protected set; }

        public virtual float BaseOffset { get; }

        public virtual bool ApplyDragBeforeGravity { get; } = false;

        /// <summary>
        /// このクラスが <see cref="Player"/> の場合 <see langword="true"/> を、それ以外の場合は <see langword="false"/> を返します
        /// </summary>
        public virtual bool IsPlayer
        {
            get { return false; }
        }

        /// <summary>
        /// <see cref="Entity"/> が既に削除されている場合は <see langword="true"/> を返します
        /// </summary>
        public bool Closed { get; protected set; }

        private Dictionary<long, Player> _hasSpawned = new Dictionary<long, Player>();

        /// <summary>
        /// <see cref="Entity"/> のメタデータを取得します
        /// </summary>
        public EntityMetadataManager DataProperties { get; private set; }
        /// <summary>
        /// <see cref="Entity"/> のHPなどのデータを取得します
        /// </summary>
        public EntityAttributeDictionary Attributes { get; private set; }

        /// <summary>
        /// <see cref="Entity"/> クラスの新しいインスタンスを作成します
        /// </summary>
        /// <param name="chunk"> <see cref="Entity"/> を作成するための <see cref="Worlds.Chunk"/> </param>
        /// <param name="nbt"> <see cref="Entity"/> を作成するための <see cref="CompoundTag"/> </param>
        public Entity(Chunk chunk, CompoundTag nbt)
        {
            this.EntityID = ++nextEntityId;

            this.Chunk = chunk;
            this.World = chunk.World;

            if (nbt != null)
            {
                this.EntityInit(nbt);

            }
            this.World.AddEntity(this);
        }

        /// <summary>
        /// <see cref="Entity"/> の初期化をします。
        /// </summary>
        /// <param name="nbt"><see cref="Entity"/> のNBTデータ</param>
        protected virtual void EntityInit(CompoundTag nbt)
        {
            ListTag list = nbt.GetList("Pos");
            this.X = ((FloatTag) list[0]).Data;
            this.Y = ((FloatTag) list[1]).Data;
            this.Z = ((FloatTag) list[2]).Data;
            list = nbt.GetList("Motion");
            this.MotionX = ((FloatTag) list[0]).Data;
            this.MotionY = ((FloatTag) list[1]).Data;
            this.MotionZ = ((FloatTag) list[2]).Data;
            list = nbt.GetList("Rotation");
            this.Yaw = ((FloatTag) list[0]).Data;
            this.Pitch = ((FloatTag) list[1]).Data;

            this.ResetLastMovements();
            this.RecalculateBoundingBox();

            this.DataProperties = new EntityMetadataManager(this.EntityID);
            this.SetDataProperty(new EntityDataLong(DATA_FLAGS, 0));
            this.SetDataProperty(new EntityDataShort(DATA_AIR, 400));
            this.SetDataProperty(new EntityDataShort(DATA_MAX_AIR, 400));
            this.SetDataProperty(new EntityDataString(DATA_NAMETAG, ""));
            this.SetDataProperty(new EntityDataLong(DATA_LEAD_HOLDER_EID, -1));
            this.SetDataProperty(new EntityDataFloat(DATA_SCALE, 1.0f));
            this.SetDataProperty(new EntityDataFloat(DATA_BOUNDING_BOX_WIDTH, this.Width));
            this.SetDataProperty(new EntityDataFloat(DATA_BOUNDING_BOX_HEIGHT, this.Height));

            this.SetFlag(DATA_FLAGS, DATA_FLAG_HAS_COLLISION);
            this.SetFlag(DATA_FLAGS, DATA_FLAG_GRAVITY);

            this.Attributes = new EntityAttributeDictionary(this.EntityID);
        }

        protected void ResetLastMovements()
        {
            this.LastLocation = this.ToLocation();
            this.LastMotion = this.GetMotion();
        }

        internal virtual bool UpdateTick(long tick)
        {
            if (this.Closed)
            {
                return false;
            }

            Logger.Info($"x {this.X} : y {this.Y} : z {this.Z}");
            
            this.TryChangeMovement();
            if (Math.Abs(this.MotionX) <= 0.00001f)
            {
                this.MotionX = 0;
            }
            if (Math.Abs(this.MotionY) <= 0.00001f)
            {
                this.MotionY = 0;
            }
            if (Math.Abs(this.MotionZ) <= 0.00001f)
            {
                this.MotionZ = 0;
            }
            if (this.MotionX != 0 || this.MotionY != 0 || this.MotionZ != 0)
            {
                this.Move(this.MotionX, this.MotionY, this.MotionZ);
            }
            this.UpdateMovement();
            this.EntityBaseTick(tick);
            return true;
        }

        protected virtual bool EntityBaseTick(long tick = 1)
        {
            if (this.Y <= -16)
            {
                this.Kill(); //TODO
                return false;
            }
            return true;
        }

        /// <summary>
        /// <see cref="Entity"/> の存在する <see cref="Worlds.Chunk"/> を取得します
        /// </summary>
        /// <returns></returns>
        public Vector2 GetChunkVector()
        {
            return new Vector2(((int) this.X) >> 4, ((int) this.Z) >> 4);
        }

        /// <summary>
        /// <see cref="Entity"/> のNBTデータを取得します
        /// </summary>
        /// <returns> <see cref="Entity"/> のNBTを <see cref="CompoundTag"/> で取得します</returns>
        public virtual CompoundTag SaveNBT()
        {
            CompoundTag nbt = new CompoundTag();

            nbt.PutString("id", this.SaveId);

            nbt.PutList(new ListTag("Pos", NBTTagType.FLOAT)
                    .Add(new FloatTag("", this.X))
                    .Add(new FloatTag("", this.Y))
                    .Add(new FloatTag("", this.Z)));
            nbt.PutList(new ListTag("Motion", NBTTagType.FLOAT)
                    .Add(new FloatTag("", this.MotionX))
                    .Add(new FloatTag("", this.MotionY))
                    .Add(new FloatTag("", this.MotionZ)));
            nbt.PutList(new ListTag("Rotation", NBTTagType.FLOAT)
                    .Add(new FloatTag("", this.Yaw))
                    .Add(new FloatTag("", this.Pitch)));
            return nbt;
        }

        /// <summary>
        /// <see cref="Entity"/> を表示範囲内の <see cref="Player"/> に表示させます
        /// </summary>
        public virtual void SpawnToAll()
        {
            if (this.Chunk == null || this.Closed)
            {
                return;
            }
            Player[] players = this.World.GetPlayers(); //TODO : ChunkPlayers
            for (int i = 0; i < players.Length; ++i)
            {
                Player player = players[i];
                if (this.IsPlayer && this.Name == player.Name)
                {
                    continue;
                }
                if (player.IsOnline)
                {
                    this.SpawnTo(player);
                }
            }
        }

        /// <summary>
        /// <see cref="Entity"/> を指定した <see cref="Player"/> に表示させます
        /// </summary>
        /// <param name="player"></param>
        public virtual void SpawnTo(Player player)
        {
            this.SendSpawnPacket(player);

            if (!this._hasSpawned.ContainsKey(player.EntityID)) //TODO : UsedChunk?
            {
                this._hasSpawned[player.EntityID] = player;
            }
        }


        /// <summary>
        /// この <see cref="Entity"/> が表示されている全ての <see cref="Player"/> から表示されなくします
        /// </summary>
        public virtual void DespawnFromAll()
        {
            Player[] players = this.Viewers;
            for (int i = 0; i < players.Length; ++i)
            {
                this.DespawnFrom(players[i]);
            }
        }

        /// <summary>
        /// この <see cref="Entity"/> を指定した <see cref="Player"/> から表示されなくします
        /// </summary>
        /// <param name="player"></param>
        public virtual void DespawnFrom(Player player)
        {
            RemoveEntityPacket pk = new RemoveEntityPacket
            {
                EntityUniqueId = this.EntityID
            };

            player.SendPacket(pk);

            if (this._hasSpawned.ContainsKey(player.EntityID))
            {
                this._hasSpawned.Remove(player.EntityID);
            }
        }

        /// <summary>
        /// <see cref="Player"/> に <see cref="AddEntityPacket"/> を送信します。 
        /// </summary>
        /// <param name="player"></param>
        public virtual void SendSpawnPacket(Player player)
        {
            AddEntityPacket pk = new AddEntityPacket
            {
                EntityUniqueId = this.EntityID,
                EntityRuntimeId = this.EntityID,
                Type = this.NetworkId,
                Position = this.ToVector3(),
                Motion = new Vector3(),
                Direction = new Vector3(this.Yaw, this.Pitch, this.HeadYaw),
                Attributes = this.Attributes,
                Metadata = this.DataProperties
            };

            player.SendPacket(pk);
        }

        public void SendPacketViewers(MinecraftPacket packet)
        {
            Player[] players = this.Viewers;
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendPacket(packet);
            }
        }

        /// <summary>
        /// この <see cref="Entity"/> が表示されている <see cref="Player"/> の配列
        /// </summary>
        public Player[] Viewers
        {
            get
            {
                return this._hasSpawned.Values.ToArray();
            }
        }

        /// <summary>
        /// この <see cref="Entity"/> を倒します。
        /// </summary>
        public virtual void Kill()
        {
            this.Close();
        }

        public virtual void Close()
        {
            this.Closed = true;
        }

        public void RecalculateBoundingBox()
        {
            float half = this.Width / 2;
            this.BoundingBox = this.BoundingBox.SetBounds(this.X - half, this.Y, this.Z - half, this.X + half, this.Y + this.Height, this.Z + half);
        }

        protected void TryChangeMovement()
        {
            float friction = 1f - this.Drag;

            if (this.ApplyDragBeforeGravity)
            {
                this.MotionY *= friction;
            }

            this.MotionY -= this.Gravity;

            if (!this.ApplyDragBeforeGravity)
            {
                this.MotionY *= friction;
            }

            //TODO : OnGround

            this.MotionX *= friction;
            this.MotionZ *= friction;
        }

        public void Move(float x, float y, float z)
        {
            float movX = x;
            float movY = y;
            float movZ = z;

            this.YSize *= 0.4f;
            AxisAlignedBB bb = this.BoundingBox;

            AxisAlignedBB[] boundingboxes = this.World.GetCollisionCubes(this, this.BoundingBox = this.BoundingBox.Offset(x, y, z), false);
            
            for (int i = 0; i < boundingboxes.Length; ++i)
            {
                y = boundingboxes[i].CalculateYOffset(this.BoundingBox, y);
            }
            this.BoundingBox = this.BoundingBox.Offset(0, y, 0);

            for (int i = 0; i < boundingboxes.Length; ++i)
            {
                x = boundingboxes[i].CalculateXOffset(this.BoundingBox, x);
            }
            this.BoundingBox = this.BoundingBox.Offset(x, 0, 0);

            for (int i = 0; i < boundingboxes.Length; ++i)
            {
                z = boundingboxes[i].CalculateZOffset(this.BoundingBox, z);
            }
            this.BoundingBox = this.BoundingBox.Offset(0, 0, z);

            if (this.StepHeight > 0 && this.YSize < 0.05f && (movX != x || movZ != z))
            {
                float cx = x;
                float cy = y;
                float cz = z;
                x = movX;
                y = movY;
                z = movZ;

                AxisAlignedBB bb1 = this.BoundingBox;
                this.BoundingBox = bb;

                boundingboxes = this.World.GetCollisionCubes(this, this.BoundingBox = this.BoundingBox.AddCoord(x, y, z), false);

                for (int i = 0; i < boundingboxes.Length; ++i)
                {
                    y = boundingboxes[i].CalculateYOffset(this.BoundingBox, y);
                }
                this.BoundingBox = this.BoundingBox.Offset(0, y, 0);

                for (int i = 0; i < boundingboxes.Length; ++i)
                {
                    x = boundingboxes[i].CalculateXOffset(this.BoundingBox, x);
                }
                this.BoundingBox = this.BoundingBox.Offset(x, 0, 0);

                for (int i = 0; i < boundingboxes.Length; ++i)
                {
                    z = boundingboxes[i].CalculateZOffset(this.BoundingBox, z);
                }
                this.BoundingBox = this.BoundingBox.Offset(0, 0, z);

                if ((cx * cx + cz * cz) >= (x * x + z * z))
                {
                    x = cx;
                    y = cy;
                    z = cz;
                    this.BoundingBox = bb1;
                }
                else
                {
                    this.YSize += 0.5f;
                }
            }

            this.X = (this.BoundingBox.Position.X + this.BoundingBox.Size.X) / 2;
            this.Y = this.BoundingBox.Position.Y - this.YSize;
            this.Z = (this.BoundingBox.Position.Z + this.BoundingBox.Size.Z) / 2;

            if (movX != x)
            {
                this.MotionX = 0;
            }
            if (movY != y)
            {
                this.MotionY = 0;
            }
            if (movZ != z)
            {
                this.MotionZ = 0;
            }
        }

        public void UpdateMovement(bool teleport = false)
        {
            float diffPosition = this.DistanceSquared(this.LastLocation);
            float yaw = this.Yaw - this.LastLocation.Yaw;
            float pitch = this.Pitch - this.LastLocation.Pitch;
            float diffRotation = (yaw * yaw) + (pitch * pitch);

            float diffMotion = this.GetMotion().Subtract(this.LastMotion).LengthSquared();

            if (teleport || diffPosition > 0.0001f || diffRotation > 1f)
            {
                this.LastLocation = this.ToLocation();
                this.BroadcastMovement(teleport);
            }

            if (diffMotion > 0.0025f || (diffMotion > 0.0001 && this.GetMotion().LengthSquared() <= 0.0001f))
            {
                this.LastMotion = this.GetMotion();
                this.BoradcastMotion();
            }
        }

        public Vector3 GetOffsetPosition(Vector3 pos)
        {
            return new Vector3(pos.X, pos.Y + this.BaseOffset, pos.Z);
        }

        protected void BroadcastMovement(bool teleport = false)
        {
            MoveEntityAbsolutePacket pk = new MoveEntityAbsolutePacket
            {
                EntityRuntimeId = this.EntityID,
                Position = this.GetOffsetPosition(this.ToVector3()),
                XRot = this.Yaw,
                YRot = this.Pitch,
                ZRot = this.HeadYaw
            };

            if (teleport)
            {
                pk.Flags |= MoveEntityAbsolutePacket.FLAG_TELEPORT;
            }
            //TODO : OnGround
            this.SendPacketViewers(pk);
        }

        protected void BoradcastMotion()
        {
            SetEntityMotionPacket pk = new SetEntityMotionPacket
            {
                EntityRuntimeId = this.EntityID,
                Motion = this.GetMotion()
            };
            this.SendPacketViewers(pk);
        }

        public Vector2 GetRotateVector2()
        {
            return new Vector2(this.Yaw, this.Pitch);
        }

        public Vector3 GetMotion()
        {
            return new Vector3(this.MotionX, this.MotionY, this.MotionZ);
        }

        public BlockFace GetDirection()
        {
            float rotation = this.Yaw % 360;
            if (rotation < 0)
            {
                rotation += 360;
            }

            if ((0 <= rotation && rotation < 45) || (315 <= rotation && rotation < 360))
            {
                return BlockFace.SOUTH;
            }
            else if (45 <= rotation && rotation < 135)
            {
                return BlockFace.WEST;
            }
            else if (135 <= rotation && rotation < 225)
            {
                return BlockFace.NORTH;
            }
            else if (225 <= rotation && rotation < 315)
            {
                return BlockFace.EAST;
            }
            return BlockFace.NORTH;
        }

        public Vector2 GetDirectionPlane()
        {
            return new Vector2((float) -Math.Cos(this.Yaw * Math.PI / 180 - Math.PI / 2),
                (float) -Math.Sin(this.Yaw * Math.PI / 180 - Math.PI / 2)).Normalized;
        }

        public Vector3 GetDirectionVector()
        {
            float y = (float) -Math.Sin(this.Pitch * (Math.PI / 180));
            double xz = Math.Cos(this.Pitch * (Math.PI / 180));
            float x = (float) (-xz * Math.Sin(this.Yaw * (Math.PI / 180)));
            float z = (float) (xz * Math.Cos(this.Yaw * (Math.PI / 180)));
            return new Vector3(x, y, z).Normalize();
        }
    }
}