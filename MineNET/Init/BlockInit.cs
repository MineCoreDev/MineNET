using System;
using System.Reflection;
using MineNET.Blocks;
using MineNET.Items;

namespace MineNET.Init
{
    public sealed class BlockInit : IDisposable
    {
        public static BlockInit In { get; private set; }

        public BlockInit()
        {
            BlockInit.In = this;
            this.Init();
        }

        public void Init()
        {
            this.Set(new BlockAir());
            this.Set(new BlockStone());
            this.Set(new BlockGrass());
            this.Set(new BlockDirt());
            this.Set(new Block(4, "Cobblestone") { Hardness = 2f, Resistance = 30f, ToolType = ItemToolType.PICKAXE });
            this.Set(new BlockPlanks());
            this.Set(new BlockSapling());
            this.Set(new BlockEmptyDrops(7, "Bedrock") { Resistance = 18000000f });
            this.Set(new BlockDynamicLiquid(8, "FlowingWater") { Resistance = 500f, LightOpacity = 3 });
            this.Set(new BlockStaticLiquid(9, "Water") { Resistance = 500f, LightOpacity = 3 });
            this.Set(new BlockDynamicLiquid(10, "FlowingLava") { Resistance = 500f, LightLevel = 15 });
            this.Set(new BlockStaticLiquid(11, "Lava") { Resistance = 500f, LightLevel = 15 });
            this.Set(new BlockSand());
            this.Set(new BlockGravel());
            this.Set(new BlockGoldOre());
            this.Set(new BlockIronOre());
            this.Set(new BlockCoalOre());
            this.Set(new BlockLog());
            this.Set(new BlockLeave(18, "Leaves"));
            this.Set(new BlockSponge());
            this.Set(new BlockGlass(20, "Glass"));
            this.Set(new BlockLapisOre());
            this.Set(new BlockSolid(22, "LapisBlock") { Hardness = 3f, Resistance = 15f, ToolType = ItemToolType.PICKAXE, ToolTier = ItemToolTier.STONE });
            this.Set(new BlockDispenser());
            this.Set(new BlockSandstone());

            FieldInfo[] fields = new BlockIDs().GetType().GetFields();
            for (int i = 25; i < fields.Length; ++i)
            {
                FieldInfo field = fields[i];
                this.Set(new Block((int) field.GetValue(null), field.Name));
            }

            this.Set(new BlockChest());
        }

        public void Set(Block block)
        {
            MineNET_Registries.Block[block.ID] = block;
        }

        public void Dispose()
        {
            BlockInit.In = null;
        }
    }
}
