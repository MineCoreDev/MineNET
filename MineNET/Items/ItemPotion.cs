using MineNET.Entities.Data;
using MineNET.Entities.Players;
using MineNET.Items.Data;

namespace MineNET.Items
{
    public class ItemPotion : Item, IConsumeable
    {
        public const int WATER = 0;
        public const int MUNDANE = 1;
        public const int LONG_MUNDANE = 2;
        public const int THICK = 3;
        public const int AWKWARD = 4;
        public const int NIGHT_VISION = 5;
        public const int LONG_NIGHT_VISION = 6;
        public const int INVISIBILITY = 7;
        public const int LONG_INVISIBILITY = 8;
        public const int LEAPING = 9;
        public const int LONG_LEAPING = 10;
        public const int STRONG_LEAPING = 11;
        public const int FIRE_RESISTANCE = 12;
        public const int LONG_FIRE_RESISTANCE = 13;
        public const int SWIFTNESS = 14;
        public const int LONG_SWIFTNESS = 15;
        public const int STRONG_SWIFTNESS = 16;
        public const int SLOWNESS = 17;
        public const int LONG_SLOWNESS = 18;
        public const int WATER_BREATHING = 19;
        public const int LONG_WATER_BREATHING = 20;
        public const int HEALING = 21;
        public const int STRONG_HEALING = 22;
        public const int HARMING = 23;
        public const int STRONG_HARMING = 24;
        public const int POISON = 25;
        public const int LONG_POISON = 26;
        public const int STRONG_POISON = 27;
        public const int REGENERATION = 28;
        public const int LONG_REGENERATION = 29;
        public const int STRONG_REGENERATION = 30;
        public const int STRENGTH = 31;
        public const int LONG_STRENGTH = 32;
        public const int STRONG_STRENGTH = 33;
        public const int WEAKNESS = 34;
        public const int LONG_WEAKNESS = 35;
        public const int WITHER = 36;

        public ItemPotion() : base(ItemFactory.POTION)
        {

        }

        public override string Name
        {
            get
            {
                return "Potion";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }

        public void OnConsume(Player player)
        {
            Effect[] effects = this.AdditionalEffects;
            for (int i = 0; i < effects.Length; ++i)
            {
                player.AddEffect(effects[i]);
            }
            if (player.IsCreative)
            {
                player.Inventory.AddItem(Item.Get(ItemFactory.GLASS_BOTTLE));
                return;
            }
            player.Inventory.MainHandItem = Item.Get(ItemFactory.GLASS_BOTTLE);
        }

        public Effect[] AdditionalEffects
        {
            get
            {
                if (this.Damage == ItemPotion.WATER || this.Damage == ItemPotion.MUNDANE || this.Damage == ItemPotion.LONG_MUNDANE || this.Damage == ItemPotion.THICK || this.Damage == ItemPotion.AWKWARD)
                {
                    return new Effect[0];
                }
                else if (this.Damage == ItemPotion.NIGHT_VISION)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.NIGHT_VISION, 3600)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_NIGHT_VISION)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.NIGHT_VISION, 9600)
                    };
                }
                else if (this.Damage == ItemPotion.INVISIBILITY)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.INVISIBILITY, 3600)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_INVISIBILITY)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.INVISIBILITY, 9600)
                    };
                }
                else if (this.Damage == ItemPotion.LEAPING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.JUMP_BOOST, 3600)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_LEAPING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.NIGHT_VISION, 9600)
                    };
                }
                else if (this.Damage == ItemPotion.STRONG_LEAPING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.NIGHT_VISION, 1800, 1)
                    };
                }
                else if (this.Damage == ItemPotion.FIRE_RESISTANCE)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.FIRE_RESISTANCE, 3600)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_FIRE_RESISTANCE)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.FIRE_RESISTANCE, 9600)
                    };
                }
                else if (this.Damage == ItemPotion.SWIFTNESS)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.SPEED, 3600)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_SWIFTNESS)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.SPEED, 9600)
                    };
                }
                else if (this.Damage == ItemPotion.STRONG_SWIFTNESS)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.SPEED, 1800, 1)
                    };
                }
                else if (this.Damage == ItemPotion.SLOWNESS)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.SLOWNESS, 1800)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_SLOWNESS)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.SLOWNESS, 4800)
                    };
                }
                else if (this.Damage == ItemPotion.WATER_BREATHING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.WATER_BREATHING, 3600)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_WATER_BREATHING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.WATER_BREATHING, 9600)
                    };
                }
                else if (this.Damage == ItemPotion.HEALING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.INSTANT_HEALTH, 1)
                    };
                }
                else if (this.Damage == ItemPotion.STRONG_HEALING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.INSTANT_HEALTH, 1, 1)
                    };
                }
                else if (this.Damage == ItemPotion.HARMING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.INSTANT_DAMAGE, 1)
                    };
                }
                else if (this.Damage == ItemPotion.STRONG_HARMING)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.INSTANT_DAMAGE, 1, 1)
                    };
                }
                else if (this.Damage == ItemPotion.POISON)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.POISON, 900)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_POISON)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.POISON, 2400)
                    };
                }
                else if (this.Damage == ItemPotion.STRONG_POISON)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.POISON, 440, 1)
                    };
                }
                else if (this.Damage == ItemPotion.REGENERATION)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.REGENERATION, 900)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_REGENERATION)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.REGENERATION, 2400)
                    };
                }
                else if (this.Damage == ItemPotion.STRONG_REGENERATION)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.REGENERATION, 440, 1)
                    };
                }
                else if (this.Damage == ItemPotion.STRENGTH)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.STRENGTH, 3600)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_STRENGTH)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.STRENGTH, 9600)
                    };
                }
                else if (this.Damage == ItemPotion.STRONG_STRENGTH)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.STRENGTH, 1800, 1)
                    };
                }
                else if (this.Damage == ItemPotion.WEAKNESS)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.WEAKNESS, 1800)
                    };
                }
                else if (this.Damage == ItemPotion.LONG_WEAKNESS)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.WEAKNESS, 4800, 1)
                    };
                }
                else if (this.Damage == ItemPotion.WITHER)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.WITHER, 800, 1)
                    };
                }
                else
                {
                    return new Effect[0];
                }
            }
        }
    }
}
