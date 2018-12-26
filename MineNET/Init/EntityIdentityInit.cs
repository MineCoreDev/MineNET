using System;
using MineNET.Entities;

namespace MineNET.Init
{
    public sealed class EntityIdentityInit : IDisposable
    {
        public static EntityIdentityInit In { get; private set; }

        public EntityIdentityInit()
        {
            EntityIdentityInit.In = this;
            this.Init();
        }

        public void Init()
        {
            this.Set(EntityIDs.CHICKEN, "minecraft:chicken");
            this.Set(EntityIDs.COW, "minecraft:cow");
            this.Set(EntityIDs.PIG, "minecraft:pig");
            this.Set(EntityIDs.SHEEP, "minecraft:sheep");
            this.Set(EntityIDs.WOLF, "minecraft:wolf");
            this.Set(EntityIDs.VILLAGER, "minecraft:villager");
            this.Set(EntityIDs.MOOSHROOM, "minecraft:mooshroom");
            this.Set(EntityIDs.SQUID, "minecraft:squid");
            this.Set(EntityIDs.RABBIT, "minecraft:rabbit");
            this.Set(EntityIDs.BAT, "minecraft:bat");
            this.Set(EntityIDs.IRON_GOLEM, "minecraft:iron_golem");
            this.Set(EntityIDs.SNOW_GOLEM, "minecraft:snow_golem");
            this.Set(EntityIDs.OCELOT, "minecraft:ocelot");
            this.Set(EntityIDs.HORSE, "minecraft:horse");
            this.Set(EntityIDs.DONKEY, "minecraft:donkey");
            this.Set(EntityIDs.MULE, "minecraft:mule");
            this.Set(EntityIDs.SKELETON_HORSE, "minecraft:skeleton_horse");
            this.Set(EntityIDs.ZOMBIE_HORSE, "minecraft:zombie_horse");
            this.Set(EntityIDs.POLAR_BEAR, "minecraft:polar_bear");
            this.Set(EntityIDs.LLAMA, "minecraft:llama");
            this.Set(EntityIDs.PARROT, "minecraft:parrot");
            this.Set(EntityIDs.ZOMBIE, "minecraft:zombie");
            this.Set(EntityIDs.CREEPER, "minecraft:creeper");
            this.Set(EntityIDs.SKELETON, "minecraft:skeleton");
            this.Set(EntityIDs.SPIDER, "minecraft:spider");
            this.Set(EntityIDs.ZOMBIE_PIGMAN, "minecraft:zombie_pigman");
            this.Set(EntityIDs.SLIME, "minecraft:slime");
            this.Set(EntityIDs.ENDERMAN, "minecraft:enderman");
            this.Set(EntityIDs.SILVERFISH, "minecraft:silverfish");
            this.Set(EntityIDs.CAVE_SPIDER, "minecraft:cave_spider");
            this.Set(EntityIDs.GHAST, "minecraft:ghast");
            this.Set(EntityIDs.MAGMA_CUBE, "minecraft:magma_cube");
            this.Set(EntityIDs.BLAZE, "minecraft:blaze");
            this.Set(EntityIDs.ZOMBIE_VILLAGER, "minecraft:zombie_villager");
            this.Set(EntityIDs.WITCH, "minecraft:witch");
            this.Set(EntityIDs.STRAY, "minecraft:stray");
            this.Set(EntityIDs.HUSK, "minecraft:husk");
            this.Set(EntityIDs.WITHER_SKELETON, "minecraft:wither_skull");
            this.Set(EntityIDs.GUARDIAN, "minecraft:guardian");
            this.Set(EntityIDs.ELDER_GUARDIAN, "minecraft:elder_guardian");
            this.Set(EntityIDs.NPC, "minecraft:npc");
            this.Set(EntityIDs.WITHER, "minecraft:wither");
            this.Set(EntityIDs.ENDER_DRAGON, "minecraft:ender_dragon");
            this.Set(EntityIDs.SHULKER, "minecraft:shulker");
            this.Set(EntityIDs.ENDERMITE, "minecraft:endermite");
            this.Set(EntityIDs.AGENT, "minecraft:agent");
            this.Set(EntityIDs.VINDICATOR, "minecraft:vindicator");
            this.Set(EntityIDs.PHANTOM, "minecraft:phantom");

            this.Set(EntityIDs.ARMOR_STAND, "minecraft:armor_stand");
            this.Set(EntityIDs.TRIPOD_CAMERA, "minecraft:tripod_camera");
            this.Set(EntityIDs.PLAYER, "minecraft:player");
            this.Set(EntityIDs.ITEM, "minecraft:item");
            this.Set(EntityIDs.TNT, "minecraft:tnt");
            this.Set(EntityIDs.FALLING_BLOCK, "minecraft:falling_block");
            this.Set(EntityIDs.MOVING_BLOCK, "minecraft:moving_block");
            this.Set(EntityIDs.XP_BOTTLE, "minecraft:xp_bottle");
            this.Set(EntityIDs.XP_ORB, "minecraft:xp_orb");
            this.Set(EntityIDs.EYE_OF_ENDER_SIGNAL, "minecraft:eye_of_ender_signal");
            this.Set(EntityIDs.ENDER_CRYSTAL, "minecraft:ender_crystal");
            this.Set(EntityIDs.FIREWORKS_ROCKET, "minecraft:fireworks_rocket");
            this.Set(EntityIDs.THROWN_TRIDENT, "minecraft:thrown_trident");
            this.Set(EntityIDs.TURTLE, "minecraft:turtle");
            this.Set(EntityIDs.CAT, "minecraft:cat");
            this.Set(EntityIDs.SHULKER_BULLET, "minecraft:shulker_bullet");
            this.Set(EntityIDs.FISHING_HOOK, "minecraft:fishing_hook");
            this.Set(EntityIDs.CHALKBOARD, "minecraft:chalkboard");
            this.Set(EntityIDs.DRAGON_FIREBALL, "minecraft:dragon_fireball");
            this.Set(EntityIDs.ARROW, "minecraft:arrow");
            this.Set(EntityIDs.SNOWBALL, "minecraft:snowball");
            this.Set(EntityIDs.EGG, "minecraft:egg");
            this.Set(EntityIDs.PAINTING, "minecraft:painting");
            this.Set(EntityIDs.MINECART, "minecraft:minecart");
            this.Set(EntityIDs.LARGE_FIREBALL, "minecraft:large_fireball");
            this.Set(EntityIDs.SPLASH_POTION, "minecraft:splash_potion");
            this.Set(EntityIDs.ENDER_PEARL, "minecraft:ender_pearl");
            this.Set(EntityIDs.LEASH_KNOT, "minecraft:leash_knot");
            this.Set(EntityIDs.WITHER_SKULL, "minecraft:wither_skull");
            this.Set(EntityIDs.BOAT, "minecraft:boat");
            this.Set(EntityIDs.WITHER_SKULL_DANGEROUS, "minecraft:wither_skull_dangerous");

            this.Set(EntityIDs.LIGHTNING_BOLT, "minecraft:lightning_bolt");
            this.Set(EntityIDs.SMALL_FIREBALL, "minecraft:small_fireball");
            this.Set(EntityIDs.AREA_EFFECT_CLOUD, "minecraft:area_effect_cloud");
            this.Set(EntityIDs.HOPPER_MINECART, "minecraft:hopper_minecart");
            this.Set(EntityIDs.TNT_MINECART, "minecraft:tnt_minecart");
            this.Set(EntityIDs.CHEST_MINECART, "minecraft:chest_minecart");

            this.Set(EntityIDs.COMMAND_BLOCK_MINECART, "minecraft:command_block_minecart");
            this.Set(EntityIDs.LINGERING_POTION, "minecraft:lingering_potion");
            this.Set(EntityIDs.LLAMA_SPIT, "minecraft:llama_spit");
            this.Set(EntityIDs.EVOCATION_FANG, "minecraft:evocation_fang");
            this.Set(EntityIDs.EVOCATION_ILLAGER, "minecraft:evocation_illager");
            this.Set(EntityIDs.VEX, "minecraft:vex");
            this.Set(EntityIDs.ICE_BOMB, "minecraft:ice_bomb");
            this.Set(EntityIDs.BALLOON, "minecraft:balloon");
            this.Set(EntityIDs.PUFFERFISH, "minecraft:pufferfish");
            this.Set(EntityIDs.SALMON, "minecraft:salmon");
            this.Set(EntityIDs.DROWNED, "minecraft:drowned");
            this.Set(EntityIDs.TROPICAL_FISH, "minecraft:tropicalfish");
            this.Set(EntityIDs.COD, "minecraft:cod");
            this.Set(EntityIDs.PANDA, "minecraft:panda");
        }

        public void Set(int networkId, string id)
        {
            MineNET_Registries.EntityIdentity[networkId] = id;
        }

        public void Dispose()
        {
            EntityIdentityInit.In = null;
        }
    }
}
