using System;
using MineNET.Blocks;

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
            this.Add(new BlockStone() { Hardness = 1.5f, Resistance = 30f });
            this.Add(new BlockGrass() { Hardness = 0.6f, Resistance = 3f });
            this.Add(new BlockDirt() { Hardness = 0.5f, Resistance = 2.5f });
            this.Add(new Block(4, "Cobblestone") { Hardness = 2f, Resistance = 30f });
            this.Add(new BlockPlanks() { Hardness = 2.0f, Resistance = 15f });
            this.Add(new BlockSapling());
            this.Add(new BlockEmptyDrops(7, "Bedrock") { Resistance = 18000000f });
            this.Add(new BlockDynamicLiquid(8, "FlowingWater") { Hardness = 500f, LightOpacity = 3 });
            this.Add(new BlockStaticLiquid(9, "Water") { Hardness = 500f, LightOpacity = 3 });
        }

        public void Add(Block block)
        {
            MineNET_Registries.Block.Add(block.ID, block);
        }

        public void Dispose()
        {
            BlockInit.In = null;
        }
    }
}
