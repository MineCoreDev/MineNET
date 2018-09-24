using System;
using System.Collections.Generic;
using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
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

        public List<Player> Viewers { get; protected set; } = new List<Player>();

        public CompoundTag NamedTag { get; protected set; }
        public EntityMetadataManager DataProperties { get; private set; }
        public EntityAttributeDictionary Attributes { get; private set; }

        public Entity(Chunk chunk, CompoundTag tag)
        {
            this.EntityID = ++nextEntityId;

            this.Chunk = chunk;
            this.World = chunk.World;

            this.NamedTag = tag;
            this.EntityInit();
        }

        protected virtual void EntityInit()
        {
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

        public virtual void InitNBT()
        {
        }

        public virtual void SaveNBT()
        {
            throw new NotImplementedException();
        }

        public virtual void SpawnToAll()
        {
            if (this.Chunk == null || this.Closed)
            {
                return;
            }
        }

        public virtual void SpawnTo(Player player)
        {
        }

        public virtual void DespawnFromAll()
        {
        }

        public virtual void DespawnFrom(Player player)
        {
            RemoveEntityPacket pk = new RemoveEntityPacket();
            pk.EntityUniqueId = this.EntityID;

            player.SendPacket(pk);
        }

        public virtual void SendSpawnPacket(Player player)
        {
            AddEntityPacket pk = new AddEntityPacket();
            pk.EntityUniqueId = this.EntityID;
            pk.EntityRuntimeId = this.EntityID;
            pk.Type = this.NetworkId;
            pk.Position = (Vector3) this.Position;
            pk.Motion = new Vector3();
            pk.Direction = new Vector2(this.Yaw, this.Pitch);
            pk.Attributes = this.Attributes;
            pk.Metadata = this.DataProperties;

            player.SendPacket(pk);
        }

        public virtual void Kill()
        {
            this.Close();
        }

        public virtual void Close()
        {
            this.Closed = true;
        }

        public Position Position
        {
            get { return new Position(this.X, this.Y, this.Z, this.World); }
        }

        public Vector2 DirectionPlane
        {
            get
            {
                return new Vector2((float) -Math.Cos(this.Yaw * Math.PI / 180 - Math.PI / 2),
                    (float) -Math.Sin(this.Yaw * Math.PI / 180 - Math.PI / 2)).Normalized;
            }
        }
    }
}