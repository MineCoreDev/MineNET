using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFactory
    {
        public const int AIR = 0;
        public const int STONE = 1;
        public const int GRASS = 2;
        public const int DIRT = 3;
        public const int COBBLESTONE = 4;
        public const int PLANKS = 5;
        public const int SAPLING = 6;
        public const int BEDROCK = 7;
        public const int FLOWING_WATER = 8;
        public const int WATER = 9;
        public const int FLOWING_LAVA = 10;
        public const int LAVA = 11;
        public const int SAND = 12;
        public const int GRAVEL = 13;
        public const int GOLD_ORE = 14;
        public const int IRON_ORE = 15;
        public const int COAL_ORE = 16;
        public const int LOG = 17;
        public const int LEAVES = 18;
        public const int SPONGE = 19;
        public const int GLASS = 20;
        public const int LAPIS_ORE = 21;
        public const int LAPIS_BLOCK = 22;
        public const int DISPENSER = 23;
        public const int SANDSTONE = 24;
        public const int NOTEBLOCK = 25;
        public const int BED = 26;
        public const int GOLDEN_RAIL = 27;
        public const int DETECTOR_RAIL = 28;
        public const int STICKY_PISTON = 29;
        public const int WEB = 30;
        public const int TALLGRASS = 31;
        public const int DEADBUSH = 32;
        public const int PISTON = 33;
        public const int PISTON_HEAD = 34;
        public const int WOOL = 35;

        public const int YELLOW_FLOWER = 37;
        public const int RED_FLOWER = 38;
        public const int BROWN_MUSHROOM = 39;
        public const int RED_MUSHROOM = 40;
        public const int GOLD_BLOCK = 41;
        public const int IRON_BLOCK = 42;
        public const int DOUBLE_STONE_SLAB = 43;
        public const int STONE_SLAB = 44;
        public const int BRICK_BLOCK = 45;
        public const int TNT = 46;
        public const int BOOKSHELF = 47;
        public const int MOSSY_COBBLESTONE = 48;
        public const int OBSIDIAN = 49;
        public const int TORCH = 50;
        public const int FIRE = 51;
        public const int MOB_SPAWNER = 52;
        public const int OAK_STAIRS = 53;
        public const int CHEST = 54;
        public const int REDSTONE_WIRE = 55;
        public const int DIAMOND_ORE = 56;
        public const int DIAMOND_BLOCK = 57;
        public const int CRAFTING_TABLE = 58;
        public const int WHEAT = 59;
        public const int FARMLAND = 60;
        public const int FURNACE = 61;
        public const int LIT_FURNACE = 62;
        public const int STANDING_SIGN = 63;
        public const int WOODEN_DOOR = 64;
        public const int LADDER = 65;
        public const int RAIL = 66;
        public const int STONE_STAIRS = 67;
        public const int WALL_SIGN = 68;
        public const int LEVER = 69;
        public const int STONE_PRESSURE_PLATE = 70;
        public const int IRON_DOOR = 71;
        public const int WOODEN_PRESSURE_PLATE = 72;
        public const int REDSTONE_ORE = 73;
        public const int LIT_REDSTONE_TORCH = 74;
        public const int UNLIT_REDSTONE_TORCH = 75;
        public const int REDSTONE_TORCH = 76;
        public const int STONE_BUTTON = 77;
        public const int SNOW_LAYER = 78;
        public const int ICE = 79;
        public const int SNOW = 80;
        public const int CACTUS = 81;
        public const int CLAY = 82;
        public const int REEDS = 83;
        public const int JUKEBOX = 84;
        public const int FENCE = 85;
        public const int PUMPKIN = 86;
        public const int NETHERRACK = 87;
        public const int SOUL_SAND = 88;
        public const int GLOWSTONE = 89;
        public const int PORTAL = 90;
        public const int LIT_PUMPKIN = 91;
        public const int CAKE = 92;
        public const int UNPOWERED_REPEATER = 93;
        public const int POWERED_REPEATER = 94;
        public const int INVISIBLEBEDROCK = 95;
        public const int TRAPDOOR = 96;
        public const int MONSTER_EGG = 97;
        public const int STONEBRICK = 98;
        public const int BROWN_MUSHROOM_BLOCK = 99;
        public const int RED_MUSHROOM_BLOCK = 100;
        public const int IRON_BARS = 101;
        public const int GLASS_PANE = 102;
        public const int MELON_BLOCK = 103;
        public const int PUMPKIN_STEM = 104;
        public const int MELON_STEM = 105;
        public const int VINE = 106;
        public const int FENCE_GATE = 107;
        public const int BRICK_STAIRS = 108;
        public const int STONE_BRICK_STAIRS = 109;
        public const int MYCELIUM = 110;
        public const int WATERLILY = 111;
        public const int NETHER_BRICK = 112;
        public const int NETHER_BRICK_FENCE = 113;
        public const int NETHER_BRICK_STAIRS = 114;
        public const int NETHER_WART = 115;
        public const int ENCHANTING_TABLE = 116;
        public const int BREWING_STAND = 117;
        public const int CAULDRON = 118;
        public const int END_PORTAL = 119;
        public const int END_PORTAL_FRAME = 120;
        public const int END_STONE = 121;
        public const int DRAGON_EGG = 122;
        public const int REDSTONE_LAMP = 123;
        public const int LIT_REDSTONE_LAMP = 124;
        public const int DROPPER = 125;
        public const int ACTIVATOR_RAIL = 126;
        public const int COCOA = 127;
        public const int SANDSTONE_SLAB = 128;
        public const int EMERALD_ORE = 129;
        public const int ENDER_CHEST = 130;
        public const int TRIPWIRE_HOOK = 131;
        public const int TRIPWIRE = 132;
        public const int EMERALD_BLOCK = 133;
        public const int SPRUCE_STAIRS = 134;
        public const int BIRCH_STAIRS = 135;
        public const int JUNGLE_STAIRS = 136;
        public const int COMMAND_BLOCK = 137;
        public const int BEACON = 138;
        public const int COBBLESTONE_WALL = 139;
        public const int FLOWER_POT = 140;
        public const int CARROTS = 141;
        public const int POTATOES = 142;
        public const int WOODEN_BUTTON = 143;
        public const int SKULL = 144;
        public const int ANVIL = 145;
        public const int TRAPPED_CHEST = 146;
        public const int LIGHT_WEIGHTED_PRESSURE_PLATE = 147;
        public const int HEAVY_WEIGHTED_PRESSURE_PLATE = 148;
        public const int UNPOWERED_COMPARATOR = 149;
        public const int POWERED_COMPARATOR = 150;
        public const int DAYLIGHT_DEteCTOR = 151;
        public const int REDSTONE_BLOCK = 152;
        public const int QUARTZ_ORE = 153;
        public const int HOPPER = 154;
        public const int QUARTZ_BLOCK = 155;
        public const int QUARTZ_STAIRS = 156;
        public const int DOUBLE_WOODEN_SLAB = 157;
        public const int WOODEN_SLAB = 158;
        public const int STAINED_HARDENED_CLAY = 159;
        public const int STAINED_GLASS_PANE = 160;
        public const int LEAVES2 = 161;
        public const int LOG2 = 162;
        public const int ACACIA_STAIRS = 163;
        public const int DARK_OAK_STAIRS = 164;
        public const int SLIME = 165;
        
        public const int IRON_TRAPDOOR = 167;
        public const int PRISMARINE = 168;
        public const int SEA_LANTERN = 169;
        public const int HAY_BLOCK = 170;
        public const int CARPET = 171;
        public const int HARDENED_CLAY = 172;
        public const int COAL_BLOCK = 173;
        public const int PACKED_ICE = 174;
        public const int DOUBLE_PLANT = 175;
        public const int STANDING_BANNER = 176;
        public const int WALL_BANNER = 177;
        public const int DAYLIGHT_DETECTOR_INVERTED = 178;
        public const int RED_SANDSTONE = 179;
        public const int RED_SANDSTONE_STAIRS = 180;
        public const int DOUBLE_STONE_SLAB2 = 181;
        public const int STONE_SLAB2 = 182;
        public const int SPRUCE_FENCE_GATE = 183;
        public const int BIRCH_FENCE_GATE = 184;
        public const int JUNGLE_FENCE_FATE = 185;
        public const int DARK_OAK_FENCE_GATE = 186;
        public const int ACACIA_FENCE_GATE = 187;
        public const int REPEATING_COMMAND_BLOCK = 188;
        public const int CHAIN_COMMAND_BLOCK = 189;

        public const int SPRUCE_DOOR = 193;
        public const int BIRCH_DOOR = 194;
        public const int JUNGLE_DOOR = 195;
        public const int ACACIA_DOOR = 196;
        public const int DARK_OAK_DOOR = 197;
        public const int GRASS_PATH = 198;
        public const int FRAME = 199;
        public const int CHORUS_FLOWER = 200;
        public const int PURPUR_BLOCK = 201;
        public const int PURPUR_STAIRS = 202;

        public const int UNDYED_SHULKER_BOX = 205;
        public const int END_BRICKS = 206;
        public const int FROSTED_ICE = 207;
        public const int END_ROD = 208;
        public const int END_GATEWAY = 209;

        public const int MAGMA = 213;
        public const int NETHER_WART_BLOCK = 214;
        public const int RED_NETHER_BRICK = 215;
        public const int BONE_BLOCK = 216;

        public const int SHULKER_BOX = 218;
        public const int PURPLE_GLAZED_TERRACOTTA = 219;
        public const int WHITE_GLAZED_TERRACOTTA = 220;
        public const int ORANGE_GLAZED_TERRACOTTA = 221;
        public const int MAGENTA_GLAZED_TERRACOTTA = 222;
        public const int LIGHT_GLAZED_TERRACOTTA = 223;
        public const int YELLOW_GLAZED_TERRACOTTA = 224;
        public const int LIME_GLAZED_TERRACOTTA = 225;
        public const int PINK_GLAZED_TERRACOTTA = 226;
        public const int GRAY_GLAZED_TERRACOTTA = 227;
        public const int SILVER_GLAZED_TERRACOTTA = 228;
        public const int CYAN_GLAZED_TERRACOTTA = 229;

        public const int BLUE_GLAZED_TERRACOTTA = 231;
        public const int BROWN_GLAZED_TERRACOTTA = 232;
        public const int GREEN_GLAZED_TERRACOTTA = 233;
        public const int RED_GLAZED_TERRACOTTA = 234;
        public const int BLACK_GLAZED_TERRACOTTA = 235;
        public const int CONCRETE = 236;
        public const int CONCRETE_POWDER = 237;

        public const int CHORUS_PLANT = 240;
        public const int STAINED_GLASS = 241;

        public const int PODZOL = 243;
        public const int BEETROOT = 244;
        public const int STONECUTTER = 245;
        public const int GLOWINGOBSIDIAN = 246;
        public const int NETHERREACTOR = 247;
        public const int INFO_UPDATE = 248;
        public const int INFO_UPDATE2 = 249;
        public const int MOVINGBLOCK = 250;
        public const int OBSERVER = 251;
        public const int STRUCTURE_BLOCK = 252;

        public static Type[] blockFactory = new Type[256];

        public static void Init()
        {
            blockFactory[AIR] = typeof(BlockAir);
        }

        public static Type GetBlockType(byte id)
        {
            return blockFactory[id];
        }

        public static Block GetBlock(byte id)
        {
            if (id == AIR) return new BlockAir();
            //else if (id == STONE) return new BlockStone();
            else return null;
        }

        [Obsolete("")]
        public static Block CreateInstance(byte id)
        {
            Type t = GetBlockType(id);
            return (Block)Activator.CreateInstance(t);
        }
    }
}
