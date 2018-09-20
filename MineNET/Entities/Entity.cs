using System;
using System.Collections;
using System.Collections.Generic;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities
{
    public abstract class Entity
    {
        #region Static Property
        private static long nextEntityId = 0;
        #endregion

        #region EntityProperties
        public const int DATA_FLAGS = 0;
        public const int DATA_HEALTH = 1; //int (minecart/boat)
        public const int DATA_VARIANT = 2; //int
        public const int DATA_COLOR = 3, DATA_COLOUR = 3; //byte
        public const int DATA_NAMETAG = 4; //string
        public const int DATA_OWNER_EID = 5; //long
        public const int DATA_TARGET_EID = 6; //long
        public const int DATA_AIR = 7; //short
        public const int DATA_POTION_COLOR = 8; //int (ARGB!)
        public const int DATA_POTION_AMBIENT = 9; //byte
                                                  /* 10 (byte) */
        public const int DATA_HURT_TIME = 11; //int (minecart/boat)
        public const int DATA_HURT_DIRECTION = 12; //int (minecart/boat)
        public const int DATA_PADDLE_TIME_LEFT = 13; //float
        public const int DATA_PADDLE_TIME_RIGHT = 14; //float
        public const int DATA_EXPERIENCE_VALUE = 15; //int (xp orb)
        public const int DATA_DISPLAY_ITEM = 16; //int (id | (data << 16))
        public const int DATA_DISPLAY_OFFSET = 17; //int
        public const int DATA_HAS_DISPLAY = 18; //byte (must be 1 for minecart to show block inside)

        //TODO: add more properties

        public const int DATA_ENDERMAN_HELD_ITEM_ID = 23; //short
        public const int DATA_ENTITY_AGE = 24; //short

        /* 26 (byte) player-specific flags
         * 27 (int) player "index"?
         * 28 (block coords) bed position */
        public const int DATA_FIREBALL_POWER_X = 29; //float
        public const int DATA_FIREBALL_POWER_Y = 30;
        public const int DATA_FIREBALL_POWER_Z = 31;
        /* 32 (unknown)
         * 33 (float) fishing bobber
         * 34 (float) fishing bobber
         * 35 (float) fishing bobber */
        public const int DATA_POTION_AUX_VALUE = 36; //short
        public const int DATA_LEAD_HOLDER_EID = 37; //long
        public const int DATA_SCALE = 38; //float
        public const int DATA_INTERACTIVE_TAG = 39; //string (button text)
        public const int DATA_NPC_SKIN_ID = 40; //string
        public const int DATA_URL_TAG = 41; //string
        public const int DATA_MAX_AIR = 42; //short
        public const int DATA_MARK_VARIANT = 43; //int
        public const int DATA_CONTAINER_TYPE = 44; //byte (ContainerComponent) 
        public const int DATA_CONTAINER_BASE_SIZE = 45; //int (ContainerComponent) 
        public const int DATA_CONTAINER_EXTRA_SLOTS_PER_STRENGTH = 46; //int (used for llamas, inventory size is baseSize + thisProp * strength) 
        public const int DATA_BLOCK_TARGET = 47; //block coords (ender crystal)
        public const int DATA_WITHER_INVULNERABLE_TICKS = 48; //int
        public const int DATA_WITHER_TARGET_1 = 49; //long
        public const int DATA_WITHER_TARGET_2 = 50; //long
        public const int DATA_WITHER_TARGET_3 = 51; //long
                                                    /* 52 (short) */
        public const int DATA_BOUNDING_BOX_WIDTH = 53; //float
        public const int DATA_BOUNDING_BOX_HEIGHT = 54; //float
        public const int DATA_FUSE_LENGTH = 55; //int
        public const int DATA_RIDER_SEAT_POSITION = 56; //vector3f
        public const int DATA_RIDER_ROTATION_LOCKED = 57; //byte
        public const int DATA_RIDER_MAX_ROTATION = 58; //float
        public const int DATA_RIDER_MIN_ROTATION = 59; //float
        public const int DATA_AREA_EFFECT_CLOUD_RADIUS = 60; //float
        public const int DATA_AREA_EFFECT_CLOUD_WAITING = 61; //int
        public const int DATA_AREA_EFFECT_CLOUD_PARTICLE_ID = 62; //int
                                                                  /* 63 (int) shulker-related */
        public const int DATA_SHULKER_ATTACH_FACE = 64; //byte
                                                        /* 65 (short) shulker-related */
        public const int DATA_SHULKER_ATTACH_POS = 66; //block coords
        public const int DATA_TRADING_PLAYER_EID = 67; //long

        /* 69 (byte) command-block */
        public const int DATA_COMMAND_BLOCK_COMMAND = 70; //string
        public const int DATA_COMMAND_BLOCK_LAST_OUTPUT = 71; //string
        public const int DATA_COMMAND_BLOCK_TRACK_OUTPUT = 72; //byte
        public const int DATA_CONTROLLING_RIDER_SEAT_NUMBER = 73; //byte
        public const int DATA_STRENGTH = 74; //int
        public const int DATA_MAX_STRENGTH = 75; //int
                                                 /* 76 (int) */
        public const int DATA_LIMITED_LIFE = 77;
        public const int DATA_ARMOR_STAND_POSE_INDEX = 78; //int
        public const int DATA_ENDER_CRYSTAL_TIME_OFFSET = 79; //int
                                                              /* 80 (byte) something to do with nametag visibility? */
        public const int DATA_COLOR_2 = 81; //byte
                                            /* 82 (unknown) */
        public const int DATA_SCORE_TAG = 83; //string
        public const int DATA_BALLOON_ATTACHED_ENTITY = 84; //int64, entity unique ID of owner
        public const int DATA_PUFFERFISH_SIZE = 85; //byte


        public const int DATA_FLAG_ONFIRE = 0;
        public const int DATA_FLAG_SNEAKING = 1;
        public const int DATA_FLAG_RIDING = 2;
        public const int DATA_FLAG_SPRINTING = 3;
        public const int DATA_FLAG_ACTION = 4;
        public const int DATA_FLAG_INVISIBLE = 5;
        public const int DATA_FLAG_TEMPTED = 6;
        public const int DATA_FLAG_INLOVE = 7;
        public const int DATA_FLAG_SADDLED = 8;
        public const int DATA_FLAG_POWERED = 9;
        public const int DATA_FLAG_IGNITED = 10;
        public const int DATA_FLAG_BABY = 11;
        public const int DATA_FLAG_CONVERTING = 12;
        public const int DATA_FLAG_CRITICAL = 13;
        public const int DATA_FLAG_CAN_SHOW_NAMETAG = 14;
        public const int DATA_FLAG_ALWAYS_SHOW_NAMETAG = 15;
        public const int DATA_FLAG_IMMOBILE = 16, DATA_FLAG_NO_AI = 16;
        public const int DATA_FLAG_SILENT = 17;
        public const int DATA_FLAG_WALLCLIMBING = 18;
        public const int DATA_FLAG_CAN_CLIMB = 19;
        public const int DATA_FLAG_SWIMMER = 20;
        public const int DATA_FLAG_CAN_FLY = 21;
        public const int DATA_FLAG_WALKER = 22;
        public const int DATA_FLAG_RESTING = 23;
        public const int DATA_FLAG_SITTING = 24;
        public const int DATA_FLAG_ANGRY = 25;
        public const int DATA_FLAG_INTERESTED = 26;
        public const int DATA_FLAG_CHARGED = 27;
        public const int DATA_FLAG_TAMED = 28;
        public const int DATA_FLAG_LEASHED = 29;
        public const int DATA_FLAG_SHEARED = 30;
        public const int DATA_FLAG_GLIDING = 31;
        public const int DATA_FLAG_ELDER = 32;
        public const int DATA_FLAG_MOVING = 33;
        public const int DATA_FLAG_BREATHING = 34;
        public const int DATA_FLAG_CHESTED = 35;
        public const int DATA_FLAG_STACKABLE = 36;
        public const int DATA_FLAG_SHOWBASE = 37;
        public const int DATA_FLAG_REARING = 38;
        public const int DATA_FLAG_VIBRATING = 39;
        public const int DATA_FLAG_IDLING = 40;
        public const int DATA_FLAG_EVOKER_SPELL = 41;
        public const int DATA_FLAG_CHARGE_ATTACK = 42;
        public const int DATA_FLAG_WASD_CONTROLLED = 43;
        public const int DATA_FLAG_CAN_POWER_JUMP = 44;
        public const int DATA_FLAG_LINGER = 45;
        public const int DATA_FLAG_HAS_COLLISION = 46;
        public const int DATA_FLAG_AFFECTED_BY_GRAVITY = 47;
        public const int DATA_FLAG_FIRE_IMMUNE = 48;
        public const int DATA_FLAG_DANCING = 49;
        public const int DATA_FLAG_ENCHANTED = 50;
        //51 is something to do with tridents
        public const int DATA_FLAG_CONTAINER_PRIVATE = 52; //inventory is private, doesn't drop contents when killed if true
                                                           //53 TransformationComponent
        public const int DATA_FLAG_SPIN_ATTACK = 54;
        public const int DATA_FLAG_SWIMMING = 55;
        public const int DATA_FLAG_BRIBED = 56; //dolphins have this set when they go to find treasure for the player

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
            return this.DataProperties.GetEntityData(id);
        }

        public void SetDataProperty(EntityData data, bool send = false)
        {
            this.DataProperties.PutEntityData(data);
            if (send)
            {
                this.SendDataProperties();
            }
        }

        public void RemoveDataProperty(int id, bool send = false)
        {
            this.DataProperties.Remove(id);
            if (send)
            {
                this.SendDataProperties();
            }
        }

        public Dictionary<int, EntityData> GetDataProperties()
        {
            return this.DataProperties.GetEntityDatas();
        }

        public void SendDataProperties()
        {
            foreach (Player player in Server.Instance.GetPlayers())
            {
                this.DataProperties.Update(player);
            }
        }

        public virtual string DisplayName
        {
            get
            {
                return ((EntityDataString) this.GetDataProperty(Entity.DATA_NAMETAG)).Data;
            }

            set
            {
                this.SetDataProperty(new EntityDataString(Entity.DATA_NAMETAG, value));
            }
        }

        public virtual string InteractiveTag //カーソルを合わせたときに出るボタン
        {
            get
            {
                return ((EntityDataString) this.GetDataProperty(Entity.DATA_INTERACTIVE_TAG)).Data;
            }

            set
            {
                this.SetDataProperty(new EntityDataString(Entity.DATA_INTERACTIVE_TAG, value));
            }
        }

        public virtual bool ShowNameTag
        {
            get
            {
                return this.GetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_CAN_SHOW_NAMETAG);
            }

            set
            {
                this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_CAN_SHOW_NAMETAG, value);
            }
        }

        public virtual bool AlwaysShowNameTag
        {
            get
            {
                return this.GetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_ALWAYS_SHOW_NAMETAG);
            }

            set
            {
                this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_ALWAYS_SHOW_NAMETAG, value);
            }
        }

        public virtual bool Sprinting
        {
            get
            {
                return this.GetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_SPRINTING);
            }

            set
            {
                this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_SPRINTING, value);
            }
        }

        public virtual bool Sneaking
        {
            get
            {
                return this.GetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_SNEAKING);
            }

            set
            {
                this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_SNEAKING, value);
            }
        }

        public virtual bool Gliding
        {
            get
            {
                return this.GetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_GLIDING);
            }

            set
            {
                this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_GLIDING, value);
            }
        }

        public virtual bool Action
        {
            get
            {
                return this.GetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_ACTION);
            }

            set
            {
                this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_ACTION, value);
            }
        }
        #endregion

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

        public virtual bool IsPlayer { get { return false; } }
        public bool Closed { get; protected set; }

        public CompoundTag NamedTag { get; }
        public EntityMetadataManager DataProperties { get; private set; }
        #endregion

        #region Ctor
        public Entity(World world, CompoundTag tag)
        {
            this.EntityID = ++Entity.nextEntityId;


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
            this.SetDataProperty(new EntityDataLong(Entity.DATA_FLAGS, 0));
            this.SetDataProperty(new EntityDataShort(Entity.DATA_AIR, 400));
            this.SetDataProperty(new EntityDataShort(Entity.DATA_MAX_AIR, 400));
            this.SetDataProperty(new EntityDataString(Entity.DATA_NAMETAG, ""));
            this.SetDataProperty(new EntityDataLong(Entity.DATA_LEAD_HOLDER_EID, -1));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_SCALE, 1.0f));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_BOUNDING_BOX_WIDTH, this.Width));
            this.SetDataProperty(new EntityDataFloat(Entity.DATA_BOUNDING_BOX_HEIGHT, this.Height));

            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_HAS_COLLISION);
            this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_AFFECTED_BY_GRAVITY);
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

        #region Save Method
        public void SaveNBT()
        {
            throw new NotImplementedException();
        }
        #endregion

        public Position Position
        {
            get
            {
                return new Position(this.X, this.Y, this.Z, this.World);
            }
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
