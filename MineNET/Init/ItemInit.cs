using System.Reflection;
using MineNET.Blocks;
using MineNET.Items;

namespace MineNET.Init
{
    public sealed class ItemInit
    {
        public static ItemInit In { get; private set; }

        public ItemInit()
        {
            ItemInit.In = this;
            this.Init();

            CreativeItemList.LoadCreativeItems();
        }

        public void Init()
        {
            foreach (int key in MineNET_Registries.Block.Keys)
            {
                if (MineNET_Registries.Block.TryGetValue(key, out Block block))
                {
                    this.Set(new ItemBlock(block));
                }
            }

            FieldInfo[] fields = new ItemIDs().GetType().GetFields(); //TODO
            for (int i = 0; i < fields.Length; ++i)
            {
                FieldInfo field = fields[i];
                this.Set(new ItemUnknown((int) field.GetValue(null), field.Name));
            }

            this.Set(new ItemIronShovel()); //256
            this.Set(new ItemIronPickaxe()); //257
            this.Set(new ItemIronAxe()); //258
            this.Set(new ItemFlintAndSteel()); //259
            this.Set(new ItemApple()); //260
            this.Set(new ItemBow()); //261
            this.Set(new ItemArrow()); //262
            this.Set(new ItemCoal()); //263
            this.Set(new ItemDiamond()); //264
            this.Set(new ItemIronIngot()); //265
            this.Set(new ItemGoldIngot()); //266
            this.Set(new ItemIronSword()); //267
            this.Set(new ItemWoodenSword()); //268
            this.Set(new ItemWoodenShovel()); //269
            this.Set(new ItemWoodenPickaxe()); //270
            this.Set(new ItemWoodenAxe()); //271
            this.Set(new ItemStoneSword()); //272
            this.Set(new ItemStoneShovel()); //273
            this.Set(new ItemStonePickaxe()); //274
            this.Set(new ItemStoneAxe()); //275
            this.Set(new ItemDiamondSword()); //276
            this.Set(new ItemDiamondShovel()); //277
            this.Set(new ItemDiamondPickaxe()); //278
            this.Set(new ItemDiamondAxe()); //279
            this.Set(new ItemStick()); //280
            this.Set(new ItemBowl()); //281
            this.Set(new ItemMushroomStew()); //282
            this.Set(new ItemGoldenSword()); //283
            this.Set(new ItemGoldenShovel()); //284
            this.Set(new ItemGoldenPickaxe()); //285
            this.Set(new ItemGoldenAxe()); //286
            this.Set(new ItemString()); //287
            this.Set(new ItemFeather()); //288
            this.Set(new ItemGunpowder()); //289
            this.Set(new ItemWoodenHoe()); //290
            this.Set(new ItemStoneHoe()); //291
            this.Set(new ItemIronHoe()); //292
            this.Set(new ItemDiamondHoe()); //293
            this.Set(new ItemGoldenHoe()); //294
            this.Set(new ItemWheatSeeds()); //295
            this.Set(new ItemWheat()); //296
            this.Set(new ItemBread()); //297
            this.Set(new ItemLeatherHelmet()); //298
            this.Set(new ItemLeatherChestplate()); //299
            this.Set(new ItemLeatherLeggings()); //300
            this.Set(new ItemLeatherBoots()); //301
            this.Set(new ItemChainmailHelmet()); //302
            this.Set(new ItemChainmailChestplate()); //303
            this.Set(new ItemChainmailLeggings()); //304
            this.Set(new ItemChainmailBoots()); //305
            this.Set(new ItemIronHelmet()); //306
            this.Set(new ItemIronChestplate()); //307
            this.Set(new ItemIronLeggings()); //308
            this.Set(new ItemIronBoots()); //309
            this.Set(new ItemDiamondHelmet()); //310
            this.Set(new ItemDiamondChestplate()); //311
            this.Set(new ItemDiamondLeggings()); //312
            this.Set(new ItemDiamondBoots()); //313
            this.Set(new ItemGoldenHelmet()); //314
            this.Set(new ItemGoldenChestplate()); //315
            this.Set(new ItemGoldenLeggings()); //316
            this.Set(new ItemGoldenBoots()); //317
            this.Set(new ItemFlint()); //318
            this.Set(new ItemPorkchop()); //319
            this.Set(new ItemCookedPorkchop()); //320
            this.Set(new ItemPainting()); //321
            this.Set(new ItemGoldenApple()); //322
            this.Set(new ItemSign()); //323
            this.Set(new ItemWoodenDoor()); //324
            this.Set(new ItemBucket()); //325

            this.Set(new ItemMinecart()); //328
            this.Set(new ItemSaddle()); //329
            this.Set(new ItemIronDoor()); //330
            this.Set(new ItemRedstone()); //331
            this.Set(new ItemSnowball()); //332
            this.Set(new ItemBoat()); //333
            this.Set(new ItemLeather()); //334
            this.Set(new ItemKelp()); //335
            this.Set(new ItemBrick()); //336
            this.Set(new ItemClayBall()); //337
            this.Set(new ItemReeds()); //338
            this.Set(new ItemPaper()); //339
            this.Set(new ItemBook()); //340
            this.Set(new ItemSlimeBall()); //341
            this.Set(new ItemChestMinecart()); //342

            this.Set(new ItemEgg()); //344
            this.Set(new ItemCompass()); //345
            this.Set(new ItemFishingRod()); //346
            this.Set(new ItemClock()); //347
            this.Set(new ItemGlowstoneDust()); //348
            this.Set(new ItemFish()); //349
            this.Set(new ItemCookedFish()); //350
            this.Set(new ItemDye()); //351
            this.Set(new ItemBone()); //352
        }

        public void Set(Item item)
        {
            MineNET_Registries.Item[item.ID] = item;
        }

        public void Dispose()
        {
            ItemInit.In = null;
        }
    }
}
