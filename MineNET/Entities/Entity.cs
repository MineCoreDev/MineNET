using System;
using MineNET.Entities.Metadata;
using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract partial class Entity
    {
        private static long nextEntityId = 0;


        #region Property & Field

        public abstract string Name { get; protected set; }

        public virtual float Width { get; }
        public virtual float Height { get; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public World World { get; protected set; }

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

        public CompoundTag NamedTag { get; protected set; }
        public EntityMetadataManager DataProperties { get; private set; }

        #endregion

        #region Ctor

        public Entity(World world, CompoundTag tag)
        {
            this.EntityID = ++nextEntityId;


            if (!this.IsPlayer)
            {
                this.World = world;
            }

            this.NamedTag = tag;
            this.EntityInit();
        }

        #endregion

        #region Init Method

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
        }

        #endregion

        #region Update Method

        internal virtual bool UpdateTick(long tick)
        {
            return true;
        }

        #endregion

        public Vector2 GetChunkVector()
        {
            return new Vector2(((int) this.X) >> 4, ((int) this.Z) >> 4);
        }

        #region Init NBT

        public virtual void InitNBT()
        {
        }

        #endregion

        #region Save Method

        public virtual void SaveNBT()
        {
            throw new NotImplementedException();
        }

        #endregion

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