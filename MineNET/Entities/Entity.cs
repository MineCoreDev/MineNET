using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
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

        public World World { get; set; }

        public float MotionX { get; set; }
        public float MotionY { get; set; }
        public float MotionZ { get; set; }

        public CompoundTag NamedTag { get; protected set; }

        public long EntityID { get; }

        public abstract float WIDTH { get; }
        public abstract float HEIGHT { get; }

        public virtual bool IsPlayer { get { return false; } }

        public abstract string Name { get; protected set; }

        public virtual bool Closed { get; protected set; }

        public Entity()
        {
            this.EntityID = Entity.nextEntityId++;

            this.SetDataProperty(new EntityDataLong(Entity.DATA_FLAGS, 0));
            this.SetDataProperty(new EntityDataShort(Entity.DATA_AIR, 400));
            this.SetDataProperty(new EntityDataShort(Entity.DATA_MAX_AIR, 400));
            this.SetDataProperty(new EntityDataString(Entity.DATA_NAMETAG, ""));
            this.SetDataProperty(new EntityDataLong(Entity.DATA_LEAD_HOLDER_EID, -1));
            this.SetDataProperty(new EntityDataLong(Entity.DATA_TARGET_EID, -1));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_SCALE, 1.0f));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_BOUNDING_BOX_WIDTH, this.WIDTH));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_BOUNDING_BOX_HEIGHT, this.HEIGHT));

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_HAS_COLLISION);
            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_AFFECTED_BY_GRAVITY);
            //this.SetFlag(Entity.DATA_FLAGS, Entity.);
        }

        public Player[] GetViewers()
        {
            return this.viewers.ToArray();
        }

        public void SendPacketViewers(DataPacket pk)
        {
            Player[] players = this.GetViewers();
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

        }

        public virtual void SpawnToAll()
        {

        }

        public virtual void DespawnFrom(Player player)
        {

        }

        public virtual void DespawnFromAll()
        {

        }

        public Vector2 Vector2
        {
            get
            {
                return new Vector2(this.X, this.Z);
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

        public Vector3i Vector3i
        {
            get
            {
                return this.Vector3.Vector3i;
            }
        }

        public Position Position
        {
            get
            {
                return new Position(this.X, this.Y, this.Z, this.World);
            }
        }

        public Location Location
        {
            get
            {
                return new Location(this.X, this.Y, this.Z, this.Yaw, this.Pitch, this.World);
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

        }

        public virtual void Close()
        {

        }

        public Vector2 DirectionPlane
        {
            get
            {
                return new Vector2((float) -Math.Cos(this.Yaw * Math.PI / 180 - Math.PI / 2), (float) -Math.Sin(this.Yaw * Math.PI / 180 - Math.PI / 2)).Normalized;
            }
        }
    }
}
