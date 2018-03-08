using System;
using System.Collections;
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

        private EntityMetadataManager dataProperties = new EntityMetadataManager();

        protected List<Player> viewers = new List<Player>();

        public CompoundTag namedTag;

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

        public long EntityID { get; }

        public abstract float WIDTH { get; }
        public abstract float HEIGHT { get; }

        public virtual bool IsPlayer { get { return false; } }

        public abstract string Name { get; protected set; }

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

        public bool GetFlag(int id, int flagID)
        {
            EntityData data = this.GetDataProperty(id);
            if (data is EntityDataLong)
            {
                EntityDataLong longData = (EntityDataLong) data;
                long flag = longData.Data;
                BitArray flags = new BitArray(BitConverter.GetBytes(flag));
                return flags[flagID];
            }
            return false;
        }

        public void SetFlag(int id, int flagID, bool value = true, bool send = false)
        {
            EntityData data = this.GetDataProperty(id);
            if (data is EntityDataLong)
            {
                EntityDataLong longData = (EntityDataLong) data;
                long flag = longData.Data;
                BitArray flags = new BitArray(BitConverter.GetBytes(flag));
                flags[flagID] = value;

                byte[] result = new byte[8];
                flags.CopyTo(result, 0);

                this.SetDataProperty(new EntityDataLong(id, BitConverter.ToInt64(result, 0)), send);
            }
        }

        public EntityData GetDataProperty(int id)
        {
            return this.dataProperties.GetEntityData(id);
        }

        public void SetDataProperty(EntityData data, bool send = false)
        {
            this.dataProperties.PutEntityData(data);
            if (send)
            {
                this.SendDataProperties();
            }
        }

        public void RemoveDataProperty(int id, bool send = false)
        {
            this.dataProperties.Remove(id);
            if (send)
            {
                this.SendDataProperties();
            }
        }

        public Dictionary<int, EntityData> GetDataProperties()
        {
            return this.dataProperties.GetEntityDatas();
        }



        public void SendDataProperties()
        {
            SetEntityDataPacket pk = new SetEntityDataPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.EntityData = this.dataProperties;

            if (this.IsPlayer)
            {
                List<Player> players = new List<Player>(this.GetViewers());
                players.Add((Player) this);
                this.AsyncSendPacketPlayers(pk, players.ToArray());
            }
            else
            {
                this.AsyncSendPacketPlayers(pk, this.GetViewers());
            }
        }

        internal virtual void OnUpdate()
        {

        }

        public virtual void Close()
        {

        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public World World { get; set; }
    }
}
