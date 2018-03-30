using System;
using System.Collections;
using System.Collections.Generic;
using MineNET.Entities.Metadata;
using MineNET.Entities.Players;
using MineNET.Network.Packets;

namespace MineNET.Entities
{
    public partial class Entity
    {
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
        public const int DATA_MINECART_DISPLAY_BLOCK = 16; //int (id | (data << 16))
        public const int DATA_MINECART_DISPLAY_OFFSET = 17; //int
        public const int DATA_MINECART_HAS_DISPLAY = 18; //byte (must be 1 for minecart to show block inside)

        //TODO: add more properties

        public const int DATA_ENDERMAN_HELD_ITEM_ID = 23; //short
        public const int DATA_ENDERMAN_HELD_ITEM_DAMAGE = 24; //short
        public const int DATA_ENTITY_AGE = 25; //short

        /* 27 (byte) player-specific flags
         * 28 (int) player "index"?
         * 29 (block coords) bed position */
        public const int DATA_FIREBALL_POWER_X = 30; //float
        public const int DATA_FIREBALL_POWER_Y = 31;
        public const int DATA_FIREBALL_POWER_Z = 32;
        /* 33 (unknown)
         * 34 (float) fishing bobber
         * 35 (float) fishing bobber
         * 36 (float) fishing bobber */
        public const int DATA_POTION_AUX_VALUE = 37; //short
        public const int DATA_LEAD_HOLDER_EID = 38; //long
        public const int DATA_SCALE = 39; //float
        public const int DATA_INTERACTIVE_TAG = 40; //string (button text)
        public const int DATA_NPC_SKIN_ID = 41; //string
        public const int DATA_URL_TAG = 42; //string
        public const int DATA_MAX_AIR = 43; //short
        public const int DATA_MARK_VARIANT = 44; //int
        /* 45 (byte) container stuff
         * 46 (int) container stuff
         * 47 (int) container stuff */
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
        /* 77 (int)
         * 78 (int) */


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
        public const int DATA_FLAG_RESTING = 22;
        public const int DATA_FLAG_SITTING = 23;
        public const int DATA_FLAG_ANGRY = 24;
        public const int DATA_FLAG_INTERESTED = 25;
        public const int DATA_FLAG_CHARGED = 26;
        public const int DATA_FLAG_TAMED = 27;
        public const int DATA_FLAG_LEASHED = 28;
        public const int DATA_FLAG_SHEARED = 29;
        public const int DATA_FLAG_GLIDING = 30;
        public const int DATA_FLAG_ELDER = 31;
        public const int DATA_FLAG_MOVING = 32;
        public const int DATA_FLAG_BREATHING = 33;
        public const int DATA_FLAG_CHESTED = 34;
        public const int DATA_FLAG_STACKABLE = 35;
        public const int DATA_FLAG_SHOWBASE = 36;
        public const int DATA_FLAG_REARING = 37;
        public const int DATA_FLAG_VIBRATING = 38;
        public const int DATA_FLAG_IDLING = 39;
        public const int DATA_FLAG_EVOKER_SPELL = 40;
        public const int DATA_FLAG_CHARGE_ATTACK = 41;
        public const int DATA_FLAG_WASD_CONTROLLED = 42;
        public const int DATA_FLAG_CAN_POWER_JUMP = 43;
        public const int DATA_FLAG_LINGER = 44;
        public const int DATA_FLAG_HAS_COLLISION = 45;
        public const int DATA_FLAG_AFFECTED_BY_GRAVITY = 46;
        public const int DATA_FLAG_FIRE_IMMUNE = 47;
        public const int DATA_FLAG_DANCING = 48;

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
            SetEntityDataPacket pk = new SetEntityDataPacket();
            pk.EntityRuntimeId = this.EntityID;
            pk.EntityData = this.DataProperties;

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

        public bool ShowNameTag
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

        public bool AlwaysShowNameTag
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

        public string InteractiveTag //カーソルを合わせたときに出るボタン
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

        public bool Sneaking
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
    }
}
