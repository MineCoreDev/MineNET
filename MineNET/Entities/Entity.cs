using System;
using System.Collections.Generic;
using System.Linq;
using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract partial class Entity
    {
        private static long nextEntityId = 0;

        public abstract string Name { get; protected set; }
        public abstract int NetworkId { get; }

        public virtual float Width { get; }
        public virtual float Height { get; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public World World { get; protected set; }

        public Chunk Chunk { get; private set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public float HeadYaw { get; set; }

        public float MotionX { get; set; }
        public float MotionY { get; set; }
        public float MotionZ { get; set; }

        public long EntityID { get; }

        public virtual bool IsPlayer
        {
            get { return false; }
        }

        public bool Closed { get; protected set; }

        protected Dictionary<long, Player> _hasSpawned = new Dictionary<long, Player>();

        public EntityMetadataManager DataProperties { get; private set; }
        public EntityAttributeDictionary Attributes { get; private set; }

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

        protected virtual void EntityInit(CompoundTag nbt)
        {
            ListTag list = nbt.GetList("Pos");
            this.X = ((FloatTag) list[0]).Data;
            this.Y = ((FloatTag) list[1]).Data;
            this.Z = ((FloatTag) list[2]).Data;

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
            this.SetFlag(DATA_FLAGS, DATA_FLAG_AFFECTED_BY_GRAVITY);

            this.Attributes = new EntityAttributeDictionary(this.EntityID);
        }

        internal virtual bool UpdateTick(long tick)
        {
            return true;
        }

        public Vector2 GetChunkVector()
        {
            return new Vector2(((int) this.X) >> 4, ((int) this.Z) >> 4);
        }

        public virtual CompoundTag SaveNBT()
        {
            CompoundTag nbt = new CompoundTag();

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

        public virtual void SpawnTo(Player player)
        {
            this.SendSpawnPacket(player);

            if (!this._hasSpawned.ContainsKey(player.EntityID)) //TODO : UsedChunk?
            {
                this._hasSpawned[player.EntityID] = player;
            }
        }

        public virtual void DespawnFromAll()
        {
            Player[] players = this.Viewers;
            for (int i = 0; i < players.Length; ++i)
            {
                this.DespawnFrom(players[i]);
            }
        }

        public virtual void DespawnFrom(Player player)
        {
            RemoveEntityPacket pk = new RemoveEntityPacket();
            pk.EntityUniqueId = this.EntityID;

            player.SendPacket(pk);

            if (this._hasSpawned.ContainsKey(player.EntityID))
            {
                this._hasSpawned.Remove(player.EntityID);
            }
        }

        public virtual void SendSpawnPacket(Player player)
        {
            AddEntityPacket pk = new AddEntityPacket
            {
                EntityUniqueId = this.EntityID,
                EntityRuntimeId = this.EntityID,
                Type = this.NetworkId,
                Position = this.GetVector3(),
                Motion = new Vector3(),
                Direction = new Vector3(this.Yaw, this.Pitch, this.HeadYaw),
                Attributes = this.Attributes,
                Metadata = this.DataProperties
            };

            player.SendPacket(pk);
        }

        public Player[] Viewers
        {
            get
            {
                return this._hasSpawned.Values.ToArray();
            }
        }

        public virtual void Kill()
        {
            this.Close();
        }

        public virtual void Close()
        {
            this.Closed = true;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(this.Yaw, this.Pitch);
        }

        public Vector3 GetVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }

        public Position GetPosition()
        {
            return new Position(this.X, this.Y, this.Z, this.World);
        }

        public Location GetLocation()
        {
            return new Location(this.X, this.Y, this.Z, this.Yaw, this.Pitch, this.World);
        }

        public Vector3 GetMotion()
        {
            return new Vector3(this.MotionX, this.MotionY, this.MotionZ);
        }

        public Vector2 GetDirectionPlane()
        {
            return new Vector2((float) -Math.Cos(this.Yaw * Math.PI / 180 - Math.PI / 2),
                (float) -Math.Sin(this.Yaw * Math.PI / 180 - Math.PI / 2)).Normalized;
        }
    }
}