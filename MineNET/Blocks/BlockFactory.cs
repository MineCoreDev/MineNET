using System;
using System.Collections.Generic;

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
        public const int COBWEB = 30;
        public const int TALLGRASS = 31;
        public const int DEADBUSH = 32;
        public const int PISTON = 33;
        public const int PISTON_HEAD = 34;
        public const int WOOL = 35;
        public const int ELEMENT_0 = 36;
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
        public const int LIT_REDSTONE_ORE = 74;
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
        public const int DAYLIGHT_DETECTOR = 151;
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
        public const int GLOW_STICK = 166;
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
        public const int JUNGLE_FENCE_GATE = 185;
        public const int DARK_OAK_FENCE_GATE = 186;
        public const int ACACIA_FENCE_GATE = 187;
        public const int REPEATING_COMMAND_BLOCK = 188;
        public const int CHAIN_COMMAND_BLOCK = 189;
        public const int HARD_GLASS_PANE = 190;
        public const int HARD_STAINED_GLASS_PANE = 191;
        public const int CHEMICAL_HEAT = 192;
        public const int SPRUCE_DOOR = 193;
        public const int BIRCH_DOOR = 194;
        public const int JUNGLE_DOOR = 195;
        public const int ACACIA_DOOR = 196;
        public const int DARK_OAK_DOOR = 197;
        public const int GRASS_PATH = 198;
        public const int FRAME = 199;
        public const int CHORUS_FLOWER = 200;
        public const int PURPUR_BLOCK = 201;
        public const int COLORED_TORCH_RG = 202;
        public const int PURPUR_STAIRS = 203;
        public const int COLORED_TORCH_BP = 204;
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
        public const int CHEMISTRY_TABLE = 238;
        public const int UNDERWATER_TORCH = 239;
        public const int CHORUS_PLANT = 240;
        public const int STAINED_GLASS = 241;

        public const int PODZOL = 243;
        public const int BEETROOT = 244;
        public const int STONECUTTER = 245;
        public const int GLOWINGOBSIDIAN = 246;
        public const int NETHERREACTOR = 247;
        public const int INFO_UPDATE = 248;
        public const int INFO_UPDATE2 = 249;
        public const int MOVING_BLOCK = 250;
        public const int OBSERVER = 251;
        public const int STRUCTURE_BLOCK = 252;
        public const int HARD_GLASS = 253;
        public const int HARD_STAINED_GLASS = 254;
        public const int RESERVED6 = 255;

        public const int PRISMARINE_STAIRS = 257;
        public const int DARK_PRISMARINE_STAIRS = 258;
        public const int PRISMARINE_BRICKS_STAIRS = 259;
        public const int STRIPPED_SPRUCE_LOG = 260;
        public const int STRIPPED_BIRCH_LOG = 261;
        public const int STRIPPED_JUNGLE_LOG = 262;
        public const int STRIPPED_ACACIA_LOG = 263;
        public const int STRIPPED_DARK_OAK_LOG = 264;
        public const int STRIPPED_OAK_LOG = 265;
        public const int BLUE_ICE = 266;
        public const int ELEMENT_1 = 267;
        //TODO
        public const int ELEMENT_118 = 384;
        public const int SEAGRASS = 385;
        public const int CORAL = 386;
        public const int CORAL_BLOCK = 387;
        public const int CORAL_FAN = 388;
        public const int CORALFAN_DEAD = 389;
        public const int CORAL_FAN_HANG = 390;
        public const int CORAL_FAN_HANG2 = 391;
        public const int CORAL_FAN_HANG3 = 392;
        public const int KELP = 393;
        public const int DRIED_KELP_BLOCK = 394;
        public const int ACACIA_BUTTON = 395;
        public const int BIRCH_BUTTON = 396;
        public const int DARK_OAK_BUTTON = 397;
        public const int JUNGLE_BUTTON = 398;
        public const int SPRUCE_BUTTON = 399;
        public const int ACACIA_TRAPDOOR = 400;
        public const int BIRCH_TRAPDOOR = 401;
        public const int DARK_OAK_TRAPDOOR = 402;
        public const int JUNGLE_TRAPDOOR = 403;
        public const int SPRUCE_TRAPDOOR = 404;
        public const int ACACIA_PRESSURE_PLATE = 405;
        public const int BIRCH_PRESSURE_PLATE = 406;
        public const int DARK_OAK_PRESSURE_PLATE = 407;
        public const int JUNGLE_PRESSURE_PLATE = 408;
        public const int SPRUCE_PRESSURE_PLATE = 409;
        public const int CARVED_PUMPKIN = 410;
        public const int SEA_PICKLE = 411;

        private static Dictionary<int, Type> Blocks = new Dictionary<int, Type>();

        public static Block GetBlock(int id)
        {
            if (BlockFactory.Blocks.ContainsKey(id))
            {
                try
                {
                    Type type = BlockFactory.Blocks[id];
                    return (Block) Activator.CreateInstance(type);
                }
                catch { }
            }
            if (id == AIR)
            {
                return new BlockAir();
            }
            else if (id == STONE)
            {
                return new BlockStone();
            }
            else if (id == GRASS)
            {
                return new BlockGrass();
            }
            else if (id == DIRT)
            {
                return new BlockDirt();
            }
            else if (id == COBBLESTONE)
            {
                return new BlockCobblestone();
            }
            else if (id == PLANKS)
            {
                return new BlockPlanks();
            }
            else if (id == SAPLING)
            {
                return new BlockSapling();
            }
            else if (id == BEDROCK)
            {
                return new BlockBedrock();
            }
            else if (id == FLOWING_WATER)
            {
                return new BlockFlowingWater();
            }
            else if (id == WATER)
            {
                return new BlockWater();
            }
            else if (id == FLOWING_LAVA)
            {
                return new BlockFlowingLava();
            }
            else if (id == LAVA)
            {
                return new BlockLava();
            }
            else if (id == SAND)
            {
                return new BlockSand();
            }
            else if (id == GRAVEL)
            {
                return new BlockGravel();
            }
            else if (id == GOLD_ORE)
            {
                return new BlockGoldOre();
            }
            else if (id == IRON_ORE)
            {
                return new BlockIronOre();
            }
            else if (id == COAL_ORE)
            {
                return new BlockCoalOre();
            }
            else if (id == LOG)
            {
                return new BlockLog();
            }
            else if (id == LEAVES)
            {
                return new BlockLeaves();
            }
            else if (id == SPONGE)
            {
                return new BlockSponge();
            }
            else if (id == GLASS)
            {
                return new BlockGlass();
            }
            else if (id == LAPIS_ORE)
            {
                return new BlockLapisOre();
            }
            else if (id == LAPIS_BLOCK)
            {
                return new BlockLapisBlock();
            }
            else if (id == DISPENSER)
            {
                return new BlockDispenser();
            }
            else if (id == SANDSTONE)
            {
                return new BlockSandstone();
            }
            else if (id == NOTEBLOCK)
            {
                return new BlockNoteBlock();
            }
            else if (id == BED)
            {
                return new BlockBed();
            }
            else if (id == GOLDEN_RAIL)
            {
                return new BlockGoldenRail();
            }
            else if (id == DETECTOR_RAIL)
            {
                return new BlockDetectorRail();
            }
            else if (id == STICKY_PISTON)
            {
                return new BlockPistonSticky();
            }
            else if (id == COBWEB)
            {
                return new BlockCobweb();
            }
            else if (id == TALLGRASS)
            {
                return new BlockTallgrass();
            }
            else if (id == DEADBUSH)
            {
                return new BlockDeadbush();
            }
            else if (id == PISTON)
            {
                return new BlockPiston();
            }
            else if (id == PISTON_HEAD)
            {
                return new BlockPistonHead();
            }
            else if (id == WOOL)
            {
                return new BlockWool();
            }

            else if (id == YELLOW_FLOWER)
            {
                return new BlockYellowFlower();
            }
            else if (id == RED_FLOWER)
            {
                return new BlockRedFlower();
            }
            else if (id == BROWN_MUSHROOM)
            {
                return new BlockBrownMushroom();
            }
            else if (id == RED_MUSHROOM)
            {
                return new BlockRedMushroom();
            }
            else if (id == GOLD_BLOCK)
            {
                return new BlockGoldBlock();
            }
            else if (id == IRON_BLOCK)
            {
                return new BlockIronBlock();
            }
            else if (id == DOUBLE_STONE_SLAB)
            {
                return new BlockDoubleStoneSlab();
            }
            else if (id == STONE_SLAB)
            {
                return new BlockStoneSlab();
            }
            else if (id == BRICK_BLOCK)
            {
                return new BlockBrickBlock();
            }
            else if (id == TNT)
            {
                return new BlockTnt();
            }
            else if (id == BOOKSHELF)
            {
                return new BlockBookshelf();
            }
            else if (id == MOSSY_COBBLESTONE)
            {
                return new BlockCobblestoneMossy();
            }
            else if (id == OBSIDIAN)
            {
                return new BlockObsidian();
            }
            else if (id == TORCH)
            {
                return new BlockTorch();
            }
            else if (id == FIRE)
            {
                return new BlockFire();
            }
            else if (id == MOB_SPAWNER)
            {
                return new BlockMobSpawner();
            }
            else if (id == OAK_STAIRS)
            {
                return new BlockOakStairs();
            }
            else if (id == CHEST)
            {
                return new BlockChest();
            }
            else if (id == REDSTONE_WIRE)
            {
                return new BlockRedstoneWire();
            }
            else if (id == DIAMOND_ORE)
            {
                return new BlockDiamondOre();
            }
            else if (id == DIAMOND_BLOCK)
            {
                return new BlockDiamondBlock();
            }
            else if (id == CRAFTING_TABLE)
            {
                return new BlockCraftingTable();
            }
            else if (id == WHEAT)
            {
                return new BlockWheat();
            }
            else if (id == FARMLAND)
            {
                return new BlockFarmland();
            }
            else if (id == FURNACE)
            {
                return new BlockFurnace();
            }
            else if (id == LIT_FURNACE)
            {
                return new BlockFurnaceLit();
            }
            else if (id == STANDING_SIGN)
            {
                return new BlockStandingSign();
            }
            else if (id == WOODEN_DOOR)
            {
                return new BlockDoorWooden();
            }
            else if (id == LADDER)
            {
                return new BlockLadder();
            }
            else if (id == RAIL)
            {
                return new BlockRail();
            }
            else if (id == STONE_STAIRS)
            {
                return new BlockStoneStairs();
            }
            else if (id == WALL_SIGN)
            {
                return new BlockWallSign();
            }
            else if (id == LEAVES)
            {
                return new BlockLeaves();
            }
            else if (id == STONE_PRESSURE_PLATE)
            {
                return new BlockStonePressurePlate();
            }
            else if (id == IRON_DOOR)
            {
                return new BlockDoorIron();
            }
            else if (id == WOODEN_PRESSURE_PLATE)
            {
                return new BlockWoodenPressurePlate();
            }
            else if (id == REDSTONE_ORE)
            {
                return new BlockRedstoneOre();
            }
            else if (id == LIT_REDSTONE_ORE)
            {
                return new BlockRedstoneOreLit();
            }
            else if (id == UNLIT_REDSTONE_TORCH)
            {
                return new BlockRedstoneTorchUnlit();
            }
            else if (id == REDSTONE_TORCH)
            {
                return new BlockRedstoneTorch();
            }
            else if (id == STONE_BUTTON)
            {
                return new BlockButtonStone();
            }
            else if (id == SNOW_LAYER)
            {
                return new BlockSnowLayer();
            }
            else if (id == ICE)
            {
                return new BlockIce();
            }
            else if (id == SNOW)
            {
                return new BlockSnow();
            }
            else if (id == CACTUS)
            {
                return new BlockCactus();
            }
            else if (id == CLAY)
            {
                return new BlockClay();
            }
            else if (id == REEDS)
            {
                return new BlockReeds();
            }
            else if (id == JUKEBOX)
            {
                return new BlockJukebox();
            }
            else if (id == FENCE)
            {
                return new BlockFence();
            }
            else if (id == PUMPKIN)
            {
                return new BlockPumpkin();
            }
            else if (id == NETHERRACK)
            {
                return new BlockNetherrack();
            }
            else if (id == SOUL_SAND)
            {
                return new BlockSoulSand();
            }
            else if (id == GLOWSTONE)
            {
                return new BlockGlowstone();
            }
            else if (id == PORTAL)
            {
                return new BlockPortal();
            }
            else if (id == LIT_PUMPKIN)
            {
                return new BlockPumpkinLit();
            }
            else if (id == CAKE)
            {
                return new BlockCake();
            }
            else if (id == UNPOWERED_REPEATER)
            {
                return new BlockRepeaterUnpowered();
            }
            else if (id == POWERED_REPEATER)
            {
                return new BlockRepeaterPowered();
            }
            else if (id == INVISIBLEBEDROCK)
            {
                return new BlockInvisiblebedrock();
            }
            else if (id == TRAPDOOR)
            {
                return new BlockTrapdoor();
            }
            else if (id == MONSTER_EGG)
            {
                return new BlockMonsterEgg();
            }
            else if (id == STONEBRICK)
            {
                return new BlockStonebrick();
            }
            else if (id == BROWN_MUSHROOM_BLOCK)
            {
                return new BlockBrownMushroomBlock();
            }
            else if (id == RED_MUSHROOM_BLOCK)
            {
                return new BlockRedMushroomBlock();
            }
            else if (id == IRON_BARS)
            {
                return new BlockIronBars();
            }
            else if (id == GLASS_PANE)
            {
                return new BlockGlassPane();
            }
            else if (id == MELON_BLOCK)
            {
                return new BlockMelonBlock();
            }
            else if (id == PUMPKIN_STEM)
            {
                return new BlockPumpkinStem();
            }
            else if (id == MELON_STEM)
            {
                return new BlockMelonStem();
            }
            else if (id == VINE)
            {
                return new BlockVine();
            }
            else if (id == FENCE_GATE)
            {
                return new BlockFenceGate();
            }
            else if (id == BRICK_STAIRS)
            {
                return new BlockStairsBrick();
            }
            else if (id == STONE_BRICK_STAIRS)
            {
                return new BlockStairsStoneBrick();
            }
            else if (id == MYCELIUM)
            {
                return new BlockMycelium();
            }
            else if (id == WATERLILY)
            {
                return new BlockWaterlily();
            }
            else if (id == NETHER_BRICK)
            {
                return new BlockNetherBrick();
            }
            else if (id == NETHER_BRICK_FENCE)
            {
                return new BlockFenceNetherBrick();
            }
            else if (id == NETHER_BRICK_STAIRS)
            {
                return new BlockStairsNetherBrick();
            }
            else if (id == NETHER_WART)
            {
                return new BlockNetherWart();
            }
            else if (id == ENCHANTING_TABLE)
            {
                return new BlockEnchantingTable();
            }
            else if (id == BREWING_STAND)
            {
                return new BlockBrewingStand();
            }
            else if (id == CAULDRON)
            {
                return new BlockCauldron();
            }
            else if (id == END_PORTAL)
            {
                return new BlockEndPortal();
            }
            else if (id == END_PORTAL_FRAME)
            {
                return new BlockEndPortalFrame();
            }
            else if (id == END_STONE)
            {
                return new BlockEndStone();
            }
            else if (id == DRAGON_EGG)
            {
                return new BlockDragonEgg();
            }
            else if (id == REDSTONE_LAMP)
            {
                return new BlockRedstoneLamp();
            }
            else if (id == LIT_REDSTONE_LAMP)
            {
                return new BlockRedstoneLampLit();
            }
            else if (id == DROPPER)
            {
                return new BlockDropper();
            }
            else if (id == ACTIVATOR_RAIL)
            {
                return new BlockRailActivator();
            }
            else if (id == COCOA)
            {
                return new BlockCocoa();
            }
            else if (id == SANDSTONE_SLAB)
            {
                return new BlockSandstoneSlab();
            }
            else if (id == EMERALD_ORE)
            {
                return new BlockEmeraldOre();
            }
            else if (id == ENDER_CHEST)
            {
                return new BlockEnderChest();
            }
            else if (id == TRIPWIRE_HOOK)
            {
                return new BlockTripwireHook();
            }
            else if (id == TRIPWIRE)
            {
                return new BlockTripwire();
            }
            else if (id == EMERALD_BLOCK)
            {
                return new BlockEmeraldBlock();
            }
            else if (id == SPRUCE_STAIRS)
            {
                return new BlockStairsSpruce();
            }
            else if (id == BIRCH_STAIRS)
            {
                return new BlockStairsBirch();
            }
            else if (id == JUNGLE_STAIRS)
            {
                return new BlockStairsJungle();
            }
            else if (id == COMMAND_BLOCK)
            {
                return new BlockCommandBlock();
            }
            else if (id == BEACON)
            {
                return new BlockBeacon();
            }
            else if (id == COBBLESTONE_WALL)
            {
                return new BlockCobblestoneWall();
            }
            else if (id == FLOWER_POT)
            {
                return new BlockFlowerPot();
            }
            else if (id == CARROTS)
            {
                return new BlockCarrots();
            }
            else if (id == POTATOES)
            {
                return new BlockPotatoes();
            }
            else if (id == WOODEN_BUTTON)
            {
                return new BlockButtonWooden();
            }
            else if (id == SKULL)
            {
                return new BlockSkull();
            }
            else if (id == ANVIL)
            {
                return new BlockAnvil();
            }
            else if (id == TRAPPED_CHEST)
            {
                return new BlockTrappedChest();
            }
            else if (id == LIGHT_WEIGHTED_PRESSURE_PLATE)
            {
                return new BlockLightWeightedPressurePlate();
            }
            else if (id == HEAVY_WEIGHTED_PRESSURE_PLATE)
            {
                return new BlockHeavyWeightedPressurePlate();
            }
            else if (id == UNPOWERED_COMPARATOR)
            {
                return new BlockComparatorUnpowered();
            }
            else if (id == POWERED_COMPARATOR)
            {
                return new BlockComparatorPowered();
            }
            else if (id == DAYLIGHT_DETECTOR)
            {
                return new BlockDaylightDetector();
            }
            else if (id == REDSTONE_BLOCK)
            {
                return new BlockRedstoneBlock();
            }
            else if (id == QUARTZ_ORE)
            {
                return new BlockQuartzOre();
            }
            else if (id == HOPPER)
            {
                return new BlockHopper();
            }
            else if (id == QUARTZ_BLOCK)
            {
                return new BlockQuartzBlock();
            }
            else if (id == QUARTZ_STAIRS)
            {
                return new BlockQuartzStairs();
            }
            else if (id == DOUBLE_WOODEN_SLAB)
            {
                return new BlockDoubleWoodenSlab();
            }
            else if (id == WOODEN_SLAB)
            {
                return new BlockWoodenSlab();
            }
            else if (id == STAINED_HARDENED_CLAY)
            {
                return new BlockStainedHardenedClay();
            }
            else if (id == STAINED_GLASS_PANE)
            {
                return new BlockStainedGlassPane();
            }
            else if (id == LEAVES2)
            {
                return new BlockLeaves2();
            }
            else if (id == LOG2)
            {
                return new BlockLog2();
            }
            else if (id == ACACIA_STAIRS)
            {
                return new BlockStairsAcacia();
            }
            else if (id == DARK_OAK_STAIRS)
            {
                return new BlockStairsDarkOak();
            }
            else if (id == SLIME)
            {
                return new BlockSlime();
            }

            else if (id == IRON_TRAPDOOR)
            {
                return new BlockTrapdoorIron();
            }
            else if (id == PRISMARINE)
            {
                return new BlockPrismarine();
            }
            else if (id == SEA_LANTERN)
            {
                return new BlockSeaLantern();
            }
            else if (id == HAY_BLOCK)
            {
                return new BlockHayBlock();
            }
            else if (id == CARPET)
            {
                return new BlockCarpet();
            }
            else if (id == HARDENED_CLAY)
            {
                return new BlockClayHardened();
            }
            else if (id == COAL_BLOCK)
            {
                return new BlockCoalBlock();
            }
            else if (id == PACKED_ICE)
            {
                return new BlockPackedIce();
            }
            else if (id == DOUBLE_PLANT)
            {
                return new BlockDoublePlant();
            }
            else if (id == STANDING_BANNER)
            {
                return new BlockStandingBanner();
            }
            else if (id == WALL_BANNER)
            {
                return new BlockWallBanner();
            }
            else if (id == DAYLIGHT_DETECTOR_INVERTED)
            {
                return new BlockDaylightDetectorInverted();
            }
            else if (id == RED_SANDSTONE)
            {
                return new BlockRedSandstone();
            }
            else if (id == RED_SANDSTONE_STAIRS)
            {
                return new BlockRedSandstoneStairs();
            }
            else if (id == DOUBLE_STONE_SLAB2)
            {
                return new BlockDoubleStoneSlab2();
            }
            else if (id == STONE_SLAB2)
            {
                return new BlockStoneSlab2();
            }
            else if (id == SPRUCE_FENCE_GATE)
            {
                return new BlockFenceGateSpruce();
            }
            else if (id == BIRCH_FENCE_GATE)
            {
                return new BlockFenceGateBirch();
            }
            else if (id == JUNGLE_FENCE_GATE)
            {
                return new BlockFenceGateJungle();
            }
            else if (id == DARK_OAK_FENCE_GATE)
            {
                return new BlockFenceGateDarkOak();
            }
            else if (id == ACACIA_FENCE_GATE)
            {
                return new BlockFenceGateAcacia();
            }
            else if (id == REPEATING_COMMAND_BLOCK)
            {
                return new BlockCommandBlockRepeating();
            }
            else if (id == CHAIN_COMMAND_BLOCK)
            {
                return new BlockCommandBlockChain();
            }

            else if (id == SPRUCE_DOOR)
            {
                return new BlockDoorSpruce();
            }
            else if (id == BIRCH_DOOR)
            {
                return new BlockDoorBirch();
            }
            else if (id == JUNGLE_DOOR)
            {
                return new BlockDoorJungle();
            }
            else if (id == ACACIA_DOOR)
            {
                return new BlockDoorAcacia();
            }
            else if (id == DARK_OAK_DOOR)
            {
                return new BlockDoorDarkOak();
            }
            else if (id == GRASS_PATH)
            {
                return new BlockGrassPath();
            }
            else if (id == FRAME)
            {
                return new BlockFrame();
            }
            else if (id == CHORUS_FLOWER)
            {
                return new BlockChorusFlower();
            }
            else if (id == PURPUR_BLOCK)
            {
                return new BlockPurpurBlock();
            }
            else if (id == PURPUR_STAIRS)
            {
                return new BlockPurpurStairs();
            }

            else if (id == UNDYED_SHULKER_BOX)
            {
                return new BlockShulkerBoxUndyed();
            }
            else if (id == END_BRICKS)
            {
                return new BlockEndBricks();
            }
            else if (id == FROSTED_ICE)
            {
                return new BlockFrostedIce();
            }
            else if (id == END_ROD)
            {
                return new BlockEndRod();
            }
            else if (id == END_GATEWAY)
            {
                return new BlockEndGateway();
            }

            else if (id == MAGMA)
            {
                return new BlockMagma();
            }
            else if (id == NETHER_WART_BLOCK)
            {
                return new BlockNetherWartBlock();
            }
            else if (id == RED_NETHER_BRICK)
            {
                return new BlockRedNetherBrick();
            }
            else if (id == BONE_BLOCK)
            {
                return new BlockBoneBlock();
            }

            else if (id == SHULKER_BOX)
            {
                return new BlockShulkerBox();
            }
            else if (id == PURPLE_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaPurple();
            }
            else if (id == WHITE_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaWhite();
            }
            else if (id == ORANGE_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaOrange();
            }
            else if (id == MAGENTA_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaMagenta();
            }
            else if (id == LIGHT_GLAZED_TERRACOTTA)
            {
                return new BlockLightGlazedTerracotta();
            }
            else if (id == YELLOW_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaYellow();
            }
            else if (id == LIME_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaLime();
            }
            else if (id == PINK_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaPink();
            }
            else if (id == GRAY_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaGray();
            }
            else if (id == SILVER_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaSilver();
            }
            else if (id == CYAN_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaCyan();
            }

            else if (id == BLUE_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaBlue();
            }
            else if (id == BROWN_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaBrown();
            }
            else if (id == GREEN_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaGreen();
            }
            else if (id == RED_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaRed();
            }
            else if (id == BLACK_GLAZED_TERRACOTTA)
            {
                return new BlockGlazedTerracottaBlack();
            }
            else if (id == CONCRETE)
            {
                return new BlockConcrete();
            }
            else if (id == CONCRETE_POWDER)
            {
                return new BlockConcretePowder();
            }

            else if (id == CHORUS_PLANT)
            {
                return new BlockChorusPlant();
            }
            else if (id == STAINED_GLASS)
            {
                return new BlockStainedGlass();
            }

            else if (id == PODZOL)
            {
                return new BlockPodzol();
            }
            else if (id == BEETROOT)
            {
                return new BlockBeetroot();
            }
            else if (id == STONECUTTER)
            {
                return new BlockStonecutter();
            }
            else if (id == GLOWINGOBSIDIAN)
            {
                return new BlockGlowingobsidian();
            }
            else if (id == NETHERREACTOR)
            {
                return new BlockNetherreactor();
            }
            else if (id == INFO_UPDATE)
            {
                return new BlockInfoUpdate();
            }
            else if (id == INFO_UPDATE2)
            {
                return new BlockInfoUpdate2();
            }
            else if (id == MOVING_BLOCK)
            {
                return new BlockMovingBlock();
            }
            else if (id == OBSERVER)
            {
                return new BlockObserver();
            }
            else if (id == STRUCTURE_BLOCK)
            {
                return new BlockStructureBlock();
            }

            else
            {
                return new BlockUnknown(id);
            }
        }

        public static Block GetBlock(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;
            int meta = 0;

            if (data.Length == 1)
            {
                int.TryParse(data[0], out id);
            }

            if (data.Length == 2)
            {
                int.TryParse(data[0], out id);
                int.TryParse(data[1], out meta);
            }

            id = id > 255 ? 0 : id;

            try
            {
                BlockFactory factory = new BlockFactory();
                id = (int) factory.GetType().GetField(data[0]).GetValue(factory);
            }
            catch
            {

            }

            Block block = Block.Get(id, meta);
            return block;
        }

        public static void RegisterBlock(Block block)
        {
            BlockFactory.Blocks[block.ID] = block.GetType();
        }
    }
}
