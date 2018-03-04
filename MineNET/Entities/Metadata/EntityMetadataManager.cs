using System.Collections.Generic;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Entities.Metadata
{
    public class EntityMetadataManager
    {
        private Dictionary<int, EntityData> entityData = new Dictionary<int, EntityData>();

        public EntityMetadataManager()
        {

        }

        public EntityData GetEntityData(int id)
        {
            return this.entityData[id];
        }

        public EntityMetadataManager PutEntityData(EntityData data)
        {
            this.entityData[data.ID] = data;
            return this;
        }

        public byte GetByte(int id)
        {
            return ((EntityDataByte) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutByte(int id, byte data)
        {
            this.entityData[id] = new EntityDataByte(id, data);
            return this;
        }

        public short GetShort(int id)
        {
            return ((EntityDataShort) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutShort(int id, short data)
        {
            this.entityData[id] = new EntityDataShort(id, data);
            return this;
        }

        public int GetInt(int id)
        {
            return ((EntityDataInt) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutInt(int id, int data)
        {
            this.entityData[id] = new EntityDataInt(id, data);
            return this;
        }

        public float GetFloat(int id)
        {
            return ((EntityDataFloat) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutFloat(int id, float data)
        {
            this.entityData[id] = new EntityDataFloat(id, data);
            return this;
        }

        public string GetString(int id)
        {
            return ((EntityDataString) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutString(int id, string data)
        {
            this.entityData[id] = new EntityDataString(id, data);
            return this;
        }

        public Item GetSlot(int id)
        {
            return ((EntityDataSlot) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutSlot(int id, Item item)
        {
            this.entityData[id] = new EntityDataSlot(id, item);
            return this;
        }

        public Vector3i GetPos(int id)
        {
            return ((EntityDataPos) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutPos(int id, Vector3i data)
        {
            this.entityData[id] = new EntityDataPos(id, data);
            return this;
        }

        public long GetLong(int id)
        {
            return ((EntityDataLong) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutLong(int id, long data)
        {
            this.entityData[id] = new EntityDataLong(id, data);
            return this;
        }

        public Vector3 GetVector(int id)
        {
            return ((EntityDataVector) this.entityData[id]).Data;
        }

        public EntityMetadataManager PutVector(int id, Vector3 data)
        {
            this.entityData[id] = new EntityDataVector(id, data);
            return this;
        }

        public bool Exists(int id)
        {
            return this.entityData.ContainsKey(id);
        }

        public void Remove(int id)
        {
            this.entityData.Remove(id);
        }

        public Dictionary<int, EntityData> GetEntityDatas()
        {
            return this.entityData;
        }
    }
}
