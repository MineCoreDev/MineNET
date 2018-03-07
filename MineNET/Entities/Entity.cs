using System;
using System.Collections;
using System.Collections.Generic;
using MineNET.Entities.Data;
using MineNET.Entities.Metadata;
using MineNET.NBT.Tags;
using MineNET.Network.Packets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class Entity : ILocation
    {
        private static long nextEntityId = 0;

        private EntityMetadataManager dataProperties = new EntityMetadataManager();

        protected List<Player> viewers = new List<Player>();

        public CompoundTag namedTag;

        public Entity()
        {
            this.EntityID = Entity.nextEntityId++;

            this.SetDataProperty(new EntityDataLong(EntityFlags.DATA_FLAGS, 0));
            this.SetDataProperty(new EntityDataShort(EntityFlags.DATA_AIR, 400));
            this.SetDataProperty(new EntityDataShort(EntityFlags.DATA_MAX_AIR, 400));
            this.SetDataProperty(new EntityDataString(EntityFlags.DATA_NAMETAG, ""));
            this.SetDataProperty(new EntityDataLong(EntityFlags.DATA_LEAD_HOLDER_EID, -1));
            this.SetDataProperty(new EntityDataLong(EntityFlags.DATA_TRADING_PLAYER_EID, -1));
            this.SetDataProperty(new EntityDataFloat(EntityFlags.DATA_SCALE, 1.0f));
            this.SetDataProperty(new EntityDataFloat(EntityFlags.DATA_BOUNDING_BOX_WIDTH, this.WIDTH));
            this.SetDataProperty(new EntityDataFloat(EntityFlags.DATA_BOUNDING_BOX_HEIGHT, this.HEIGHT));

            this.SetFlag(EntityFlags.DATA_FLAGS, EntityFlags.DATA_FLAG_HAS_COLLISION);
            this.SetFlag(EntityFlags.DATA_FLAGS, EntityFlags.DATA_FLAG_AFFECTED_BY_GRAVITY);
            //this.SetFlag(EntityFlags.DATA_FLAGS, EntityFlags.);
        }

        public long EntityID { get; }

        public abstract float WIDTH { get; }
        public abstract float HEIGHT { get; }

        public bool IsPlayer { get; protected set; }

        public abstract string Name { get; protected set; }

        string displayName;
        public string DisplayName
        {
            get
            {
                return this.displayName;
            }

            set
            {
                this.displayName = value;
                //TODO: SendEntityData
            }
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

        public virtual void SetMotion(Vector3 motion)
        {
            SetEntityMotionPacket pk = new SetEntityMotionPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.Motion = motion;

            this.SendPacketViewers(pk);
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

        public void SetFlag(int id, int flagID, bool value = true)
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

                this.SetDataProperty(new EntityDataLong(id, BitConverter.ToInt64(result, 0)));
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
                SendDataAll();
            }
        }

        public void RemoveDataProperty(int id, bool send = false)
        {
            this.dataProperties.Remove(id);
            if (send)
            {
                SendDataAll();
            }
        }

        public Dictionary<int, EntityData> GetDataProperties()
        {
            return this.dataProperties.GetEntityDatas();
        }

        public void SendDataAll()
        {
            this.SendData(this.GetViewers());
        }

        public void SendData(params Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                SetEntityDataPacket pk = new SetEntityDataPacket();
                pk.EntityRuntimeId = this.EntityID;
                pk.EntityData = this.dataProperties;
                players[i].SendPacket(pk);
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
