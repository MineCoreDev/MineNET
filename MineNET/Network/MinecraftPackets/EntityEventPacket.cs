namespace MineNET.Network.MinecraftPackets
{
    public class EntityEventPacket : MinecraftPacket
    {
        public const int HURT_ANIMATION = 2;
        public const int DEATH_ANIMATION = 3;
        public const int ARM_SWING = 4;

        public const int TAME_FAIL = 6;
        public const int TAME_SUCCESS = 7;
        public const int SHAKE_WET = 8;
        public const int USE_ITEM = 9;
        public const int EAT_GRASS_ANIMATION = 10;
        public const int FISH_HOOK_BUBBLE = 11;
        public const int FISH_HOOK_POSITION = 12;
        public const int FISH_HOOK_HOOK = 13;
        public const int FISH_HOOK_TEASE = 14;
        public const int SQUID_INK_CLOUD = 15;
        public const int ZOMBIE_VILLAGER_CURE = 16;

        public const int RESPAWN = 18;
        public const int IRON_GOLEM_OFFER_FLOWER = 19;
        public const int IRON_GOLEM_WITHDRAW_FLOWER = 20;
        public const int LOVE_PARTICLES = 21; //breeding

        public const int WITCH_SPELL_PARTICLES = 24;
        public const int FIREWORK_PARTICLES = 25;

        public const int SILVERFISH_SPAWN_ANIMATION = 27;

        public const int WITCH_DRINK_POTION = 29;
        public const int WITCH_THROW_POTION = 30;
        public const int MINECART_TNT_PRIME_FUSE = 31;

        public const int PLAYER_ADD_XP_LEVELS = 34;
        public const int ELDER_GUARDIAN_CURSE = 35;
        public const int AGENT_ARM_SWING = 36;
        public const int ENDER_DRAGON_DEATH = 37;
        public const int DUST_PARTICLES = 38; //not sure what this is

        public const int EATING_ITEM = 57;

        public const int BABY_ANIMAL_FEED = 60; //green particles, like bonemeal on crops
        public const int DEATH_SMOKE_CLOUD = 61;
        public const int COMPLETE_TRADE = 62;
        public const int REMOVE_LEASH = 63; //data 1 = cut leash

        public const int CONSUME_TOTEM = 65;
        public const int PLAYER_CHECK_TREASURE_HUNTER_ACHIEVEMENT = 66; //mojang...
        public const int ENTITY_SPAWN = 67; //used for MinecraftEventing stuff, not needed
        public const int DRAGON_PUKE = 68; //they call this puke particles
        public const int ITEM_ENTITY_MERGE = 69;

        public override byte PacketID { get; } = MinecraftProtocol.ENTITY_EVENT_PACKET;

        public long EntityRuntimeId { get; set; }
        public byte EventId { get; set; }
        public int Data { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteByte(this.EventId);
            this.WriteSVarInt(this.Data);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.EventId = this.ReadByte();
            this.Data = this.ReadSVarInt();
        }
    }
}
