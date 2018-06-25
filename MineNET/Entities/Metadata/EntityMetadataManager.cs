using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using System.Collections.Generic;

namespace MineNET.Entities.Metadata
{
    public class EntityMetadataManager
    {
        private Dictionary<int, EntityData> EntityData { get; } = new Dictionary<int, EntityData>();

        public long EntityID { get; }

        public EntityMetadataManager(long entityID)
        {
            this.EntityID = entityID;
        }

        public EntityData GetEntityData(int id)
        {
            return this.EntityData[id];
        }

        public EntityMetadataManager PutEntityData(EntityData data)
        {
            this.EntityData[data.ID] = data;
            return this;
        }

        public byte GetByte(int id)
        {
            return ((EntityDataByte) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutByte(int id, byte data)
        {
            this.EntityData[id] = new EntityDataByte(id, data);
            return this;
        }

        public short GetShort(int id)
        {
            return ((EntityDataShort) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutShort(int id, short data)
        {
            this.EntityData[id] = new EntityDataShort(id, data);
            return this;
        }

        public int GetInt(int id)
        {
            return ((EntityDataInt) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutInt(int id, int data)
        {
            this.EntityData[id] = new EntityDataInt(id, data);
            return this;
        }

        public float GetFloat(int id)
        {
            return ((EntityDataFloat) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutFloat(int id, float data)
        {
            this.EntityData[id] = new EntityDataFloat(id, data);
            return this;
        }

        public string GetString(int id)
        {
            return ((EntityDataString) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutString(int id, string data)
        {
            this.EntityData[id] = new EntityDataString(id, data);
            return this;
        }

        public ItemStack GetSlot(int id)
        {
            return ((EntityDataSlot) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutSlot(int id, ItemStack item)
        {
            this.EntityData[id] = new EntityDataSlot(id, item);
            return this;
        }

        public BlockCoordinate3D GetPos(int id)
        {
            return ((EntityDataPos) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutPos(int id, BlockCoordinate3D data)
        {
            this.EntityData[id] = new EntityDataPos(id, data);
            return this;
        }

        public long GetLong(int id)
        {
            return ((EntityDataLong) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutLong(int id, long data)
        {
            this.EntityData[id] = new EntityDataLong(id, data);
            return this;
        }

        public Vector3 GetVector(int id)
        {
            return ((EntityDataVector) this.EntityData[id]).Data;
        }

        public EntityMetadataManager PutVector(int id, Vector3 data)
        {
            this.EntityData[id] = new EntityDataVector(id, data);
            return this;
        }

        public bool GetBool(int id)
        {
            return this.GetByte(id) == 1;
        }

        public EntityMetadataManager PutBool(int id, bool data)
        {
            this.PutByte(id, data ? (byte) 1 : (byte) 0);
            return this;
        }

        public bool Exists(int id)
        {
            return this.EntityData.ContainsKey(id);
        }

        public void Remove(int id)
        {
            this.EntityData.Remove(id);
        }

        public void Update(Player player)
        {
            SetEntityDataPacket pk = new SetEntityDataPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.EntityData = this;
            player.SendPacket(pk);
        }

        public Dictionary<int, EntityData> GetEntityDatas()
        {
            return this.EntityData;
        }
    }
}
