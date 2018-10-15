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
            this.Add(new BlockAir());
            this.Add(new BlockStone());
            this.Add(new BlockGrass());
            this.Add(new BlockDirt());
            this.Add(new Block(4, "Cobblestone") { Hardness = 2f, Resistance = 30f, ToolType = ItemToolType.PICKAXE });
            this.Add(new BlockPlanks());
            this.Add(new BlockSapling());
            this.Add(new BlockEmptyDrops(7, "Bedrock") { Resistance = 18000000f });
            this.Add(new BlockDynamicLiquid(8, "FlowingWater") { Resistance = 500f, LightOpacity = 3 });
            this.Add(new BlockStaticLiquid(9, "Water") { Resistance = 500f, LightOpacity = 3 });
            this.Add(new BlockDynamicLiquid(10, "FlowingLava") { Resistance = 500f, LightLevel = 15 });
            this.Add(new BlockStaticLiquid(11, "Lava") { Resistance = 500f, LightLevel = 15 });
            this.Add(new BlockSand());
            this.Add(new BlockGravel());
            this.Add(new BlockGoldOre());
            this.Add(new BlockIronOre());
            this.Add(new BlockCoalOre());
            this.Add(new BlockLog());
            this.Add(new BlockLeave(18, "Leaves"));
            this.Add(new BlockSponge());
            this.Add(new BlockGlass(20, "Glass"));
            this.Add(new BlockLapisOre());
            this.Add(new BlockSolid(22, "LapisBlock") { Hardness = 3f, Resistance = 15f, ToolType = ItemToolType.PICKAXE, ToolTier = ItemToolTier.STONE });
            this.Add(new BlockDispenser());
            this.Add(new BlockSandstone());

            FieldInfo[] fields = new BlockIDs().GetType().GetFields();
            for (int i = 25; i < fields.Length; ++i)
            {
                FieldInfo field = fields[i];
                this.Add(new Block((int) field.GetValue(null), field.Name));
            }
        }

        public void Add(Block block)
        {
            MineNET_Registries.Block.Add(block.ID, block);
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
