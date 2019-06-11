using System;
using System.Collections;
using System.Collections.Generic;
using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;

namespace MineNET.Entities
{
    public abstract partial class Entity
    {
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
        public const int DATA_INTERACTIVE_TAG = 39; //string (button text)    public const int DATA_SKIN_ID = 40; // int ???
        public const int DATA_NPC_SKIN_ID = 41; //string
        public const int DATA_URL_TAG = 42; //string
        public const int DATA_MAX_AIR = 43; //short
        public const int DATA_MARK_VARIANT = 44; //int
        public const int DATA_CONTAINER_TYPE = 45; //byte
        public const int DATA_CONTAINER_BASE_SIZE = 46; //int
        public const int DATA_CONTAINER_EXTRA_SLOTS_PER_STRENGTH = 47; //int
        public const int DATA_BLOCK_TARGET = 48; //block coords (ender crystal)
        public const int DATA_WITHER_INVULNERABLE_TICKS = 49; //int
        public const int DATA_WITHER_TARGET_1 = 50; //long
        public const int DATA_WITHER_TARGET_2 = 51; //long
        public const int DATA_WITHER_TARGET_3 = 52; //long
                                                           /* 53 (short) */
        public const int DATA_BOUNDING_BOX_WIDTH = 54; //float
        public const int DATA_BOUNDING_BOX_HEIGHT = 55; //float
        public const int DATA_FUSE_LENGTH = 56; //int
        public const int DATA_RIDER_SEAT_POSITION = 57; //vector3f
        public const int DATA_RIDER_ROTATION_LOCKED = 58; //byte
        public const int DATA_RIDER_MAX_ROTATION = 59; //float
        public const int DATA_RIDER_MIN_ROTATION = 60; //float
        public const int DATA_AREA_EFFECT_CLOUD_RADIUS = 61; //float
        public const int DATA_AREA_EFFECT_CLOUD_WAITING = 62; //int
        public const int DATA_AREA_EFFECT_CLOUD_PARTICLE_ID = 63; //int
                                                                         /* 64 (int) shulker-related */
        public const int DATA_SHULKER_ATTACH_FACE = 65; //byte
                                                               /* 66 (short) shulker-related */
        public const int DATA_SHULKER_ATTACH_POS = 67; //block coords
        public const int DATA_TRADING_PLAYER_EID = 68; //long

        /* 70 (byte) command-block */
        public const int DATA_COMMAND_BLOCK_COMMAND = 71; //string
        public const int DATA_COMMAND_BLOCK_LAST_OUTPUT = 72; //string
        public const int DATA_COMMAND_BLOCK_TRACK_OUTPUT = 73; //byte
        public const int DATA_CONTROLLING_RIDER_SEAT_NUMBER = 74; //byte
        public const int DATA_STRENGTH = 75; //int
        public const int DATA_MAX_STRENGTH = 76; //int
                                                        // 77 (int)
        public const int DATA_LIMITED_LIFE = 78;
        public const int DATA_ARMOR_STAND_POSE_INDEX = 79; // int
        public const int DATA_ENDER_CRYSTAL_TIME_OFFSET = 80; // int
        public const int DATA_ALWAYS_SHOW_NAMETAG = 81; // byte
        public const int DATA_COLOR_2 = 82; // byte
                                                   // 83 unknown
        public const int DATA_SCORE_TAG = 84; //String
        public const int DATA_BALLOON_ATTACHED_ENTITY = 85; // long
        public const int DATA_PUFFERFISH_SIZE = 86;

