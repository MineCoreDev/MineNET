using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class LevelSoundEventPacket : DataPacket
    {
        public const int SOUND_ITEM_USE_ON = 0;
        public const int SOUND_HIT = 1;
        public const int SOUND_STEP = 2;
        public const int SOUND_FLY = 3;
        public const int SOUND_JUMP = 4;
        public const int SOUND_BREAK = 5;
        public const int SOUND_PLACE = 6;
        public const int SOUND_HEAVY_STEP = 7;
        public const int SOUND_GALLOP = 8;
        public const int SOUND_FALL = 9;
        public const int SOUND_AMBIENT = 10;
        public const int SOUND_AMBIENT_BABY = 11;
        public const int SOUND_AMBIENT_IN_WATER = 12;
        public const int SOUND_BREATHE = 13;
        public const int SOUND_DEATH = 14;
        public const int SOUND_DEATH_IN_WATER = 15;
        public const int SOUND_DEATH_TO_ZOMBIE = 16;
        public const int SOUND_HURT = 17;
        public const int SOUND_HURT_IN_WATER = 18;
        public const int SOUND_MAD = 19;
        public const int SOUND_BOOST = 20;
        public const int SOUND_BOW = 21;
        public const int SOUND_SQUISH_BIG = 22;
        public const int SOUND_SQUISH_SMALL = 23;
        public const int SOUND_FALL_BIG = 24;
        public const int SOUND_FALL_SMALL = 25;
        public const int SOUND_SPLASH = 26;
        public const int SOUND_FIZZ = 27;
        public const int SOUND_FLAP = 28;
        public const int SOUND_SWIM = 29;
        public const int SOUND_DRINK = 30;
        public const int SOUND_EAT = 31;
        public const int SOUND_TAKEOFF = 32;
        public const int SOUND_SHAKE = 33;
        public const int SOUND_PLOP = 34;
        public const int SOUND_LAND = 35;
        public const int SOUND_SADDLE = 36;
        public const int SOUND_ARMOR = 37;
        public const int SOUND_ADD_CHEST = 38;
        public const int SOUND_THROW = 39;
        public const int SOUND_ATTACK = 40;
        public const int SOUND_ATTACK_NODAMAGE = 41;
        public const int SOUND_ATTACK_STRONG = 42;
        public const int SOUND_WARN = 43;
        public const int SOUND_SHEAR = 44;
        public const int SOUND_MILK = 45;
        public const int SOUND_THUNDER = 46;
        public const int SOUND_EXPLODE = 47;
        public const int SOUND_FIRE = 48;
        public const int SOUND_IGNITE = 49;
        public const int SOUND_FUSE = 50;
        public const int SOUND_STARE = 51;
        public const int SOUND_SPAWN = 52;
        public const int SOUND_SHOOT = 53;
        public const int SOUND_BREAK_BLOCK = 54;
        public const int SOUND_LAUNCH = 55;
        public const int SOUND_BLAST = 56;
        public const int SOUND_LARGE_BLAST = 57;
        public const int SOUND_TWINKLE = 58;
        public const int SOUND_REMEDY = 59;
        public const int SOUND_UNFECT = 60;
        public const int SOUND_LEVELUP = 61;
        public const int SOUND_BOW_HIT = 62;
        public const int SOUND_BULLET_HIT = 63;
        public const int SOUND_EXTINGUISH_FIRE = 64;
        public const int SOUND_ITEM_FIZZ = 65;
        public const int SOUND_CHEST_OPEN = 66;
        public const int SOUND_CHEST_CLOSED = 67;
        public const int SOUND_SHULKERBOX_OPEN = 68;
        public const int SOUND_SHULKERBOX_CLOSED = 69;
        public const int SOUND_POWER_ON = 70;
        public const int SOUND_POWER_OFF = 71;
        public const int SOUND_ATTACH = 72;
        public const int SOUND_DETACH = 73;
        public const int SOUND_DENY = 74;
        public const int SOUND_TRIPOD = 75;
        public const int SOUND_POP = 76;
        public const int SOUND_DROP_SLOT = 77;
        public const int SOUND_NOTE = 78;
        public const int SOUND_THORNS = 79;
        public const int SOUND_PISTON_IN = 80;
        public const int SOUND_PISTON_OUT = 81;
        public const int SOUND_PORTAL = 82;
        public const int SOUND_WATER = 83;
        public const int SOUND_LAVA_POP = 84;
        public const int SOUND_LAVA = 85;
        public const int SOUND_BURP = 86;
        public const int SOUND_BUCKET_FILL_WATER = 87;
        public const int SOUND_BUCKET_FILL_LAVA = 88;
        public const int SOUND_BUCKET_EMPTY_WATER = 89;
        public const int SOUND_BUCKET_EMPTY_LAVA = 90;
        public const int SOUND_RECORD_13 = 91;
        public const int SOUND_RECORD_CAT = 92;
        public const int SOUND_RECORD_BLOCKS = 93;
        public const int SOUND_RECORD_CHIRP = 94;
        public const int SOUND_RECORD_FAR = 95;
        public const int SOUND_RECORD_MALL = 96;
        public const int SOUND_RECORD_MELLOHI = 97;
        public const int SOUND_RECORD_STAL = 98;
        public const int SOUND_RECORD_STRAD = 99;
        public const int SOUND_RECORD_WARD = 100;
        public const int SOUND_RECORD_11 = 101;
        public const int SOUND_RECORD_WAIT = 102;
        public const int SOUND_GUARDIAN_FLOP = 104;
        public const int SOUND_ELDERGUARDIAN_CURSE = 105;
        public const int SOUND_MOB_WARNING = 106;
        public const int SOUND_MOB_WARNING_BABY = 107;
        public const int SOUND_TELEPORT = 108;
        public const int SOUND_SHULKER_OPEN = 109;
        public const int SOUND_SHULKER_CLOSE = 110;
        public const int SOUND_HAGGLE = 111;
        public const int SOUND_HAGGLE_YES = 112;
        public const int SOUND_HAGGLE_NO = 113;
        public const int SOUND_HAGGLE_IDLE = 114;
        public const int SOUND_CHORUSGROW = 115;
        public const int SOUND_CHORUSDEATH = 116;
        public const int SOUND_GLASS = 117;
        public const int SOUND_CAST_SPELL = 118;
        public const int SOUND_PREPARE_ATTACK = 119;
        public const int SOUND_PREPARE_SUMMON = 120;
        public const int SOUND_PREPARE_WOLOLO = 121;
        public const int SOUND_FANG = 122;
        public const int SOUND_CHARGE = 123;
        public const int SOUND_CAMERA_TAKE_PICTURE = 124;
        public const int SOUND_LEASHKNOT_PLACE = 125;
        public const int SOUND_LEASHKNOT_BREAK = 126;
        public const int SOUND_GROWL = 127;
        public const int SOUND_WHINE = 128;
        public const int SOUND_PANT = 129;
        public const int SOUND_PURR = 130;
        public const int SOUND_PURREOW = 131;
        public const int SOUND_DEATH_MIN_VOLUME = 132;
        public const int SOUND_DEATH_MID_VOLUME = 133;
        public const int SOUND_IMITATE_BLAZE = 134;
        public const int SOUND_IMITATE_CAVE_SPIDER = 135;
        public const int SOUND_IMITATE_CREEPER = 136;
        public const int SOUND_IMITATE_ELDER_GUARDIAN = 137;
        public const int SOUND_IMITATE_ENDER_DRAGON = 138;
        public const int SOUND_IMITATE_ENDERMAN = 139;
        public const int SOUND_IMITATE_EVOCATION_ILLAGER = 141;
        public const int SOUND_IMITATE_GHAST = 142;
        public const int SOUND_IMITATE_HUSK = 143;
        public const int SOUND_IMITATE_ILLUSION_ILLAGER = 144;
        public const int SOUND_IMITATE_MAGMA_CUBE = 145;
        public const int SOUND_IMITATE_POLAR_BEAR = 146;
        public const int SOUND_IMITATE_SHULKER = 147;
        public const int SOUND_IMITATE_SILVERFISH = 148;
        public const int SOUND_IMITATE_SKELETON = 149;
        public const int SOUND_IMITATE_SLIME = 150;
        public const int SOUND_IMITATE_SPIDER = 151;
        public const int SOUND_IMITATE_STRAY = 152;
        public const int SOUND_IMITATE_VEX = 153;
        public const int SOUND_IMITATE_VINDICATION_ILLAGER = 154;
        public const int SOUND_IMITATE_WITCH = 155;
        public const int SOUND_IMITATE_WITHER = 156;
        public const int SOUND_IMITATE_WITHER_SKELETON = 157;
        public const int SOUND_IMITATE_WOLF = 158;
        public const int SOUND_IMITATE_ZOMBIE = 159;
        public const int SOUND_IMITATE_ZOMBIE_PIGMAN = 160;
        public const int SOUND_IMITATE_ZOMBIE_VILLAGER = 161;
        public const int SOUND_BLOCK_END_PORTAL_FRAME_FILL = 162;
        public const int SOUND_BLOCK_END_PORTAL_SPAWN = 163;
        public const int SOUND_RANDOM_ANVIL_USE = 164;
        public const int SOUND_BOTTLE_DRAGONBREATH = 165;
        public const int SOUND_DEFAULT = 166;
        public const int SOUND_UNDEFINED = 167;

        public const int ID = ProtocolInfo.LEVEL_SOUND_EVENT_PACKET;

        public override byte PacketID
        {
            get
            {
                return LevelSoundEventPacket.ID;
            }
        }

        public byte Sound { get; set; }
        public Vector3 Position { get; set; }
        public int ExtraData { get; set; } = -1;
        public int Pitch { get; set; } = 1;
        public bool IsBabyMob { get; set; }
        public bool IsGlobal { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Sound);
            this.WriteVector3(this.Position);
            this.WriteSVarInt(this.ExtraData);
            this.WriteSVarInt(this.Pitch);
            this.WriteBool(this.IsBabyMob);
            this.WriteBool(this.IsGlobal);
        }

        public override void Decode()
        {
            base.Decode();

            this.Sound = this.ReadByte();
            this.Position = this.ReadVector3();
            this.ExtraData = this.ReadSVarInt();
            this.Pitch = this.ReadSVarInt();
            this.IsBabyMob = this.ReadBool();
            this.IsGlobal = this.ReadBool();
        }
    }
}
