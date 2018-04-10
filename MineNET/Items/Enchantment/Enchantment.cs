namespace MineNET.Items.Enchantment
{
    public abstract class Enchantment
    {
        public const int PROTECTION = 0;
        public const int FIRE_PROTECTION = 1;
        public const int FEATHER_FALLING = 2;
        public const int BLAST_PROTECTION = 3;
        public const int PROJECTILE_PROTECTION = 4;
        public const int THORNS = 5;
        public const int RESPIRATION = 6;
        public const int DEPTH_STRIDER = 7;
        public const int AQUA_AFFINITY = 8;
        public const int SHARPNESS = 9;
        public const int SMITE = 10;
        public const int BANE_OF_ARTHROPODS = 11;
        public const int KNOCKBACK = 12;
        public const int FIRE_ASPECT = 13;
        public const int LOOTING = 14;
        public const int EFFICIENCY = 15;
        public const int SILK_TOUCH = 16;
        public const int UNBREAKING = 17;
        public const int FORTUNE = 18;
        public const int POWER = 19;
        public const int PUNCH = 20;
        public const int FLAME = 21;
        public const int INFINITY = 22;
        public const int LUCK_OF_THE_SEA = 23;
        public const int LURE = 24;
        public const int FROST_WALKER = 25;
        public const int MENDING = 26;

        public static Enchantment GetEnchantment(int id, int level = 1)
        {
            Enchantment enchantment;
            if (id == Enchantment.PROTECTION)
            {
                enchantment = new EnchantmentProtection();
            }
            else if (id == Enchantment.FIRE_PROTECTION)
            {
                enchantment = new EnchantmentFireProtection();
            }
            else if (id == Enchantment.FEATHER_FALLING)
            {
                enchantment = new EnchantmentFeatherFalling();
            }
            else if (id == Enchantment.BLAST_PROTECTION)
            {
                enchantment = new EnchantmentBlastProtection();
            }
            else if (id == Enchantment.PROJECTILE_PROTECTION)
            {
                enchantment = new EnchantmentProjectileProtection();
            }
            else if (id == Enchantment.THORNS)
            {
                enchantment = new EnchantmentThorns();
            }
            else if (id == Enchantment.RESPIRATION)
            {
                enchantment = new EnchantmentRespiration();
            }
            else if (id == Enchantment.DEPTH_STRIDER)
            {
                enchantment = new EnchantmentDepthStrider();
            }
            else if (id == Enchantment.AQUA_AFFINITY)
            {
                enchantment = new EnchantmentAquaAffinity();
            }
            else
            {
                return null;
            }
            enchantment.Level = level;
            return enchantment;
        }

        public abstract int ID { get; }
        public abstract int MinLevel { get; }
        public abstract int MaxLevel { get; }
        public abstract int Weight { get; }

        public abstract string Name { get; }

        public int Level { get; set; } = 1;
    }
}
