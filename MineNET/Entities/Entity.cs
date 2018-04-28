using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MineNET.Blocks.Data;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract partial class Entity : ILocation
    {
        private static long nextEntityId = 0;

        public EntityMetadataManager DataProperties { get; protected set; } = new EntityMetadataManager();

        protected List<Player> viewers = new List<Player>();

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public float HeadYaw { get; set; }

        public World World { get; set; }
        public Chunk Chunk { get; set; }

        public float MotionX { get; set; }
        public float MotionY { get; set; }
        public float MotionZ { get; set; }

        public CompoundTag NamedTag { get; protected set; } = new CompoundTag();

        public long EntityID { get; }

        public abstract float WIDTH { get; }
        public abstract float HEIGHT { get; }

        public virtual bool IsPlayer { get { return false; } }

        public abstract string Name { get; protected set; }

        public virtual bool Closed { get; protected set; }

        public Entity(World world, CompoundTag tag)
        {
            this.EntityID = ++Entity.nextEntityId;

            this.SetDataProperty(new EntityDataLong(Entity.DATA_FLAGS, 0));
            this.SetDataProperty(new EntityDataShort(Entity.DATA_AIR, 400));
            this.SetDataProperty(new EntityDataShort(Entity.DATA_MAX_AIR, 400));
            this.SetDataProperty(new EntityDataString(Entity.DATA_NAMETAG, ""));
            this.SetDataProperty(new EntityDataLong(Entity.DATA_LEAD_HOLDER_EID, -1));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_SCALE, 1.0f));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_BOUNDING_BOX_WIDTH, this.WIDTH));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_BOUNDING_BOX_HEIGHT, this.HEIGHT));

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_HAS_COLLISION);
            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_AFFECTED_BY_GRAVITY);

            if (!this.IsPlayer)
            {
                ListTag pos = tag.GetList("Pos");
                this.Vector3 = new Vector3(
                    pos.GetTag<FloatTag>(0).Data,
                    pos.GetTag<FloatTag>(1).Data,
                    pos.GetTag<FloatTag>(2).Data);
                ListTag rotation = tag.GetList("Rotation");
                this.Yaw = pos.GetTag<FloatTag>(0).Data;
                this.Pitch = pos.GetTag<FloatTag>(1).Data;

                this.World = world;
                this.World.AddEntity(this);
            }

            this.EntityInit();
        }

        public abstract void EntityInit();

        public Player[] Viewers
        {
            get
            {
                return this.viewers.ToArray();
            }
        }

        public void SendPacketViewers(DataPacket pk)
        {
            Player[] players = this.Viewers;
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].HasSpawned)
                {
                    players[i].SendPacket(pk);
                }
            }
        }

        public async void AsyncSendPacketViewers(DataPacket pk)
        {
            await Task.Run(() =>
            {
                this.SendPacketViewers(pk);
            });
        }

        public void SendPacketPlayers(DataPacket pk, params Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].HasSpawned)
                {
                    players[i].SendPacket(pk);
                }
            }
        }

        public async void AsyncSendPacketPlayers(DataPacket pk, params Player[] players)
        {
            await Task.Run(() =>
            {
                this.SendPacketPlayers(pk, players);
            });
        }

        public virtual void SetMotion(Vector3 motion)
        {
            SetEntityMotionPacket pk = new SetEntityMotionPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.Motion = motion;

            this.AsyncSendPacketViewers(pk);
        }

        public virtual void SpawnTo(Player player)
        {
            Vector2 chunkPos = this.GetChunkVector();
            if (!this.viewers.Contains(player) && player.loadedChunk.ContainsKey(new Tuple<int, int>(chunkPos.FloorX, chunkPos.FloorY)))
            {
                this.viewers.Add(player);
            }
        }

        public virtual void SpawnToAll()
        {
            if (this.Closed)
            {
                return;
            }

            Player[] players = this.World.Players.Values.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                this.SpawnTo(players[i]);
            }
        }

        public virtual void DespawnFrom(Player player)
        {
            if (this.viewers.Contains(player))
            {
                RemoveEntityPacket pk = new RemoveEntityPacket();
                pk.EntityUniqueId = this.EntityID;
                player.SendPacket(pk);

                this.viewers.Remove(player);

                Logger.Info("Despawn");
            }
        }

        public virtual void DespawnFromAll()
        {
            Player[] players = this.viewers.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                this.DespawnFrom(players[i]);
            }
        }

        public Vector3 Vector3
        {
            get
            {
                return new Vector3(this.X, this.Y, this.Z);
            }

            protected set
            {
                this.X = value.X;
                this.Y = value.Y;
                this.Z = value.Z;
            }
        }

        public Vector3 Direction
        {
            get
            {
                return new Vector3(this.Yaw, this.Pitch, this.HeadYaw);
            }
        }

        public Vector2 GetChunkVector()
        {
            return new Vector2(((int) this.X) >> 4, ((int) this.Z) >> 4);
        }

        internal virtual void OnUpdate(int tick)
        {

        }

        public virtual void SaveNBT()
        {
            ListTag pos = new ListTag("Pos", NBTTagType.FLOAT);
            pos.Add(new FloatTag(this.X));
            pos.Add(new FloatTag(this.Y));
            pos.Add(new FloatTag(this.Z));
            this.NamedTag.PutList(pos);

            ListTag rotation = new ListTag("Rotation", NBTTagType.FLOAT);
            rotation.Add(new FloatTag(this.Yaw));
            rotation.Add(new FloatTag(this.Pitch));
            this.NamedTag.PutList(rotation);
        }

        public virtual void Close()
        {
            this.World.RemoveEntity(this);
        }

        public Vector2 DirectionPlane
        {
            get
            {
                return new Vector2((float) -Math.Cos(this.Yaw * Math.PI / 180 - Math.PI / 2), (float) -Math.Sin(this.Yaw * Math.PI / 180 - Math.PI / 2)).Normalized;
            }
        }

        public BlockFace DirectionBlockFace
        {
            get
            {
                float rotation = this.Yaw;
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
                else if (225 < rotation && rotation < 315)
                {
                    return BlockFace.EAST;
                }
                return BlockFace.NORTH;
            }
        }

        public static explicit operator Vector2(Entity entity)
        {
            return new Vector2(entity.X, entity.Y);
        }

        public static explicit operator Vector3(Entity entity)
        {
            return new Vector3(entity.X, entity.Y, entity.Z);
        }

        public static explicit operator Vector3i(Entity entity)
        {
            return new Vector3i((int) entity.X, (int) entity.Y, (int) entity.Z);
        }

        public static explicit operator Position(Entity entity)
        {
            return new Position(entity.X, entity.Y, entity.Z, entity.World);
        }

        public static explicit operator Location(Entity entity)
        {
            return new Location(entity.X, entity.Y, entity.Z, entity.Yaw, entity.Pitch, entity.World);
        }
    }
}
