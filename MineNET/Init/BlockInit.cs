using MineNET.Blocks;
using System;

namespace MineNET.Init
{
    public sealed class BlockInit : IDisposable
    {
        public static BlockInit In { get; private set; }

        public BlockAir Air { get; } = new BlockAir();
        public BlockStone Stone { get; } = new BlockStone();
        public BlockGrass Grass { get; } = new BlockGrass();
        public BlockDirt Dirt { get; } = new BlockDirt();

        public BlockInit()
        {
            BlockInit.In = this;
            this.Init();
        }

        public void Init()
        {
            this.Add(this.Air);
            this.Add(this.Stone);
            this.Add(this.Grass);
            this.Add(this.Dirt);
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