        public const int DATA_FLAGS_EXTENDED = 92;


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
        public const int DATA_FLAG_ORPHANED = 29;
        public const int DATA_FLAG_LEASHED = 30;
        public const int DATA_FLAG_SHEARED = 31;
        public const int DATA_FLAG_GLIDING = 32;
        public const int DATA_FLAG_ELDER = 33;
        public const int DATA_FLAG_MOVING = 34;
        public const int DATA_FLAG_BREATHING = 35;
        public const int DATA_FLAG_CHESTED = 36;
        public const int DATA_FLAG_STACKABLE = 37;
        public const int DATA_FLAG_SHOWBASE = 38;
        public const int DATA_FLAG_REARING = 39;
        public const int DATA_FLAG_VIBRATING = 40;
        public const int DATA_FLAG_IDLING = 41;
        public const int DATA_FLAG_EVOKER_SPELL = 42;
        public const int DATA_FLAG_CHARGE_ATTACK = 43;
        public const int DATA_FLAG_WASD_CONTROLLED = 44;
        public const int DATA_FLAG_CAN_POWER_JUMP = 45;
        public const int DATA_FLAG_LINGER = 46;
        public const int DATA_FLAG_HAS_COLLISION = 47;
        public const int DATA_FLAG_GRAVITY = 48;
        public const int DATA_FLAG_FIRE_IMMUNE = 49;
        public const int DATA_FLAG_DANCING = 50;
        public const int DATA_FLAG_ENCHANTED = 51;
        public const int DATA_FLAG_SHOW_TRIDENT_ROPE = 52; // tridents show an animated rope when enchanted with loyalty after they are thrown and return to their owner. To be combined with DATA_OWNER_EID
        public const int DATA_FLAG_CONTAINER_PRIVATE = 53; //inventory is private, doesn't drop contents when killed if true
                                                           //public const int TransformationComponent 54; ???
        public const int DATA_FLAG_SPIN_ATTACK = 55;
        public const int DATA_FLAG_SWIMMING = 56;
        public const int DATA_FLAG_BRIBED = 57; //dolphins have this set when they go to find treasure for the player
        public const int DATA_FLAG_PREGNANT = 58;
        public const int DATA_FLAG_LAYING_EGG = 59;

        #endregion

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
            foreach (Player player in this.Viewers)
            {
                this.DataProperties.Update(player);
            }

            if (this.IsPlayer)
            {
                this.DataProperties.Update((Player) this);
            }
        }

        #region Property

        public virtual string DisplayName
        {
            get
            {
                return ((EntityDataString) this.GetDataProperty(DATA_NAMETAG)).Data;
            }

            set
            {
                this.SetDataProperty(new EntityDataString(DATA_NAMETAG, value));
            }
        }

        public virtual string InteractiveTag //カーソルを合わせたときに出るボタン
        {
            get
            {
                return ((EntityDataString) this.GetDataProperty(DATA_INTERACTIVE_TAG)).Data;
            }


            set
            {
                this.SetDataProperty(new EntityDataString(DATA_INTERACTIVE_TAG, value));
            }
        }

        public virtual bool ShowNameTag
        {
            get
            {
                return this.GetFlag(DATA_FLAGS, DATA_FLAG_CAN_SHOW_NAMETAG);
            }

            set
            {
                this.SetFlag(DATA_FLAGS, DATA_FLAG_CAN_SHOW_NAMETAG, value);
            }
        }

        public virtual bool AlwaysShowNameTag
        {
            get
            {
                return this.GetFlag(DATA_FLAGS, DATA_FLAG_ALWAYS_SHOW_NAMETAG);
            }

            set
            {
                this.SetFlag(DATA_FLAGS, DATA_FLAG_ALWAYS_SHOW_NAMETAG, value);
            }
        }

        public virtual bool Sprinting
        {
            get
            {
                return this.GetFlag(DATA_FLAGS, DATA_FLAG_SPRINTING);
            }

            set
            {
                this.SetFlag(DATA_FLAGS, DATA_FLAG_SPRINTING, value);
            }
        }

        public virtual bool Sneaking
        {
            get
            {
                return this.GetFlag(DATA_FLAGS, DATA_FLAG_SNEAKING);
            }

            set
            {
                this.SetFlag(DATA_FLAGS, DATA_FLAG_SNEAKING, value);
            }
        }

        public virtual bool Gliding
        {
            get
            {
                return this.GetFlag(DATA_FLAGS, DATA_FLAG_GLIDING);
            }

            set
            {
                this.SetFlag(DATA_FLAGS, DATA_FLAG_GLIDING, value);
            }
        }

        public virtual bool Action
        {
            get
            {
                return this.GetFlag(DATA_FLAGS, DATA_FLAG_ACTION);
            }

            set
            {
                this.SetFlag(DATA_FLAGS, DATA_FLAG_ACTION, value);
            }
        }

        #endregion
    }
}